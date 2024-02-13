using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerConfig", menuName = "Player/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;
    [field: SerializeField] public float LookRotationSensitivity { get; private set; } = 1f;
    [field: SerializeField] public float TopAngleClamp { get; private set; } = 89f;
    [field: SerializeField] public float BottomAngleClamp { get; private set; } = -89f;
}
