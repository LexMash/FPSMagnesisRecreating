using UnityEngine;

namespace Infrastructure
{
    public abstract class StateBase : IState
    {
        protected readonly IStateMachine _stateMachine;

        protected StateBase(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            Debug.Log($"Enter State {GetType().Name}");
        }

        public virtual void Exit()
        {
            Debug.Log($"Exit State {GetType().Name}");
        }

        public virtual void Update(float deltaTime)
        {
        }
    }
}

