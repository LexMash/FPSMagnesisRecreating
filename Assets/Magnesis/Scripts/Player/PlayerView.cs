using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [field:SerializeField] public RayCaster RayCaster {  get; private set; }
    [field:SerializeField] public Mover Mover { get; private set; }
}
