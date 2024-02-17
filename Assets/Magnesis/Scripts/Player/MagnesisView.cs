using UnityEngine;

public class MagnesisView : MonoBehaviour
{
    [field:SerializeField] public Transform Pivot { get; private set; }
    [field:SerializeField] public Joint Joint { get; private set; }
}
