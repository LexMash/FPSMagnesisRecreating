using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Mover : MonoBehaviour, IFixedUpdatable
{
    [SerializeField] private Transform _rotationTarget;
    [SerializeField] private CharacterController _controller;

    public Vector3 Velocity => _controller.velocity;

    private UpdateService _updateService;

    private Vector3 _gravity = new Vector3(0f, -9.8f, 0f);
    private PlayerConfig _playerConfig;

    private Vector2 _inputDirection;
    private Vector2 _cachedDeltaRotation;

    private float _currentCameraRotationAngle;
    private bool _isInitialed;
    
    public void Init(PlayerConfig config, UpdateService updateService)
    {
        _playerConfig = config;
        _updateService = updateService;

        _updateService.Register<IFixedUpdatable>(this);

        _isInitialed = true;
    }

    private void OnEnable()
    {
        if(_isInitialed)
        {
            _updateService.Register<IFixedUpdatable>(this);
        }      
    }

    private void OnDisable()
    {
        if (_isInitialed)
        {
            _updateService.Unregister<IFixedUpdatable>(this);
        }
    }

    public void FixedTick(float deltaTime)
    {
        ApplyRotation(deltaTime);
        ApplyVelocity(deltaTime);
    }

    public void Tick(float deltaTime) { }
    public void LateTick(float deltaTime) { }

    public void SetDirection(Vector2 vector) => _inputDirection = vector;
    public void SetRotation(Vector2 deltaRotation) => _cachedDeltaRotation = deltaRotation;

    private void ApplyVelocity(float deltaTime)
    {
        Vector3 moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
        Vector3 velocity = (_playerConfig.MovementSpeed * moveDirection + _gravity) * deltaTime;

        _controller.Move(velocity);
    }

    private void ApplyRotation(float deltaTime)
    {
        _cachedDeltaRotation *= _playerConfig.LookRotationSensitivity * deltaTime;
        _currentCameraRotationAngle += -_cachedDeltaRotation.y;

        _currentCameraRotationAngle = ClampAngle(_currentCameraRotationAngle, _playerConfig.BottomAngleClamp, _playerConfig.TopAngleClamp);
        _rotationTarget.localRotation = Quaternion.Euler(_currentCameraRotationAngle, 0.0f, 0.0f);

        transform.Rotate(Vector3.up * _cachedDeltaRotation.x);
    }

    private float ClampAngle(float currentAngle, float minAngle, float maxAngle)
    {
        if (currentAngle < -360f)
            currentAngle += 360f;

        if (currentAngle > 360f)
            currentAngle -= 360f;

        return Mathf.Clamp(currentAngle, minAngle, maxAngle);
    }

    private void Reset()
    {
        _controller = GetComponent<CharacterController>();
    }
}
