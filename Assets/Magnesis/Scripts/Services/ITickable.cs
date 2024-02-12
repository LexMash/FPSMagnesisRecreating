public interface ITickable
{
    void Tick(float deltaTime);
}

public interface IFixedUpdatable : ITickable { }

public interface IUpdatable : ITickable { }

public interface ILateUpdatable : ITickable { }
