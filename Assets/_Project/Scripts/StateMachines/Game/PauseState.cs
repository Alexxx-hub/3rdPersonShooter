using UnityEngine;

namespace _Project.Scripts.StateMachines.Game
{
    public class PauseState : GameStateBase
    {
        //--------------------------------------------------------------------------------------------------------------
        public override void EnterState()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void ExitState()
        {

        }
        //--------------------------------------------------------------------------------------------------------------
    }
}