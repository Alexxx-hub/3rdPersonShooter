using _Project.Scripts.Services;
using _Project.Scripts.Units;
using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.StateMachines
{
    public class ReloadState : MovementBaseState
    {
        private float _exitSpeed = 2.2f;
        
        private PlayerWeapon _playerWeapon;
        private InventoryService _inventoryService;
        
        public ReloadState(SimplePlayer player, Animator animator, PlayerInputsService playerInputsService, PlayerWeapon playerWeapon, InventoryService inventoryService) 
            : base(player, animator, playerInputsService)
        {
            _playerWeapon = playerWeapon;
            _inventoryService = inventoryService;
        }

        public override async void EnterState()
        {
            if (CanReload())
            {
                _playerWeapon.currentAmmo = _inventoryService.GetAmmo(_playerWeapon.CurrentWeaponConfiguration, _playerWeapon.currentAmmo);
                _animator.SetBool("Reload", true);
                _player.RigSystem.RigSystemOff();
                await EnterAnimatorLayer(2, _exitSpeed);
                _player._isReloading = true;
                ExitState(_player.IdleState);
            }
            else
            {
                _animator.SetBool("Reload", false);
                ExitState(_player.IdleState);
            }
        }

        public override void UpdateState()
        {
            
        }

        public override  void ExitState(MovementBaseState state)
        {
            _player.SwitchState(state);
        }

        private bool CanReload()
        {
            if (_playerWeapon.currentAmmo == _playerWeapon.CurrentWeaponConfiguration.ClipSize) return false;
            return _inventoryService.AmmoExist(_playerWeapon.CurrentWeaponConfiguration);
        }
    }
}