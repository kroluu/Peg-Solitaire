using Board;
using UnityEngine;

namespace Machine
{
    public class StartPointState : State
    {
        protected override void Awake()
        {
            state = MachineState.StartPoint;
            base.Awake();
            StateMachineManager.Instance.stateMachine.Configure(state).Permit(MachineTrigger.BallClicked,MachineState.BallChoose);
            StateMachineManager.Instance.stateMachine.Configure(state)
                .Permit(MachineTrigger.StartAgain, MachineState.Initialize);
        }

        public override void DoActionInState()
        {
            base.DoActionInState();
            if (Input.GetKeyDown(KeyCode.Mouse0) && RaycastInfo.detectedGameElement is Ball)
            {
                CreateStartPot();
            }
        }

        private void CreateStartPot()
        {
            Ball ball = (RaycastInfo.detectedGameElement as Ball);
            if (ball != null)
            {
                ball.gameObject.SetActive(false);
                CoordInfo coordInfo = ball.coordInfo;
                PotManager.Instance.SetPotState(PotManager.Instance.FindPotByCoords(coordInfo),PotState.Free);
                BallsManager.Instance.ballsLeft.Remove(ball);
            }

            /*if (coordInfo != null)
            {
                Destroy(coordInfo);
            }*/
            PotManager.Instance.OnUpdateScore();
            StateMachineManager.Instance.stateMachine.Fire(MachineTrigger.BallClicked);
            
        }
    }
}
