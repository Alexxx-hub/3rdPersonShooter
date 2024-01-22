using _Project.Scripts.Data;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Singletone;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Services
{
    public enum Windowstate
    {
        Win,
        Lose
    }
    
    public class EndGameWindow : MonoBehaviour, IDataProvider
    {
        private const string WinText = "Win";
        private const string LoseText = "Lose";
        
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _totalEnemyKilled;
        [SerializeField] private TextMeshProUGUI _totalWins;
        [SerializeField] private TextMeshProUGUI _totalLose;

        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _resetButton;

        private int _winCount, _loseCount, _enemyKilledCount;
        //--------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            _menuButton.onClick.AddListener(LoadMenu);
            _resetButton.onClick.AddListener(ResetGame);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(LoadMenu);
            _resetButton.onClick.RemoveListener(ResetGame);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            gameObject.SetActive(false);
        }
        //--------------------------------------------------------------------------------------------------------------
        public void ShowWindow(int enemyKilled, int wins, int lose, Windowstate windowstate)
        {
            switch ((int)windowstate)
            {
                case 0:
                    _title.text = WinText;
                    break;
                case 1:
                    _title.text = LoseText;
                    break;
            }
            
            _winCount += wins;
            _loseCount += lose;
            _enemyKilledCount += enemyKilled;
            
            _totalEnemyKilled.text = $"Enemy killed : {_enemyKilledCount}";
            _totalWins.text = $"Win: {_winCount}";
            _totalLose.text = $"Lose: {_loseCount}";
            
            gameObject.SetActive(true);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void ResetGame()
        {
            GameSignals.RestartGame();
            gameObject.SetActive(false);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void LoadMenu()
        {
            ResetGame();
            SceneLoader.Instance.LoadMainMenu();
        }
        //--------------------------------------------------------------------------------------------------------------
        public void LoadData(GameData data)
        {
            _winCount = data.totalWins;
            _loseCount = data.totalLose;
            _enemyKilledCount = data.totalEnemyKilled;
        }
        //--------------------------------------------------------------------------------------------------------------
        public void SaveData(ref GameData data)
        {
            data.totalWins = _winCount;
            data.totalLose = _loseCount;
            data.totalEnemyKilled = _enemyKilledCount;
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}