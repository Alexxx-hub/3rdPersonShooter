using _Project.Scripts.Singletone;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Services
{
    public class MainMenuService : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        //--------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            _playButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(ExitGame);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(StartGame);
            _exitButton.onClick.RemoveListener(ExitGame);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void StartGame()
        {
            SceneLoader.Instance.LoadLevel();
        }
        //--------------------------------------------------------------------------------------------------------------
        private void ExitGame()
        {
            Application.Quit();
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}