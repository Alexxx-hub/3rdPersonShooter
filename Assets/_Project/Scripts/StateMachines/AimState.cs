using _Project.Scripts.Services;
using _Project.Scripts.Units;
using UnityEngine;

namespace _Project.Scripts.StateMachines
{
    public class AimState : MovementBaseState
    {
        private float _exitSpeed = 2.2f;
        
        public AimState(SimplePlayer player, Animator animator, PlayerInputsService playerInputsService) : base(player, animator, playerInputsService)
        {
            
        }
        //--------------------------------------------------------------------------------------------------------------
        public override async void EnterState()
        {
            _animator.SetBool("Aim", true);
            await EnterAnimatorLayer(1, _exitSpeed);
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void UpdateState()
        {
            if (_playerInputsService.IsAutoFire()) ExitState(_player.ShootingState);
            if (_playerInputsService.IsJump()) ExitState(_player.JumpState);
            if (!_playerInputsService.IsAim())
            {
                if (_playerInputsService.Direction.magnitude <= 0.1f) ExitState(_player.IdleState);
                else ExitState(_player.WalkState);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public override async void ExitState(MovementBaseState state)
        {
            _animator.SetBool("Aim", false);
            await ExitAnimatorLayer(1, _exitSpeed);
            _player.SwitchState(state);
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}