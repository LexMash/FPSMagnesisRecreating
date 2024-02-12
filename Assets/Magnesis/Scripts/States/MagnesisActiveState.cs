using Infrastructure;

public class MagnesisActiveState : StateBase
{
    private readonly MagnesisData _data;
    private readonly MagnesisAbilityConfig _config;

    public MagnesisActiveState(IStateMachine stateMachine, MagnesisData data, MagnesisAbilityConfig config) : base(stateMachine)
    {
        _data = data;
        _config = config;
    }

    //private float _maxFollowSpeedToTarget;
    //private float _smoothFactor;

    //private Transform _magneticTarget;

    //private void FixedUpdate()
    //{
    //    //переделать на подписку сервиса обновлений
    //    if (_magneticTarget == null)
    //        return;

    //    Vector3 direction = (_magneticTarget.position - transform.position).normalized;
    //    float magnitude = Mathf.Clamp(direction.magnitude, 0f, _maxFollowSpeedToTarget);

    //    //для объектов типо дверей требуется прикладывать силу к точке. Потом попробуемс
    //    //_rigidBody.AddForceAtPosition(direction * magnitude, _forceApplyOffset + transform.position, ForceMode.VelocityChange);

    //    _rigidBody.velocity = Vector3.Lerp(_rigidBody.velocity, direction * magnitude, _smoothFactor * Time.fixedDeltaTime);
    //}

    //public void SetMagneticTarget(Transform target)
    //{
    //    _magneticTarget = target;
    //    _rigidBody.useGravity = false;
    //}

    //public void RemoveMagneticTarget()
    //{
    //    _magneticTarget = null;
    //    _rigidBody.useGravity = true;
    //}
}
