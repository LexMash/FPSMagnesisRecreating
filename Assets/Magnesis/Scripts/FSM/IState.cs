using System;

namespace Infrastructure
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update(float deltaTime);
    }
}
