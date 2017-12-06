using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public abstract class StateMachineGeneric<T> : StateMachineBase where T : EntityCore
    {
        private T Holder = null;
        private AiStateInterface<T> currentState = null;
        private AiStateInterface<T> PreviousState = null;

        private void Start()
        {
            Init_Target_InitState();
        }

        protected abstract void Init_Target_InitState();

        protected void Configure(T holder, AiStateInterface<T> initState)
        {
            Holder = holder;
            ChangeState(initState);
        }

        protected virtual void Update() {
            if (currentState != null) { currentState.Execute(Holder); }
        }

        public void ChangeState(AiStateInterface<T> e)
        {
            PreviousState = currentState;

            if (currentState != null) {
                currentState.Exit(Holder);
            }

            currentState = e;

            if (currentState != null) {
                currentState.Enter(Holder);
            }
        }

        public void RevertToPreviousState()
        {
            if (PreviousState != null)
            {
                ChangeState(PreviousState);
            }
        }
    }
}
