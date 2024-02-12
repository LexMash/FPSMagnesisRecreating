using Infrastructure;

public class MagnesisShotState : StateBase
{
    private readonly MagnesisData _data;
    private readonly MagnesisAbilityConfig _config;

    public MagnesisShotState(IStateMachine stateMachine, MagnesisData data, MagnesisAbilityConfig config) : base(stateMachine)
    {
        _data = data;
        _config = config;
    }
}
