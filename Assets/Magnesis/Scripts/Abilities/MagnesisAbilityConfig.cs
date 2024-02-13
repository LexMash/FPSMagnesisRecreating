using UnityEngine;

[CreateAssetMenu(fileName = "MagnesisConfig", menuName = "AbilitiesConfigs/MagnesisConfig")]
public class MagnesisAbilityConfig : ScriptableObject
{
    [field: SerializeField] public AbilityType Type { get; private set; }
    [field: SerializeField] public float MaxUsageDistance { get; private set; }
    [field: SerializeField] public float MaxScanObjectRadius { get; private set; } = 10f;
    [field: SerializeField] public float MaxFollowSpeedToTarget { get; private set; } = 5f;
    [field: SerializeField] public float SmoothLerp { get; private set; } = 2f;
}