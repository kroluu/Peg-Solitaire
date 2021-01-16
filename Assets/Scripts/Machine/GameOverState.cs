using System;
using Board;
using UnityEngine;

namespace Machine
{
    public class GameOverState : State
    {
        public event Action OnStartNewGameAction;
        protected override void Awake()
        {
            state = MachineState.GameOver;
            base.Awake();
            StateMachineManager.Instance.stateMachine.Configure(state)
                .Permit(MachineTrigger.StartAgain, MachineState.Initialize).OnEntry(StateMachineManager.Instance.OnGameOver);
        }

        public void OnStartNewGame()
        {
            OnStartNewGameAction?.Invoke();
        }


    }
}
