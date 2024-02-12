﻿using System;
using System.Collections.Generic;

public class AbilityService : IDisposable, IUpdatable
{
    private readonly Dictionary<AbilityType, IAbility> _abilitiesMap = new();
    private readonly UpdateService _updateService;
    private IAbility _currentAbility;

    public AbilityService(UpdateService updateService, IAbility[] abilities)
    {
        _updateService = updateService;

        for (int i = 0; i < abilities.Length; i++)
        {
            var ability = abilities[i];
            _abilitiesMap.Add(ability.Type, ability);
        }      
    }

    public bool AbilitySelected => _currentAbility != null;
    public bool AbilityIsActive => _currentAbility.IsActive;

    public void Activate(AbilityType type)
    {
        if(_currentAbility != null && _currentAbility.Type != type)
        {
            DeactivateActiveAbility();
        }

        _currentAbility = Get(type);
        _currentAbility.Activate();

        _updateService.Register<IUpdatable>(this);
    }

    public void DeactivateActiveAbility()
    {
        _updateService.Unregister<IUpdatable>(this);

        _currentAbility.Deactivate();
        _currentAbility = null;       
    }

    public void UseActiveAbility()
    {
        _currentAbility.Use();
    }

    public void Dispose()
    {
        _updateService.Unregister<IUpdatable>(this);

        _currentAbility = null;
        _abilitiesMap.Clear();
    }

    public void Tick(float deltaTime) => _currentAbility.UpdateState(deltaTime);

    private IAbility Get(AbilityType abilityType)
    {
        if (_abilitiesMap.TryGetValue(abilityType, out IAbility ability))
        {
            return ability;
        }
        else
        {
            throw new ArgumentException($"Ability with type {abilityType} - not contains in ability map");
        }
    }
}