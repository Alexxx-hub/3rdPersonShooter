using _Project.Scripts.Services;
using _Project.Scripts.Units;
using UnityEngine;

namespace _Project.Scripts.StateMachines
{
    public class RunState : MovementBaseState
    {
        public RunState(SimplePlayer player, Animator animator, PlayerInputsService playerInputsService) : base(player, animator, playerInputsService)
        {
            
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void EnterState()
        {
            _animator.SetBool("Running", true);
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void UpdateState()
        {
            if (_playerInputsService.IsJump()) ExitState(_player.JumpState);
            if(!_playerInputsService.IsRunning() /*|| _playerInputsService.IsAim()*/) ExitState(_player.WalkState);
            else if (_playerInputsService.Direction.magnitude <= 0.1f) ExitState(_player.IdleState);
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void ExitState(MovementBaseState state)
        {
            _animator.SetBool("Running", false);
            _player.SwitchState(state);
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}