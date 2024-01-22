using _Project.Scripts.Interfaces;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.Weapons
{
    public class PlayerWeapon : MonoBehaviour
    {
        [HideInInspector] public int currentAmmo;
        [HideInInspector] public bool isEmpty;
        
        [SerializeField] private float _bulletVelocity;
        [SerializeField] private Transform _barrelPosition;
        [SerializeField] private LayerMask _mask;

        private float _fireRateTimer;
        private Transform _particleSpawnPosition;
        private WeaponConfiguration _currentWeaponConfiguration;
        private InventoryService _inventoryService;
        
        public WeaponConfiguration CurrentWeaponConfiguration => _currentWeaponConfiguration;
        //--------------------------------------------------------------------------------------------------------------
        private bool ShouldFire()
        {
            _fireRateTimer += Time.deltaTime;
            if (_fireRateTimer < _currentWeaponConfiguration.FireRate) return false;
            if (currentAmmo == 0)
            {
                isEmpty = true;
                return false;
            }
            return true;
        }
        //--------------------------------------------------------------------------------------------------------------
        public void Construct(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        //--------------------------------------------------------------------------------------------------------------
        public void Fire(Transform direction)
        {
            if(!ShouldFire()) return;
            _fireRateTimer = 0;
            _barrelPosition.LookAt(direction);

            GameObject currBullet = Instantiate(_currentWeaponConfiguration.Particles, _particleSpawnPosition.position, Quaternion.identity);
            Rigidbody rb = currBullet.GetComponent<Rigidbody>();
            rb.AddForce(_particleSpawnPosition.forward * _bulletVelocity, ForceMode.Impulse);

            RaycastFire();
            currentAmmo--;
            PlayerSignals.Fire(CurrentWeaponConfiguration.ClipSize, currentAmmo);
            Debug.Log(currentAmmo);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void RaycastFire()
        {
            Ray newRay = new Ray(_barrelPosition.position, _barrelPosition.forward);
            
            if (Physics.Raycast(newRay, out RaycastHit hit, _currentWeaponConfiguration.Range, _mask))
            {
                IDestructable victim = hit.transform.GetComponent<IDestructable>();

                if (victim != null)
                {
                    victim.TakeDamage(_currentWeaponConfiguration.Damage);
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public void ChangeWeapon(WeaponConfiguration configuration, Weapon weapon)
        {
            _currentWeaponConfiguration = configuration;
            if (_currentWeaponConfiguration != null)
            {
                _fireRateTimer = _currentWeaponConfiguration.FireRate;
                currentAmmo = configuration.ClipSize;
                _particleSpawnPosition = weapon.particleSpawnPosition;
                isEmpty = false;

                if (_inventoryService.AmmoExist(_currentWeaponConfiguration))
                {
                    PlayerSignals.AmmoUpdate(_inventoryService.Items[_currentWeaponConfiguration.Id]);
                }
                PlayerSignals.Fire(CurrentWeaponConfiguration.ClipSize, currentAmmo);
            }
            else
            {
                PlayerSignals.Fire(0, 0);
                PlayerSignals.AmmoUpdate(0);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public bool Exist()
        {
            if (_currentWeaponConfiguration != null) return true;
            return false;
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}