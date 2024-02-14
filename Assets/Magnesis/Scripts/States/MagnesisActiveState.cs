using Infrastructure;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagnesisActiveState : StateBase
{
    private readonly MagnesisData _data;
    private readonly MagnesisAbilityConfig _config;
    private readonly MagnesisView _magnesisView;
    private readonly GameInput _input;

    private PhysicalObject _currentObject;
    private Vector3 _velocity;
    private Vector3 _targetPivotPosition;
    private bool _onCollision;

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

        _currentObject.CollisionEnter += CurrentObjectCollisionEnter;
        _currentObject.CollisionExit += CurrentObjectCollisionExit;

        _currentObject.EnableGravity(false);
        _currentObject.EnableRotation(false);

        _input.Magnesis.Enable();
        _input.Magnesis.NearFar.performed += NearFarPerformed;

        _targetPivotPosition = _magnesisView.Pivot.localPosition;
    }

    public override void Exit()
    {
        base.Exit();

        _currentObject.EnableGravity(true);
        _currentObject.EnableRotation(true);

        _currentObject.CollisionEnter -= CurrentObjectCollisionEnter;
        _currentObject.CollisionExit -= CurrentObjectCollisionExit;

        _currentObject = null;

        _input.Magnesis.NearFar.performed -= NearFarPerformed;
        _input.Magnesis.Disable();
    }

    public override void Update(float deltaTime)
    {
        SetDistanceFromPivotToPlayer(deltaTime);
        SetObjectPosition(deltaTime);
    }

    private void SetObjectPosition(float deltaTime)
    {
        Vector3 calculatedPosition =
                    Vector3.SmoothDamp(_currentObject.transform.position, _magnesisView.Pivot.position, ref _velocity, _config.SmoothLerp * deltaTime);

        if (!_onCollision)
        {
            _currentObject.transform.position = calculatedPosition;
            _currentObject.Velocity = Vector3.zero;
        }
        else
            _currentObject.Velocity = _velocity;
    }

    private void SetDistanceFromPivotToPlayer(float deltaTime)
    {
        _magnesisView.Pivot.localPosition =
                            Vector3.Lerp(_magnesisView.Pivot.localPosition, _targetPivotPosition, _config.SmoothLerp * deltaTime);
    }

    private void CurrentObjectCollisionEnter(Collision collision) => _onCollision = true;
    private void CurrentObjectCollisionExit(Collision collision) => _onCollision = false;

    private void NearFarPerformed(InputAction.CallbackContext ctx)
    {
        var newTarget = _targetPivotPosition + ctx.ReadValue<float>() * _config.NearFarStep * Vector3.forward;
        var clampedDistance = Mathf.Clamp(newTarget.magnitude, _config.MinUsageDistance, _config.MaxUsageDistance);
        _targetPivotPosition = Vector3.forward * clampedDistance;
    }
}