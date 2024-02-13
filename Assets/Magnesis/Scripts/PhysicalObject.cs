using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalObject : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Vector3 _centerMassOffset;
    public Vector3 Velocity
    {
        get => _rigidBody.velocity;
        set => _rigidBody.velocity = value;
    }

    private Vector3 InteractionPoint => transform.position + _centerMassOffset;

    public void EnableGravity(bool isEnable) => _rigidBody.useGravity = isEnable;
    public void EnableRotation(bool isEnable) => _rigidBody.constraints = isEnable ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeRotation;

    public void ApplyVelocity(Vector3 velocity) 
        => _rigidBody.AddForce(velocity, ForceMode.VelocityChange);

    //public void ApplyVelocityAtPosition(Vector3 velocity, Vector3 position)
    //    => _rigidBody.AddForceAtPosition(velocity, position, ForceMode.VelocityChange);

    private void Reset() 
        => _rigidBody = GetComponent<Rigidbody>();

    internal void HoverEnable()
    {
        throw new NotImplementedException();
    }

    internal void HoverDisable()
    {
        throw new NotImplementedException();
    }
}
