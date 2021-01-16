using System.Linq;
using Audio;
using Board;
using Stateless;
using UnityEngine;

namespace Machine
{
    public class SpotChooseState : State
    {
        protected override void Awake()
        {
            state = MachineState.SpotChoose;
            base.Awake();
            StateMachineManager.Instance.stateMachine.Configure(state).Permit(MachineTrigger.SpotClicked,MachineState.BallChoose).Permit(MachineTrigger.SpotNotHovered,MachineState.BallChoose);
            StateMachineManager.Instance.stateMachine.Configure(state)
                .OnEntry((() => RaycastInfo.OnPotDetectAction += PotDetection))
                .OnExit(() =>
                {
                    RaycastInfo.OnPotDetectAction -= PotDetection;
                });
            StateMachineManager.Instance.stateMachine.Configure(state)
                .Permit(MachineTrigger.NoMoreMoves, MachineState.GameOver);
        }

        void PotDetection()
        {
            StateMachineManager.Instance.stateMachine.Fire(MachineTrigger.SpotNotHovered);
        }
        
        public override void DoActionInState()
        {
            base.DoActionInState();
            if (Input.GetKeyDown(KeyCode.Mouse0) && RaycastInfo.detectedGameElement is Pot)
            {
                MakeMove();
            }
        }

        private void MakeMove()
        {
            Pot pot = RaycastInfo.detectedGameElement as Pot;
            Ball betweenBall = GetBetweenBall(BallsManager.Instance.ballToMakeMove,pot);
            BallsManager.Instance.MoveBallToCoord(BallsManager.Instance.ballToMakeMove,pot.coordInfo,betweenBall);
            AudioManager.Instance.PlaySound(SoundType.MakeMove);
            StateMachineManager.Instance.stateMachine.Fire(BallsManager.Instance.CheckIfMoveExist() ? MachineTrigger.SpotClicked:MachineTrigger.NoMoreMoves);
        }

        private Ball GetBetweenBall(Ball _selectedBall,Pot _clickedPot)
        {
            int[] foundCoord = new int[2];
            if (_selectedBall.coordInfo.coord[0] - _clickedPot.coordInfo.coord[0] != 0)
            {
                foundCoord[0] = _selectedBall.coordInfo.coord[0] +
                                (_selectedBall.coordInfo.coord[0] - _clickedPot.coordInfo.coord[0] > 0 ? -1 : 1);
                foundCoord[1] = _selectedBall.coordInfo.coord[1];
            }

            else if (_selectedBall.coordInfo.coord[1] - _clickedPot.coordInfo.coord[1] != 0)
            {
                foundCoord[0] = _selectedBall.coordInfo.coord[0];
                foundCoord[1] = _selectedBall.coordInfo.coord[1] +
                                (_selectedBall.coordInfo.coord[1] - _clickedPot.coordInfo.coord[1] > 0 ? -1 : 1);
            }

            Ball ball =  BallsManager.Instance.ballsLeft.FirstOrDefault(x => x.coordInfo.coord[0] == foundCoord[0] && x.coordInfo.coord[1] == foundCoord[1]);

            return ball;

        }
    }
}
