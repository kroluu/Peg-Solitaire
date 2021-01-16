using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Board;
using Managers;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Machine
{
    public class InitializeState : State
    {
        [SerializeField] private Transform board;
        protected override void Awake()
        {
            state = MachineState.Initialize;
            base.Awake();
            StateMachineManager.Instance.stateMachine.Configure(state).Permit(MachineTrigger.ConfigureEnded,MachineState.StartPoint).OnEntry(()=>MakeConfig(false));
            StateMachineManager.Instance.stateMachine.Configure(state)
                .Permit(MachineTrigger.LoadedGame, MachineState.BallChoose);

        }

        private void Start()
        {
            MakeConfig();
        }

        private void MakeConfig(bool _firstRun=true)
        {
            if (!_firstRun)
            { 
                DestroyImmediate(BallsManager.Instance.gameObject);
                Instantiate(board);
            }
            PotManager.Instance.CoordsInit();
            BallsManager.Instance.CoordsInit();
            if (SceneLoader.loadGameFromSave)
            {
                LoadSave();
            }
            PotManager.Instance.OnUpdateScore();
            StateMachineManager.Instance.stateMachine.Fire(SceneLoader.loadGameFromSave ? MachineTrigger.LoadedGame:MachineTrigger.ConfigureEnded);
        }

        public override void DoActionInState()
        {

        }

        private void LoadSave()
        {
            SavableBoard savableBoard = Settings.Instance.GetSaveFile(Application.persistentDataPath +
                                                                      Settings.PATH_TO_BOARD + "/" +
                                                                      SceneLoader.saveNameToLoad+".json");
            
            #region Balls
            
            foreach (Ball ball in BallsManager.Instance.balls)
            {
                if (!savableBoard.savableBalls.Any(x => x.id == ball.ballId))
                {
                    BallsManager.Instance.RemoveBall(ball);
                    continue;
                }

                SavableBall savableBall = savableBoard.savableBalls.FirstOrDefault(x => x.id == ball.ballId);
                if(savableBall==null) continue;
                ball.LoadBall(savableBall);
                
            }
            
            #endregion

            #region Pots

            foreach (Pot pot in PotManager.Instance.potsList)
            {
                SavablePot savablePot = savableBoard.savablePots.FirstOrDefault(x => x.id == pot.potId);
                pot.LoadPot(savablePot);
            }
            

            #endregion
            
            
            
        }
    }
}
