using Infrastructure;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagnesisActiveState : StateBase
{
    private const float FWD_BCK_STEP = 0.5f;
    private readonly MagnesisData _data;
    private readonly MagnesisAbilityConfig _config;
    private readonly MagnesisView _magnesisView;
    private readonly GameInput _input;

    private PhysicalObject _currentObject;
    private Vector3 _velocity;
    private Vector3 _cashedPivotLocalPos;

    public MagnesisActiveState(IStateMachine stateMachine,
        MagnesisData data,
        MagnesisAbilityConfig config,
        MagnesisView magnesisView,
        GameInput input
        ) : base(stateMachine)
    {
        _data = data;
        _config = config;
        _magnesisView = magnesisView;
        _input = input;
    }

    public override void Enter()
    {
        base.Enter();

        _currentObject = _data.TargetObject;

        _currentObject.EnableGravity(false);
        _currentObject.EnableRotation(false);

        _input.Magnesis.Enable();
        _input.Magnesis.NearFar.performed += NearFarPerformed;

        _cashedPivotLocalPos = _magnesisView.Pivot.localPosition;
    }

    private void NearFarPerformed(InputAction.CallbackContext ctx)
    {
        _cashedPivotLocalPos += ctx.ReadValue<float>() * FWD_BCK_STEP * Vector3.forward;
    }

    public override void Update(float deltaTime)
    {
        _magnesisView.Pivot.localPosition = Vector3.Lerp(_magnesisView.Pivot.localPosition, _cashedPivotLocalPos, _config.SmoothLerp * deltaTime);

        _currentObject.transform.position = Vector3.SmoothDamp(_currentObject.transform.position, _magnesisView.Pivot.position, ref _velocity, _config.SmoothLerp * deltaTime);
        _currentObject.Velocity = _velocity;
    }

    public override void Exit() 
    { 
        base.Exit();

        _currentObject.EnableGravity(true);
        _currentObject.EnableRotation(true);

        _currentObject = null;

        _input.Magnesis.NearFar.performed -= NearFarPerformed;
        _input.Magnesis.Disable();
    }
}
