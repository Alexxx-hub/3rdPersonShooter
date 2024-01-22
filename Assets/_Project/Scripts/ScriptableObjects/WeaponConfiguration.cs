using UnityEngine;

namespace _Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapons", menuName = "Weapon/ Create new weapon")]
    public class WeaponConfiguration : Item
    {
        [Header("Characteristics")]
        [SerializeField] private int _clipSize;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _range;
        [SerializeField] private float _damage;
        [SerializeField] private bool _semiAuto;
        
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _particles;

        public int ClipSize => _clipSize;
        public float FireRate => _fireRate;
        public float Range => _range;
        public float Damage => _damage;
        public bool SemiAuto => _semiAuto;
        public GameObject Prefab => _prefab;
        public GameObject Particles => _particles;
    }
}