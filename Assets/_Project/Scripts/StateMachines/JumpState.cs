using _Project.Scripts.Services;
using _Project.Scripts.Units;
using UnityEngine;

namespace _Project.Scripts.StateMachines
{
    public class JumpState : MovementBaseState
    {
        public JumpState(SimplePlayer player, Animator animator, PlayerInputsService playerInputsService) : base(player, animator, playerInputsService)
        {
            
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void EnterState()
        {
            _animator.SetBool("Jump", true);
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void UpdateState()
        {
            if (_player.IsGrounded())
            {
                if(_playerInputsService.IsRunning())ExitState(_player.RunState);
                if (_playerInputsService.Direction.magnitude <= 0.1f) ExitState(_player.IdleState);
                else ExitState(_player.WalkState);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void ExitState(MovementBaseState state)
        {
            _animator.SetBool("Jump", false);
            _player.SwitchState(state);
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}