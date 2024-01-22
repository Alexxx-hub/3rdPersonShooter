using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Singletone
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }
        //--------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            
            DontDestroyOnLoad(this);
        }
        //--------------------------------------------------------------------------------------------------------------
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
        //--------------------------------------------------------------------------------------------------------------
        public void LoadLevel()
        {
            SceneManager.LoadScene(1);
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}