using _Project.Scripts.Services;
using _Project.Scripts.Units;
using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.StateMachines
{
    public class IdleState : MovementBaseState
    {
        private PlayerWeapon _playerWeapon;
        
        public IdleState(SimplePlayer player, Animator animator, PlayerInputsService playerInputsService, PlayerWeapon playerWeapon) : base(player, animator, playerInputsService)
        {
            _playerWeapon = playerWeapon;
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void EnterState()
        {
          
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void UpdateState()
        {
            if (_playerInputsService.Direction.magnitude >= 0.1f)
            {
                if (_playerInputsService.IsRunning()) _player.SwitchState(_player.RunState);
                else 
                    _player.SwitchState(_player.WalkState);
            }
            if (!_player._isReloading)
            {
                if(_playerInputsService.IsJump()) _player.SwitchState(_player.JumpState);
                if(_playerInputsService.IsReload()) _player.SwitchState(_player.ReloadState);
                //if(_playerInputsService.IsAim()) _player.SwitchState(_player.AimState);
                if (_playerInputsService.IsAutoFire()) _player.SwitchState(_player.ShootingState);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void ExitState(MovementBaseState state)
        {
            
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}