public interface ITickable
{
    void FixedTick(float deltaTime);
    void Tick(float deltaTime);
    void LateTick(float deltaTime);
}

public interface IFixedUpdatable : ITickable 
{
}

public interface IUpdatable : ITickable 
{
}

public interface ILateUpdatable : ITickable 
{
}