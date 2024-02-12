using Unity.Burst.CompilerServices;

namespace Infrastructure
{
    public interface IStateMachine
    {
        static IState EmptyState => _emptyState;      
        IStateMachine RegisterState(IState state);
        void SetState<T>() where T : IState;
        void Update(float deltaTime);

        private static readonly IState _emptyState = new EmptyState();
    }
}
