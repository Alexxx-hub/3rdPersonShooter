using _Project.Scripts.Interfaces;
using _Project.Scripts.Services;
using _Project.Scripts.StateMachines;
using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.Units
{
    public abstract class UnitBase : MonoBehaviour, IDestructable
    {
        [Header("Base settings")]
        [SerializeField] protected float _maxHealth;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _runSpeedMultiplier;
        [SerializeField] protected float _jumpForce;
        
        [Header("Physics settings")]
        [SerializeField] protected float _gravity;
        [SerializeField] private float _groundOffset;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundMask;

        protected float _currentHealth;
        
        protected Animator _animator;
        protected WeaponBase _weapon;
        protected MovementBaseState _currentState;
        protected HPBarService _hpBarService;

        public float MaxHealth => _maxHealth;

        public virtual float CurrentHealth
        {
            get
            {
                return _currentHealth;
            }

            set
            {
                _currentHealth = value;
                _hpBarService.UpdateBar(_currentHealth, _maxHealth);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            _currentHealth = _maxHealth;
        }
        //--------------------------------------------------------------------------------------------------------------
        protected abstract void Move();
        //--------------------------------------------------------------------------------------------------------------
        public bool IsGrounded()
        {
            var playerPos = transform.position;
            var spherePos = new Vector3(playerPos.x, playerPos.y - _groundOffset, playerPos.z);
            if (Physics.CheckSphere(spherePos, _groundCheckRadius, _groundMask))
            {
                return true;
            }

            return false;
        }
        //--------------------------------------------------------------------------------------------------------------
        public virtual void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            if(CurrentHealth <= 0) Die();
        }
        //--------------------------------------------------------------------------------------------------------------
        public virtual void Die()
        {
            gameObject.SetActive(false);
            GameSignals.EnemyDie();
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}