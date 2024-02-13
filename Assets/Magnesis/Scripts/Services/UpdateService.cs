using System;
using System.Collections.Generic;

public class UpdateService : IDisposable
{
    private readonly Dictionary<Type, List<ITickable>> _updateMap = new();
    private readonly List<ITickable> _fixedUpdatables = new();
    private readonly List<ITickable> _updatables = new();
    private readonly List<ITickable> _lateUpdatables = new();

    public UpdateService()
    {
        _updateMap.Add(typeof(IFixedUpdatable), _fixedUpdatables);
        _updateMap.Add(typeof(IUpdatable), _updatables);
        _updateMap.Add(typeof(ILateUpdatable), _lateUpdatables);
    }

    public void Register<T>(T updatable) where T : ITickable
    {
        if(_updateMap.TryGetValue(typeof(T), out List<ITickable> updatables))
        {
            if (updatables.Contains(updatable))
                return;

            updatables.Add(updatable);
        }
    }

    public void Unregister<T>(T updatable) where T : ITickable
    {
        if (_updateMap.TryGetValue(typeof(T), out List<ITickable> updatables))
        {
            if (updatables.Contains(updatable))
            {
                updatables.Remove(updatable) ;
            }
        }
    }

    public void FixedUpdate(float timeDelta)
    {
        for(int i = 0; i < _fixedUpdatables.Count; i++)
            _fixedUpdatables[i].FixedTick(timeDelta);
    }

    public void Update(float timeDelta)
    {
        for (int i = 0; i < _updatables.Count; i++)
            _updatables[i].Tick(timeDelta);
    }

    public void LateUpdate(float timeDelta) 
    {
        for (int i = 0; i < _lateUpdatables.Count; i++)
            _lateUpdatables[i].LateTick(timeDelta);
    }

    public void Dispose()
    {
        _updateMap.Clear();
        _fixedUpdatables.Clear();
        _updatables.Clear();
        _lateUpdatables.Clear();
    }
}