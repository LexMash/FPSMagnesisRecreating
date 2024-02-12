using Infrastructure;

public class MagnesisWaitTargetState : StateBase
{
    private readonly MagnesisData _data;
    private readonly MagnesisAbilityConfig _config;
    private readonly RayCaster _rayCaster;

    private PhysicalObject _currentCastedObject;

    public MagnesisWaitTargetState(IStateMachine stateMachine, MagnesisData data, MagnesisAbilityConfig config) : base(stateMachine)
    {
        _data = data;
        _config = config;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update(float deltaTime)
    {
        
    }
}
