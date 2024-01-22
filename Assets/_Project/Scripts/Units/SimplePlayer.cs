using _Project.Scripts.Interfaces;
using _Project.Scripts.Services;
using _Project.Scripts.StateMachines;
using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.Units
{
    public class SimplePlayer : UnitBase, IResetObject
    {
        [HideInInspector] public bool _isReloading;
        
        [SerializeField] private Transform _playerAimTarget;
        [SerializeField] private RigSystem _rigSystem;
        
        private Vector3 _direction;
        
        private CharacterController _characterController;
        private PlayerInputsService _playerInputsService;
        private InventoryService _inventoryService;
        private AimService _aimService;
        private PlayerWeapon _playerWeapon;

        private IdleState _idleState;
        private WalkState _walkState;
        private JumpState _jumpState;
        private RunState _runState;
        private AimState _aimState;
        private ReloadState _reloadState;
        private ShootingState _shootingState;

        public IdleState IdleState => _idleState;
        public WalkState WalkState => _walkState;
        public JumpState JumpState => _jumpState;
        public RunState RunState => _runState;
        public AimState AimState => _aimState;
        public ReloadState ReloadState => _reloadState;
        public ShootingState ShootingState => _shootingState;
        
        public Transform PlayerAimTarget => _playerAimTarget;
        public RigSystem RigSystem => _rigSystem;
        public AimService AimService => _aimService;
        //--------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _playerInputsService = GetComponent<PlayerInputsService>();
            _aimService = GetComponent<AimService>();
            _inventoryService = GetComponent<InventoryService>();
            _hpBarService = GetComponent<HPBarService>();

            _playerWeapon = GetComponent<PlayerWeapon>();
            _playerWeapon.Construct(_inventoryService);
            
            _idleState = new IdleState(this, _animator, _playerInputsService, _playerWeapon);
            _walkState = new WalkState(this, _animator, _playerInputsService);
            _jumpState = new JumpState(this, _animator, _playerInputsService);
            _runState = new RunState(this, _animator, _playerInputsService);
            _aimState = new AimState(this, _animator, _playerInputsService);
            _reloadState = new ReloadState(this, _animator, _playerInputsService, _playerWeapon, _inventoryService);
            _shootingState = new ShootingState(this, _animator, _playerInputsService, _playerWeapon);
            
            SwitchState(_idleState);
            _isReloading = false;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            Move();
            _currentState.UpdateState();
        }
        //--------------------------------------------------------------------------------------------------------------
        public void SwitchState(MovementBaseState state)
        {
            _currentState = state;
            _currentState.EnterState();
        }
        //--------------------------------------------------------------------------------------------------------------
        public void WeaponIsReloaded()
        {
            _animator.SetBool("Reload", false);
            _animator.SetLayerWeight(2, 0);
            _isReloading = false;
            _playerWeapon.isEmpty = false;
            _rigSystem.RigSystemOn();
            SwitchState(_walkState);
            
            PlayerSignals.Fire(_playerWeapon.CurrentWeaponConfiguration.ClipSize, _playerWeapon.currentAmmo);
            PlayerSignals.AmmoUpdate(_inventoryService.Items[_playerWeapon.CurrentWeaponConfiguration.Id]);
        }
        //--------------------------------------------------------------------------------------------------------------
        protected override void Move()
        {
            if (IsGrounded())
            {
                _animator.SetFloat("VInput", Input.GetAxis("Vertical"));
                _animator.SetFloat("HInput", Input.GetAxis("Horizontal"));
                
                var currentSpeed = _playerInputsService.IsRunning() ? _speed * _runSpeedMultiplier : _speed;
                
                _direction = _playerInputsService.Direction * currentSpeed;

                if (_playerInputsService.IsJump())
                    _direction.y = _jumpForce;
            }

            _direction.y -= _gravity * Time.deltaTime;
            _characterController.Move(_direction * Time.deltaTime);
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void Die()
        {
            GameSignals.PlayerDie();
        }
        //--------------------------------------------------------------------------------------------------------------
        public void Reset()
        {
            _animator.SetBool("Reload", false);
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", false);
            _animator.SetBool("Jump", false);
            _animator.SetBool("Aim", false);
            _animator.SetBool("Reload", false);
            
            _animator.SetLayerWeight(1, 0);
            _animator.SetLayerWeight(2, 0);
            _animator.SetLayerWeight(3, 0);
            
            _isReloading = false;
            _playerWeapon.isEmpty = false;
            _rigSystem.RigSystemOn();
            
            SwitchState(_idleState);

            _currentHealth = _maxHealth;

            _playerWeapon.currentAmmo = 0;
            _playerWeapon.isEmpty = true;

            _inventoryService.Reset();
            _hpBarService.ResetBar();
        }

        public void WriteDefaultState()
        {
            
        }
    }
}