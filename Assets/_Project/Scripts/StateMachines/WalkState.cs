using _Project.Scripts.Services;
using _Project.Scripts.Units;
using UnityEngine;

namespace _Project.Scripts.StateMachines
{
    public class WalkState : MovementBaseState
    {

        public WalkState(SimplePlayer player, Animator animator, PlayerInputsService playerInputsService) : base(player, animator, playerInputsService)
        {
            
        }

        public override void EnterState()
        {
            _animator.SetBool("Walking", true);
        }

        public override void UpdateState()
        {
            if (!_player._isReloading)
            {
                if (_playerInputsService.IsAutoFire()) ExitState(_player.ShootingState);
                if(_playerInputsService.IsRunning() && !_playerInputsService.IsAim()) ExitState(_player.RunState);
                else if (_playerInputsService.IsJump()) ExitState(_player.JumpState);
               //else if(_playerInputsService.IsAim()) _player.SwitchState(_player.AimState);
                else if(_playerInputsService.IsReload()) _player.SwitchState(_player.ReloadState);
                else if (_playerInputsService.Direction.magnitude <= 0.1f) ExitState(_player.IdleState);
            }

        }

        public override void ExitState(MovementBaseState state)
        {
            _animator.SetBool("Walking", false);
            _player.SwitchState(state);
        }
    }
}