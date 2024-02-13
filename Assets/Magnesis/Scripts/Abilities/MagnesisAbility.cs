using Infrastructure;

public class MagnesisAbility : IAbility
{
    public AbilityType Type => _config.Type;
    public bool IsActive { get; private set; }

    private readonly IStateMachine _stateMachine;
    private readonly MagnesisData _data;

    private readonly MagnesisAbilityConfig _config;
    private readonly PlayerView _playerView;
    private readonly GameInput _input;

    private bool _isUsing;

    public MagnesisAbility(MagnesisAbilityConfig config, PlayerView playerView, GameInput input)
    {
        _config = config;
        _playerView = playerView;
        _input = input;
        _data = new MagnesisData();

        _stateMachine = new StateMachine();
        _stateMachine
            .RegisterState(new MagnesisWaitTargetState(_stateMachine, _data, _config, _playerView.RayCaster))
            .RegisterState(new MagnesisActiveState(_stateMachine, _data, _config, _playerView.MagnesisView, _input))
            .RegisterState(new MagnesisShotState(_stateMachine, _data, _playerView.MagnesisView))
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
        _isUsing = false;
        _stateMachine.SetState<EmptyState>();
    }

    public void Use()
    {
        if (!_isUsing)
        {
            _stateMachine.SetState<MagnesisShotState>();
            _isUsing = true;
        }        
    }

    public void UpdateState(float deltaTime)
    {
        _stateMachine.Update(deltaTime);
    }
}
