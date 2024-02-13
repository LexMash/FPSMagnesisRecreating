using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Mover : MonoBehaviour, IFixedUpdatable
{
    [SerializeField] private Transform _rotationTarget;
    [SerializeField] private CharacterController _controller;

    private UpdateService _updateService;

    private float _gravity = -9.81f;
    private Vector3 _velocity;
    private PlayerConfig _playerConfig;

    private Vector2 _inputDirection;
    private Vector2 _cachedDeltaRotation;

    private float _currentCameraRotationAngle;
    private Vector2 _cashedSmoothedDeltaRotation;
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
            _updateService.Unregister<IFixedUpdatable>(this);
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
    public void SetRotation(Vector2 deltaRotation)
    {
        _cachedDeltaRotation = deltaRotation;
    }

    private void ApplyVelocity(float deltaTime)
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        Vector3 moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
        _controller.Move(_playerConfig.MovementSpeed * deltaTime * moveDirection);

        _velocity.y += _gravity * deltaTime;
        _controller.Move(_velocity);
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
