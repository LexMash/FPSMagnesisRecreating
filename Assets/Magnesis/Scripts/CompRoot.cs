using System;
using System.Collections.Generic;
using UnityEngine;

public class CompRoot : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private MagnesisAbilityConfig _magnesisConfig;

    private List<IDisposable> _disposables = new();
    private UpdateService _updateService;
    private GameInput _input;
    private MagnesisAbility _magnesisAbility;
    private AbilityService _abilityService;
    private PlayerController _playerController;


    private void Awake()
    {
        _updateService = new();
        _disposables.Add(_updateService);

        _input = new();
        _disposables.Add(_input);

        _magnesisAbility = new(_magnesisConfig, _playerView);
        _abilityService = new AbilityService(_updateService, new []{ _magnesisAbility });
        _disposables.Add(_abilityService);

        _playerController = new(_input, _abilityService, _playerView);
        _disposables.Add(_playerController);
    }

    private void FixedUpdate() => _updateService.FixedUpdate(Time.fixedDeltaTime);
    private void Update() => _updateService.Update(Time.deltaTime);
    private void LateUpdate() => _updateService.LateUpdate(Time.deltaTime);

    private void OnDestroy()
    {
        for(int i = _disposables.Count; i > 0; i--)
            _disposables[i].Dispose();

        _updateService = null;
    }
}
