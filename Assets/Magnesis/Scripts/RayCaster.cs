using System;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _targetMask;

    public event Action<Collider> OnCasted;
    public event Action Missed;

    private Collider _cashedCollider;
    private bool _isMissEarly;

    private Vector3 _screenCenter;
    private RaycastHit[] _raycastHits;

    private void Awake()
    {
        _raycastHits = new RaycastHit[1];
        _screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    public void RayCast(float distance)
    {
        Ray ray = _camera.ScreenPointToRay(_screenCenter);

        if (Physics.RaycastNonAlloc(ray, _raycastHits, distance, _targetMask, QueryTriggerInteraction.Ignore) > 0)
        {
            Collider collider = _raycastHits[0].collider;

            //Debug.Log(collider.name);

            if (IsSameObject(collider))
                return;

            HandleHit(collider);
            return;
        }

        HandleMiss();
    }

    private bool IsSameObject(Collider collider) => _cashedCollider == collider;

    private void HandleHit(Collider other)
    {
        _cashedCollider = other;
        OnCasted?.Invoke(_cashedCollider);
        _isMissEarly = false;
    }

    private void HandleMiss()
    {
        if (!_isMissEarly)
        {
            _isMissEarly = true;
            _cashedCollider = null;
            Missed?.Invoke();
        }
    }
}
