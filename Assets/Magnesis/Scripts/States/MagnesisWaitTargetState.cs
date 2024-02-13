using Infrastructure;
using UnityEngine;

public class MagnesisWaitTargetState : StateBase
{
    private readonly MagnesisData _data;
    private readonly MagnesisAbilityConfig _config;
    private readonly RayCaster _rayCaster;

    public MagnesisWaitTargetState(
        IStateMachine stateMachine, 
        MagnesisData data, 
        MagnesisAbilityConfig config, 
        RayCaster rayCaster
        ) : base(stateMachine)
    {
        _data = data;
        _config = config;
        _rayCaster = rayCaster;
    }

    public override void Enter()
    {
        base.Enter();

        _rayCaster.OnCasted += OnCasted;
        _rayCaster.Missed += OnMissed;
    }

    private void OnCasted(Collider other)
    {
        var target = other.GetComponentInParent<PhysicalObject>();

        if (target != null )
        {
            //target.HoverEnable();
            _data.TargetObject = target;
        }
    }

    private void OnMissed()
    {
        //_data.TargetObject.HoverDisable();
        _data.TargetObject = null;
    }

    public override void Update(float deltaTime)
    {
        _rayCaster.RayCast(_config.MaxUsageDistance);
    }

    public override void Exit()
    {
        base.Exit();

        _rayCaster.OnCasted -= OnCasted;
        _rayCaster.Missed -= OnMissed;
    }
}
