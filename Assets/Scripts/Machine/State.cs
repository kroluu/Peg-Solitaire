using UnityEngine;

namespace Machine
{
    public abstract class State : MonoBehaviour
    {
        protected MachineState state;
        protected virtual void Awake()
        {
            StateMachineManager.Instance.OnChangeStateAction += SetStateInstance;
            StateMachineManager.Instance.stateMachine.Configure(state).OnEntry(()=>StateMachineManager.Instance.OnChangeState(state));
        }

        public virtual void DoActionInState()
        {
            
        }

        protected virtual void OnDestroy()
        {
            if(StateMachineManager.Instance != null)
                StateMachineManager.Instance.OnChangeStateAction -= SetStateInstance;
        }

        private void SetStateInstance(MachineState _machineState)
        {
            if (_machineState == state)
            {
                StateMachineManager.Instance.currentState = this;
            }
        }
    }
}
