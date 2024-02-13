using UnityEngine;

[CreateAssetMenu(fileName = "MagnesisConfig", menuName = "AbilitiesConfigs/MagnesisConfig")]
public class MagnesisAbilityConfig : ScriptableObject
{
    [field: SerializeField] public AbilityType Type { get; private set; }
    [field: SerializeField] public float MaxUsageDistance { get; private set; }
}