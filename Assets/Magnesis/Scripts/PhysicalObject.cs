using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalObject : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Vector3 _centerMassOffset;

    private Vector3 InteractionPoint => transform.position + _centerMassOffset;

    public void EnableGravity(bool isEnable) 
        => _rigidBody.useGravity = isEnable;

    public void ApplyVelocity(Vector3 velocity) 
        => _rigidBody.AddForceAtPosition(velocity, InteractionPoint, ForceMode.VelocityChange);

    //public void ApplyVelocityAtPosition(Vector3 velocity, Vector3 position)
    //    => _rigidBody.AddForceAtPosition(velocity, position, ForceMode.VelocityChange);

    private void Reset() 
        => _rigidBody = GetComponent<Rigidbody>();
}
