using _Project.Scripts.Services;
using _Project.Scripts.Units;
using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.StateMachines
{
    public class ShootingState : MovementBaseState
    {
        private float _exitSpeed = 2.2f;

        private PlayerWeapon _playerWeapon;
        
        public ShootingState(SimplePlayer player, Animator animator, PlayerInputsService playerInputsService, PlayerWeapon playerWeapon) 
            : base(player, animator, playerInputsService)
        {
            _playerWeapon = playerWeapon;
        }

        public override void EnterState()
        {
 
        }

        public override void UpdateState()
        {
            if (_playerWeapon.Exist())
            {
                if (_playerInputsService.IsAutoFire() && !_playerWeapon.isEmpty)
                {
                    _playerWeapon.Fire(_player.AimService.AimPosition);
                    if (!_animator.GetBool("Shooting"))
                    {
                        _animator.SetBool("Shooting", true);
                        _animator.SetLayerWeight(3, 1);
                    }
                }
                else
                {
                    //if (_playerInputsService.IsAim()) ExitState(_player.AimState);
                    if(_playerInputsService.IsRunning()) ExitState(_player.RunState);
                    else if(_playerInputsService.Direction.magnitude > 0.1f) ExitState(_player.WalkState);
                    else
                    {
                        ExitState(_player.IdleState);
                    }
                }
            }
            else
            {
                ExitState(_player.IdleState);
            }
        }

        public override async void ExitState(MovementBaseState state)
        {
            _animator.SetBool("Shooting", false);
            await ExitAnimatorLayer(3, _exitSpeed);
            _player.SwitchState(state);
        }
    }
}