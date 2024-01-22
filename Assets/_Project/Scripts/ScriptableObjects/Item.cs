using UnityEngine;

namespace _Project.Scripts.ScriptableObjects
{
    public enum Type
    {
        Weapon,
        Amo
    }
    
    public abstract class Item : ScriptableObject
    {
        [Header("Base Settings")]
        [SerializeField] protected int _id;
        [SerializeField] protected int _count;
        [SerializeField] protected Type _type;

        public int Id => _id;
        public int Count => _count;
        public Type Type => _type;
    }
}