using _Project.Scripts.Interfaces;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] private Item _item;
        public Item Item => _item;
        //--------------------------------------------------------------------------------------------------------------
        private void OnTriggerEnter(Collider collision)
        {
            ICollector collector = collision.gameObject.GetComponent<ICollector>();

            if (collector != null)
            {
                collector.PickUp(Item);
                Destroy(gameObject);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}