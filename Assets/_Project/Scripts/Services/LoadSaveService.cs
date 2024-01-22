using _Project.Scripts.Data;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class LoadSaveService : MonoBehaviour
    {
        [Header("File Storage Config")] 
        [SerializeField] private string _fileName;
        
        [SerializeField] private GameObject _endGameWindow;
        
        private GameData _gameData;
        private IDataProvider _dataProvider;

        private FileDataHandler _fileDataHandler;
        //--------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            GameSignals.onPlayerDie += LoadGame;
            GameSignals.onGameRestart += SaveGame;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void OnDisable()
        {
            GameSignals.onPlayerDie -= LoadGame;
            GameSignals.onGameRestart -= SaveGame;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
            _dataProvider = _endGameWindow.GetComponent<IDataProvider>();
            LoadGame();
        }
        //--------------------------------------------------------------------------------------------------------------
        private void NewGame()
        {
            _gameData = new GameData();
        }
        //--------------------------------------------------------------------------------------------------------------
        private void LoadGame()
        {
            _gameData = _fileDataHandler.Load();
            
            if (_gameData == null)
            {
                NewGame();
            }
            
            _dataProvider.LoadData(_gameData);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void SaveGame()
        {
            _dataProvider.SaveData(ref _gameData);
            
            _fileDataHandler.Save(_gameData);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void OnApplicationQuit()
        {
            SaveGame();
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}