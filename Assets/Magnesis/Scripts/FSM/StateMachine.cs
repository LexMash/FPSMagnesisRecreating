using System;
using System.Collections.Generic;


namespace Infrastructure
{
    public class StateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }
        private readonly Dictionary<Type, IState> _stateMap = new();     

        public IStateMachine RegisterState(IState state)
        {
            _stateMap[state.GetType()] = state;
            return this;
        }

        public virtual void SetState<T>() where T : IState
        {
            var type = typeof(T);

            if(_stateMap.TryGetValue(type, out IState state))
            {
                CurrentState?.Exit();

                CurrentState = state;

                CurrentState.Enter();
            }
            else
            {
                throw new ArgumentException("StateMachine don`t have this type of state");
            }
        }

        public virtual void Update(float deltaTime) => CurrentState?.Update(deltaTime);
    }
}
