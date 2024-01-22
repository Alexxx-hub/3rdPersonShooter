using System;
using System.Collections.Generic;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;
using Type = _Project.Scripts.ScriptableObjects.Type;

namespace _Project.Scripts.Services
{
    public class InventoryService : MonoBehaviour
    {
        [SerializeField] private WeaponConfiguration[] _slots;

        private int _currentActiveSlot;
        private Dictionary<int, int> _items;

        public Dictionary<int, int> Items => _items;
        //--------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            Initialize();
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Initialize()
        {
            _currentActiveSlot = 0;
            _items = new Dictionary<int, int>();
            _slots = new WeaponConfiguration[3];
        }
        //--------------------------------------------------------------------------------------------------------------
        private int HasFreeSlot()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] == null) return i;
            }

            return -1;
        }
        //--------------------------------------------------------------------------------------------------------------
        public void AddItem(Item item)
        {
            if (item.Type == Type.Weapon)
            {
                int freeSlot = HasFreeSlot();
                if (freeSlot != -1)
                {
                    _slots[freeSlot] = item as WeaponConfiguration;;
                }
            }
            else
            {
                if (_items.ContainsKey(item.Id))
                {
                    _items[item.Id] += item.Count;
                }
                else
                {
                    _items.Add(item.Id, item.Count);
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public void DropWeapon()
        {
            _slots[_currentActiveSlot] = null;
        }
        //--------------------------------------------------------------------------------------------------------------
        public WeaponConfiguration GetWeapon(int id)
        {
            _currentActiveSlot = id;
            return _slots[_currentActiveSlot];
        }
        //--------------------------------------------------------------------------------------------------------------
        public int GetAmmo(WeaponConfiguration configuration, int currentAmmo)
        {
            int ammoToReload;
            
            if (_items[configuration.Id] >= currentAmmo)
            { 
                ammoToReload = configuration.ClipSize - currentAmmo;
                _items[configuration.Id] -= ammoToReload;
                ammoToReload = configuration.ClipSize;
            }
            else if(_items[configuration.Id] > 0)
            {
                if (_items[configuration.Id] + currentAmmo > configuration.ClipSize)
                { 
                    ammoToReload = _items[configuration.Id] + currentAmmo - configuration.ClipSize;
                    _items[configuration.Id] = ammoToReload;
                    ammoToReload = configuration.ClipSize;
                }
                else
                { 
                    ammoToReload = _items[configuration.Id];
                    _items.Remove(configuration.Id);
                    ammoToReload = currentAmmo + ammoToReload;
                }
            }
            else
            {
                ammoToReload = 0;
            }

            return ammoToReload;
        }
        //--------------------------------------------------------------------------------------------------------------
        public bool AmmoExist(WeaponConfiguration configuration)
        {
            return _items.ContainsKey(configuration.Id);
        }
        //--------------------------------------------------------------------------------------------------------------
        public void Reset()
        {
            _currentActiveSlot = 0; 
            Array.Clear(_slots, 0, 3);
            _items.Clear();
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}