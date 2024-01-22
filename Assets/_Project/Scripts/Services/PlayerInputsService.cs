using System;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class PlayerInputsService : MonoBehaviour
    {
        public event Action<int> onChangeWeapon;
        public event Action onDropWeapon;

        private Vector3 _direction;

        public Vector3 Direction => _direction;
        //--------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            _direction = GetMovementDirection();
            ChangeWeapon();
            Drop();
        }
        //--------------------------------------------------------------------------------------------------------------
        private Vector3 GetMovementDirection()
        {

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            

            Vector3 movement = transform.forward * vertical + transform.right * horizontal;

            return movement.normalized;
        }
        //--------------------------------------------------------------------------------------------------------------
        public bool IsRunning()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }
        //--------------------------------------------------------------------------------------------------------------
        public bool IsJump()
        {
            return Input.GetKey(KeyCode.Space);
        }
        //--------------------------------------------------------------------------------------------------------------
        public Vector2 MouseAxis()
        {
            return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        }
        //--------------------------------------------------------------------------------------------------------------
        public bool IsAim()
        {
            if (Input.GetMouseButton(1))
            {
                return true;
            }

            return false;
        }
        //--------------------------------------------------------------------------------------------------------------
        public bool IsAutoFire()
        {
            if (Input.GetMouseButton(0))
            {
                return true;
            }

            return false;
        }
        //--------------------------------------------------------------------------------------------------------------
        public bool IsOneShootFire()
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }

            return false;
        }
        //--------------------------------------------------------------------------------------------------------------
        public bool IsReload()
        {
            if (Input.GetKeyDown(KeyCode.R)) return true;

            return false;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Drop()
        {
            if (Input.GetKeyDown(KeyCode.Q)) onDropWeapon?.Invoke();
        }
        //--------------------------------------------------------------------------------------------------------------
        private void ChangeWeapon()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) onChangeWeapon?.Invoke(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) onChangeWeapon?.Invoke(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) onChangeWeapon?.Invoke(2);
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}