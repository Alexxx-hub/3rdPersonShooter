using System.Threading.Tasks;
using _Project.Scripts.Services;
using _Project.Scripts.Units;
using UnityEngine;

namespace _Project.Scripts.StateMachines
{
    public abstract class MovementBaseState
    {
        protected SimplePlayer _player;
        protected Animator _animator;
        protected PlayerInputsService _playerInputsService;
        //--------------------------------------------------------------------------------------------------------------
        protected MovementBaseState(SimplePlayer player, Animator animator, PlayerInputsService playerInputsService)
        {
            _player = player;
            _animator = animator;
            _playerInputsService = playerInputsService;
        }
        //--------------------------------------------------------------------------------------------------------------
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState(MovementBaseState state);
        //--------------------------------------------------------------------------------------------------------------
        protected async Task ExitAnimatorLayer(int index, float speed)
        {
            while (_animator.GetLayerWeight(index) != 0)
            {
               _animator.SetLayerWeight(index, _animator.GetLayerWeight(index) - Time.deltaTime * speed);

               if (_animator.GetLayerWeight(index) < 0)
               {
                   _animator.SetLayerWeight(index, 0);
               }
               
               await Task.Yield();
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        protected async Task EnterAnimatorLayer(int index, float speed)
        {
            while (_animator.GetLayerWeight(index) < 1f)
            {
                _animator.SetLayerWeight(index, _animator.GetLayerWeight(index) + Time.deltaTime * speed);

                if (_animator.GetLayerWeight(index) > 1)
                {
                    _animator.SetLayerWeight(index, 1);
                }
               
                await Task.Yield();
            }
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}