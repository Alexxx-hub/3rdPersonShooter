using _Project.Scripts.Interfaces;
using _Project.Scripts.Services;
using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.Units
{
    public class SimpleEnemy : UnitBase, IResetObject
    {
        [SerializeField] private float _detectionRadius;
        [SerializeField] private LayerMask _playerMask;
        [SerializeField] private RigSystem rigSystem;

        private bool _isPlayerDetected;
        private SimplePlayer _player;

        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;
        
        public override float CurrentHealth
        {
            get => _currentHealth;

            set
            {
                _currentHealth = value;
                _isPlayerDetected = true;
                _hpBarService.UpdateBar(_currentHealth, _maxHealth);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public void Construct(SimplePlayer player)
        {
            _player = player;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            _weapon = GetComponent<WeaponBase>();
            _hpBarService = GetComponent<HPBarService>();
            rigSystem.RigSystemOff();
            WriteDefaultState();
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (_isPlayerDetected)
            {
                RotatePlayer();
                _weapon.Fire(_player.PlayerAimTarget);
            }
            else
            {
                CheckDistanceToPlayer();
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        protected override void Move()
        {
            
        }
        //--------------------------------------------------------------------------------------------------------------
        private void CheckDistanceToPlayer()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) > _detectionRadius)
            {
                _isPlayerDetected = false;
                rigSystem.RigSystemOff();
            }
            else
            {
                _isPlayerDetected = true;
                rigSystem.RigSystemOn();
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        private void RotatePlayer()
        {
            Vector3 direction = _player.transform.position;
            direction.y = transform.position.y;
            transform.LookAt(direction);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
        //--------------------------------------------------------------------------------------------------------------
        public void Reset()
        {
            _currentHealth = _maxHealth;
            _isPlayerDetected = false;
            _hpBarService.ResetBar();

            gameObject.SetActive(false);
            
            transform.position = _defaultPosition;
            transform.rotation = _defaultRotation;
            
            gameObject.SetActive(true);
        }
        //--------------------------------------------------------------------------------------------------------------
        public void WriteDefaultState()
        {
            _defaultPosition = transform.position;
            _defaultRotation = transform.rotation;
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}