using System;
using System.Security.Cryptography;
using Board;
using Pattern;
using Stateless;

namespace Machine
{ 
    public class StateMachineManager : Singleton<StateMachineManager>
    {
        public readonly StateMachine<MachineState,MachineTrigger> stateMachine = new StateMachine<MachineState, MachineTrigger>(MachineState.Initialize);
        public State currentState;
        public event Action<MachineState> OnChangeStateAction;
        public event Action OnGameOverAction;
        

        private void Awake()
        {
            AssignInstance(this);
        }

        private void OnDestroy()
        {
            RaycastInfo.OnPotReset();
        }

        public void OnChangeState(MachineState _stateToSet)
        {
            OnChangeStateAction?.Invoke(_stateToSet);
        }

        public void OnGameOver()
        {
            OnGameOverAction?.Invoke();
        }
        
        

        public void Update()
        {
            if (currentState != null)
            {
                currentState.DoActionInState();
            }
                
        }
    }
}
    

