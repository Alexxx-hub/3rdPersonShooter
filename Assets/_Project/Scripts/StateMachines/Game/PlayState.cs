using System.Collections.Generic;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.StateMachines.Game
{
    public class PlayState : GameStateBase
    {
        private IResetObject[] _resetObjects;
        private List<IResetObject> _resetObjectsList;
        //--------------------------------------------------------------------------------------------------------------
        public PlayState(IResetObject[] resetObjects)
        {
            _resetObjects = resetObjects;
            _resetObjectsList = new List<IResetObject>();
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void EnterState()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            foreach (var obj in _resetObjects)
            {
                obj.Reset();
            }

            foreach (var obj in _resetObjectsList)
            {
                obj.Reset();
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public override void ExitState()
        {

        }
        //--------------------------------------------------------------------------------------------------------------
        public void AddResetableObj(IResetObject resetObject)
        {
            _resetObjectsList.Add(resetObject);
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}