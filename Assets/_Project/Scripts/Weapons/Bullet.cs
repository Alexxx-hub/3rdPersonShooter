using UnityEngine;

namespace _Project.Scripts.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _timeToDestroy;

        private float _timer;
        //--------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            _timer = 0;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            _timer += Time.deltaTime;
            if(_timer >= _timeToDestroy) Destroy(gameObject);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}