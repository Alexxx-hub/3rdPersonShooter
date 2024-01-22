using _Project.Scripts.Interfaces;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Units;
using _Project.Scripts.Weapons;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class EquipmentService : MonoBehaviour, ICollector, IResetObject
    {
        [SerializeField] private Transform _pointToSpawn;
        [SerializeField] private PlayerInputsService _playerInputsService;
        [SerializeField] private InventoryService _inventoryService;
        [SerializeField] private PlayerWeapon _playerWeapon;
        [SerializeField] private SimplePlayer _player;

        private WeaponConfiguration _currentWeaponConfiguration;
        private Weapon _currentWeapon;
        //--------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            _playerInputsService.onChangeWeapon += InstantiateWeapon;
            _playerInputsService.onDropWeapon += DropWeapon;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void OnDisable()
        {
            _playerInputsService.onChangeWeapon -= InstantiateWeapon;
            _playerInputsService.onDropWeapon -= DropWeapon;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void InstantiateWeapon(int id)
        {
            if (!_playerInputsService.IsAutoFire() && !_player._isReloading)
            {
                if(_currentWeapon != null) Destroy(_currentWeapon.gameObject);
            
                _currentWeaponConfiguration = _inventoryService.GetWeapon(id);
                if (_currentWeaponConfiguration != null)
                {
                    _currentWeapon = Instantiate(_currentWeaponConfiguration.Prefab, _pointToSpawn.position,
                        quaternion.identity, _pointToSpawn).GetComponent<Weapon>();
            
                    _currentWeapon.transform.localPosition = Vector3.zero;;
                    _currentWeapon.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 90));
                
                    _playerWeapon.ChangeWeapon(_currentWeaponConfiguration, _currentWeapon);
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        private void DropWeapon()
        {
            if (_currentWeapon != null && !_playerInputsService.IsAutoFire() && !_player._isReloading)
            {
                Destroy(_currentWeapon.gameObject);
                _inventoryService.DropWeapon();
                _playerWeapon.ChangeWeapon(null, null);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public void PickUp(Item item)
        {
            _inventoryService.AddItem(item);

            if (_currentWeaponConfiguration && _inventoryService.AmmoExist(_currentWeaponConfiguration))
            {
                PlayerSignals.AmmoUpdate(_inventoryService.Items[_currentWeaponConfiguration.Id]);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public void Reset()
        {
            if(_currentWeapon != null) Destroy(_currentWeapon.gameObject);
            _currentWeaponConfiguration = null;
        }

        public void WriteDefaultState()
        {
           
        }
    }
}