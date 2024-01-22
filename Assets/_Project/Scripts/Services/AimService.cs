using UnityEngine;

namespace _Project.Scripts.Services
{
    public class AimService : MonoBehaviour
    {
        [SerializeField] private float _aimSmoothSpeed;
        [SerializeField] private float _yAxisAngleClamp;
        [SerializeField] private Transform _cameraFolloPosition;
        [SerializeField] private PlayerInputsService _playerInputsService;

        [SerializeField] private GameObject _mainCamera;
        [SerializeField] private GameObject _aimCamera;

        [SerializeField] private Transform _aimPosition;
        [SerializeField] private LayerMask _aimMask;
        
        private float xAxis, yAxis;
        private Ray _direction;

        public Transform AimPosition => _aimPosition;
        public Ray Direction => _direction;
        //--------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            _aimCamera.SetActive(false);
            _mainCamera.SetActive(true);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            xAxis += _playerInputsService.MouseAxis().x;
            yAxis -= _playerInputsService.MouseAxis().y;
            yAxis = Mathf.Clamp(yAxis, -_yAxisAngleClamp, _yAxisAngleClamp);

            if (_playerInputsService.IsAim() && !_aimCamera.activeInHierarchy)
            {
                _mainCamera.SetActive(false);
                _aimCamera.SetActive(true);
            }
            else if (!_playerInputsService.IsAim() && !_mainCamera.activeInHierarchy)
            {
                _aimCamera.SetActive(false);
                _mainCamera.SetActive(true);
            }
            
            AimPositionUpdate();
        }
        //--------------------------------------------------------------------------------------------------------------
        private void LateUpdate()
        {
            _cameraFolloPosition.localEulerAngles =
                new Vector3(yAxis, _cameraFolloPosition.localEulerAngles.y, _cameraFolloPosition.localEulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
        }
        //--------------------------------------------------------------------------------------------------------------
        private Ray SetViewDirection()
        {
            Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
            return _direction = Camera.main.ScreenPointToRay(screenCentre);;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void AimPositionUpdate()
        {
            if (Physics.Raycast(SetViewDirection(), out RaycastHit hit, Mathf.Infinity, _aimMask))
            {
                _aimPosition.position =
                    Vector3.Lerp(_aimPosition.position, hit.point, _aimSmoothSpeed * Time.deltaTime);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}