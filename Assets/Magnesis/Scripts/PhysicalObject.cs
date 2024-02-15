using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalObject : MonoBehaviour, IHighLightable
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Vector3 _interactionPosOffset;

    public event Action<Collision> CollisionEnter;
    public event Action<Collision> CollisionExit;

    public Vector3 Velocity
    {
        get => _rigidBody.velocity;
        set => _rigidBody.velocity = value;
    }

    public Vector3 AngularVelocity
    {
        get => _rigidBody.angularVelocity;
        set => _rigidBody.angularVelocity = value;
    }

    public Vector3 InteractionPoint => transform.position + _interactionPosOffset;
    public void EnableGravity(bool isEnable) => _rigidBody.useGravity = isEnable;

    public void EnableRotation(bool isEnable) 
        => _rigidBody.constraints = isEnable ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeRotation;

    public void ApplyVelocity(Vector3 velocity) 
        => _rigidBody.AddForce(velocity, ForceMode.VelocityChange);

    public void ApplyVelocityAtPosition(Vector3 velocity, Vector3 position)
        => _rigidBody.AddForceAtPosition(velocity, position, ForceMode.VelocityChange);

    public void ApplyVelocityAtPosition(Vector3 velocity)
        => _rigidBody.AddForceAtPosition(velocity, InteractionPoint, ForceMode.VelocityChange);

    public void SetInterpolationType(RigidbodyInterpolation interpolation) 
        => _rigidBody.interpolation = interpolation;

    public void HoverEnable()
    {
        throw new NotImplementedException();
    }

    public void HoverDisable()
    {
        throw new NotImplementedException();
    }

    public void HighLightEnable()
    {
        throw new NotImplementedException();
    }

    public void HighLightDisable()
    {
        throw new NotImplementedException();
    }

    private void OnCollisionEnter(Collision collision) => CollisionEnter?.Invoke(collision);
    private void OnCollisionExit(Collision collision) => CollisionExit?.Invoke(collision);

    private void Reset() => _rigidBody = GetComponent<Rigidbody>();
}
