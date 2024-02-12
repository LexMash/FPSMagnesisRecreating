using Infrastructure;

public class MagnesisAbility : IAbility
{
    public AbilityType Type => _config.Type;
    public bool IsActive { get; private set; }

    private readonly IStateMachine _stateMachine;
    private readonly MagnesisData _data;

    private readonly MagnesisAbilityConfig _config;
    private readonly PlayerView _playerView;

    public MagnesisAbility(MagnesisAbilityConfig config, PlayerView playerView)
    {
        _config = config;
        _playerView = playerView;
        _data = new MagnesisData();

        _stateMachine = new StateMachine();
        _stateMachine
            .RegisterState(new MagnesisWaitTargetState(_stateMachine, _data, _config))
            .RegisterState(new MagnesisActiveState(_stateMachine, _data, _config))
            .RegisterState(new MagnesisShotState(_stateMachine, _data, _config))
            .RegisterState(IStateMachine.EmptyState);
    }

    public void Activate()
    {
        IsActive = true;
        _stateMachine.SetState<MagnesisWaitTargetState>();
    }

    public void Deactivate()
    {
        IsActive = false;
        _stateMachine.SetState<EmptyState>();
    }

    public void Use()
    {
        _stateMachine.SetState<MagnesisShotState>();
    }

    public void UpdateState(float deltaTime)
    {
        _stateMachine.Update(deltaTime);
    }
}
