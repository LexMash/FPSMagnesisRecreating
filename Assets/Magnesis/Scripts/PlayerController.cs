using System;
using UnityEngine.InputSystem;

public class PlayerController : IDisposable
{
    private readonly GameInput _input;
    private readonly AbilityService _abilityService;
    private readonly PlayerView _view;

    private AbilityType _currentAbility = AbilityType.None;

    public PlayerController(GameInput input, AbilityService abilityService, PlayerView view)
    {
        _input = input;
        _abilityService = abilityService;
        _view = view;

        Subscribe();
    }

    private void Subscribe()
    {
        _input.Player.Enable();
        _input.Player.SetAbility.performed += SetAbilityPerformed;
        _input.Player.AbilityStateSwitch.performed += AbilityStateSwitchPerformed;
        _input.Player.Fire.performed += FirePerformed;
    }

    private void Unsubscribe()
    {
        _input.Player.SetAbility.performed -= SetAbilityPerformed;
        _input.Player.Fire.performed -= FirePerformed;
        _input.Player.AbilityStateSwitch.performed += AbilityStateSwitchPerformed;
        _input.Player.Disable();
    }

    private void AbilityStateSwitchPerformed(InputAction.CallbackContext obj)
    {
        if (_abilityService.AbilitySelected)
        {
            Action action = _abilityService.AbilityIsActive ? DeactivateAbility : ActivateAbility;
            action.Invoke();
        }
    }

    private void FirePerformed(InputAction.CallbackContext context)
    {
        if (_abilityService.AbilityIsActive)
        {
            UseAbility();
        }
    }

    private void SetAbilityPerformed(InputAction.CallbackContext obj)
    {
        SetAbility(AbilityType.Magnesis);
    }

    private void SetAbility(AbilityType type) => _currentAbility = type;
    private void ActivateAbility() => _abilityService.Activate(_currentAbility);
    private void UseAbility() => _abilityService.UseActiveAbility();
    private void DeactivateAbility() => _abilityService.DeactivateActiveAbility();

    public void Dispose()
    {
        Unsubscribe();
    }
}