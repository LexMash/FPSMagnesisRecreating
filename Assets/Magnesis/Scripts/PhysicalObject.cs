using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalObject : MonoBehaviour, IHighLightable
{
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [SerializeField] private Vector3 _interactionPosOffset;

    public event Action<Collision> CollisionEnter;
    public event Action<Collision> CollisionExit;

    public Vector3 Velocity
    {
        get => Rigidbody.velocity;
        set => Rigidbody.velocity = value;
    }

    public Vector3 AngularVelocity
    {
        get => Rigidbody.angularVelocity;
        set => Rigidbody.angularVelocity = value;
    }

    public Vector3 InteractionPoint => transform.position + _interactionPosOffset;
    public void EnableGravity(bool isEnable) => Rigidbody.useGravity = isEnable;

    public void EnableRotation(bool isEnable) 
        => Rigidbody.constraints = isEnable ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeRotation;

    public void ApplyVelocity(Vector3 velocity) 
        => Rigidbody.AddForce(velocity, ForceMode.VelocityChange);

    public void ApplyVelocityAtPosition(Vector3 velocity, Vector3 position)
        => Rigidbody.AddForceAtPosition(velocity, position, ForceMode.VelocityChange);

    public void ApplyVelocityAtPosition(Vector3 velocity)
        => Rigidbody.AddForceAtPosition(velocity, InteractionPoint, ForceMode.VelocityChange);

    public void SetInterpolationType(RigidbodyInterpolation interpolation) 
        => Rigidbody.interpolation = interpolation;

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

    private void Reset() => Rigidbody = GetComponent<Rigidbody>();
}
