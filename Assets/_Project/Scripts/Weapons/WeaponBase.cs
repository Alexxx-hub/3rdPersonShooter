using _Project.Scripts.Interfaces;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Weapons
{
    public class WeaponBase : MonoBehaviour
    {
        [SerializeField] private WeaponConfiguration _weaponConfiguration;
        
        [SerializeField] private float _bulletVelocity;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _barrelPosition;
        [SerializeField] private LayerMask _mask;

        private float _fireRateTimer;
        
        public WeaponConfiguration WeaponConfiguration => _weaponConfiguration;
        //--------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            _fireRateTimer = _weaponConfiguration.FireRate;
        }
        //--------------------------------------------------------------------------------------------------------------
        private bool ShouldFire()
        {
            _fireRateTimer += Time.deltaTime;
            if (_fireRateTimer < _weaponConfiguration.FireRate) return false;
            return true;
        }
        //--------------------------------------------------------------------------------------------------------------
        public void Fire(Transform direction)
        {
            if(!ShouldFire()) return;
            _fireRateTimer = 0;
            _barrelPosition.LookAt(direction);

            GameObject currBullet = Instantiate(_bullet, _barrelPosition.position, _barrelPosition.rotation);
            Rigidbody rb = currBullet.GetComponent<Rigidbody>();
            rb.AddForce(_barrelPosition.forward * _bulletVelocity, ForceMode.Impulse);

            RaycastFire();
        }
        //--------------------------------------------------------------------------------------------------------------
        private void RaycastFire()
        {
            Ray newRay = new Ray(_barrelPosition.position, _barrelPosition.forward);
            
            if (Physics.Raycast(newRay, out RaycastHit hit, _weaponConfiguration.Range, _mask))
            {
                IDestructable victim = hit.transform.GetComponent<IDestructable>();

                if (victim != null)
                {
                    victim.TakeDamage(_weaponConfiguration.Damage);
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}