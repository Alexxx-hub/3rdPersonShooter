using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace _Project.Scripts.Units
{
    public class RigSystem : MonoBehaviour
    {
        [SerializeField] private Rig _rig;
        //--------------------------------------------------------------------------------------------------------------
        public void RigSystemOff()
        {
            _rig.weight = 0;
        }
        //--------------------------------------------------------------------------------------------------------------
        public void RigSystemOn()
        {
            _rig.weight = 1;
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}