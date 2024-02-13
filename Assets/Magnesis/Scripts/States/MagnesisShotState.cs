using Infrastructure;

public class MagnesisShotState : StateBase
{
    private readonly MagnesisData _data;
    private readonly MagnesisView _view;

    public MagnesisShotState(IStateMachine stateMachine, MagnesisData data, MagnesisView view) : base(stateMachine)
    {
        _data = data;
        _view = view;
    }

    public override void Enter()
    {
        base.Enter();

        //заглушка
        if (_data.TargetObject == null)
        {
            _stateMachine.SetState<MagnesisWaitTargetState>();
            return;
        }

        _view.Pivot.position = _data.TargetObject.transform.position;
        _stateMachine.SetState<MagnesisActiveState>();
    }

    public override void Update(float deltaTime)
    {
        //shot logic
    }
}