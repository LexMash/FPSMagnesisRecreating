public interface IAbility
{
    AbilityType Type { get; }

    bool IsActive { get; }
    void Activate();
    void Deactivate();
    void Use();
    void UpdateState(float deltaTime);
}
