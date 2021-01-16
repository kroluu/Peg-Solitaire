using Audio;
using Board;
using UnityEngine;

namespace Machine
{
    public class BallChooseState : State
    {
        protected override void Awake()
        {
            state = MachineState.BallChoose;
            base.Awake();
            StateMachineManager.Instance.stateMachine.Configure(state).Permit(MachineTrigger.SpotHovered,MachineState.SpotChoose).OnEntryFrom(MachineTrigger.SpotClicked,DeselectBall);
            StateMachineManager.Instance.stateMachine.Configure(state)
                .OnEntry((() => RaycastInfo.OnPotDetectAction += PotDetection))
                .OnExit(() =>
                {
                    RaycastInfo.OnPotDetectAction -= PotDetection;
                });
            StateMachineManager.Instance.stateMachine.Configure(state)
                .Permit(MachineTrigger.StartAgain, MachineState.Initialize);
        }
        public override void DoActionInState()
        {
            base.DoActionInState();
            if (Input.GetKeyDown(KeyCode.Mouse0) && RaycastInfo.detectedGameElement is Ball)
            {
                SelectBall();
            }
        }

        void PotDetection()
        {
            StateMachineManager.Instance.stateMachine.Fire(MachineTrigger.SpotHovered);
        }

        private void SelectBall()
        {
            DeselectBall();
            Ball ball = RaycastInfo.detectedGameElement as Ball;
            if(ball == null) return;
            ball.GetComponent<MeshRenderer>().material.color=Color.red;
            BallsManager.Instance.ballToMakeMove = ball;
            AudioManager.Instance.PlaySound(SoundType.ClickBall);
        }

        private void DeselectBall()
        {
            if (BallsManager.Instance.ballToMakeMove != null)
            {
                BallsManager.Instance.ballToMakeMove.GetComponent<MeshRenderer>().material.color=Color.white;
                BallsManager.Instance.ballToMakeMove = null;
            }
            
        }
    }
}

