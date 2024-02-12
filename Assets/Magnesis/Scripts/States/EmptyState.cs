using Infrastructure;

public sealed class EmptyState : IState
{
    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Update(float deltaTime)
    {
    }
}
