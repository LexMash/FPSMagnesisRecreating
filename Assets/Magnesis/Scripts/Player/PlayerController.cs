using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : IDisposable
{
    private readonly GameInput _input;
    private readonly AbilityService _abilityService;
    private readonly PlayerView _view;

    public PlayerController(GameInput input, AbilityService abilityService, PlayerView view)
    {
        _input = input;
        _abilityService = abilityService;
        _view = view;

        Cursor.lockState = CursorLockMode.Locked;
        Subscribe();
    }

    private void Subscribe()
    {       
        _input.Player.Enable();
        _input.Player.SetAbility.performed += SetAbilityPerformed;
        _input.Player.AbilityStateSwitch.performed += AbilityStateSwitchPerformed;
        _input.Player.Fire.performed += FirePerformed;
        _input.Player.Move.performed += MovePerformed;
        _input.Player.Move.canceled += MoveCanceled;
        _input.Player.Look.performed += LookPerformed;
    }

    private void Unsubscribe()
    {
        _input.Player.SetAbility.performed -= SetAbilityPerformed;
        _input.Player.Fire.performed -= FirePerformed;
        _input.Player.AbilityStateSwitch.performed += AbilityStateSwitchPerformed;
        _input.Player.Move.performed -= MovePerformed;
        _input.Player.Move.canceled -= MoveCanceled;
        _input.Player.Disable();
    }

    private void MovePerformed(InputAction.CallbackContext ctx) => _view.Mover.SetDirection(ctx.ReadValue<Vector2>());
    private void MoveCanceled(InputAction.CallbackContext context) => _view.Mover.SetDirection(Vector2.zero);
    private void LookPerformed(InputAction.CallbackContext ctx) => _view.Mover.SetRotation(ctx.ReadValue<Vector2>());

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

    private void SetAbilityPerformed(InputAction.CallbackContext ctx)
    {
        _abilityService.ChooseAbilityByType(AbilityType.Magnesis);
    }

    private void ActivateAbility()
    {
        _abilityService.Activate();
    }

    private void UseAbility()
    {
        _abilityService.UseActiveAbility();
    }

    private void DeactivateAbility()
    {
        _abilityService.DeactivateActiveAbility();
    }

    public void Dispose()
    {
        Unsubscribe();
    }
}