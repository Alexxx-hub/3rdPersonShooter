using _Project.Scripts.StateMachines.Game;
using _Project.Scripts.Units;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class LevelService : MonoBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] private SimplePlayer _player;
        [SerializeField] private Transform _playerInitPosition;

        [Header("Level Settings")] 
        [SerializeField] private GameObject _riflePrefab;
        [SerializeField] private GameObject _minigunPrefab;
        [SerializeField] private GameObject _rifleAmmoPrefab;
        [SerializeField] private GameObject _minigunAmmoPrefab;
        [SerializeField] private Transform[] _riffleSpawns;
        [SerializeField] private Transform[] _minigunSpawns;
        [SerializeField] private Transform[] _riffleAmmoSpawns;
        [SerializeField] private Transform[] _minigunAmmoSpawns;
        
        [SerializeField] private int _enemyMaxCount;
        [SerializeField] private HUDService _hudService;
        [SerializeField] private EndGameWindow _endGameWindow;
        [SerializeField] private SimpleEnemy[] _enemies;
        [SerializeField] private EquipmentService _equipmentService;
        
        private int _winCount, _loseCount, _enemyCountLeft, _enemyKilledCount;

        private GameStateMachine _stateMachine;
        private PlayState _playState;
        private PauseState _pauseState;
        //--------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            GameSignals.onEnemyKilled += AddEnemyKill;
            GameSignals.onPlayerDie += AddLose;
            GameSignals.onGameRestart += ResetLevel;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void OnDisable()
        {
            GameSignals.onEnemyKilled -= AddEnemyKill;
            GameSignals.onPlayerDie -= AddLose;
            GameSignals.onGameRestart -= ResetLevel;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            InitializeLevel();
            _hudService.Init(_enemyMaxCount);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void InitializeLevel()
        {
            _playState = new PlayState(_enemies);
            _playState.AddResetableObj(_equipmentService);
            _playState.AddResetableObj(_player);
            _pauseState = new PauseState();
            _stateMachine = new GameStateMachine(_playState);
            
            foreach (var enemy in _enemies)
            {
                enemy.Construct(_player);
            }
            
            ResetLevel();
        }
        //--------------------------------------------------------------------------------------------------------------
        private void ResetLevel()
        {
            _stateMachine.SwitchState(_playState);
            
            // reset level stats
            _enemyCountLeft = _enemyMaxCount;
            _enemyKilledCount = _winCount = _loseCount = 0;
            
            _hudService.ResetHUD();
            
            SpawnLevelItems();
            ResetPlayer();
        }
        //--------------------------------------------------------------------------------------------------------------
        private void AddEnemyKill()
        {
            _enemyKilledCount++;
            _enemyCountLeft--;
            _hudService.UpdateEnemyCount(_enemyCountLeft);

            if (_enemyCountLeft == 0)
            {
                AddWins();
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        private void AddWins()
        {
            _winCount++;
            _hudService.UpdateWins(_winCount);
            _endGameWindow.ShowWindow(_enemyKilledCount, _winCount, _loseCount, Windowstate.Win);
            _stateMachine.SwitchState(_pauseState);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void AddLose()
        {
            _loseCount++;
            _hudService.UpdateLose(_loseCount);
            _endGameWindow.ShowWindow(_enemyKilledCount, _winCount, _loseCount, Windowstate.Lose);
            _player.gameObject.SetActive(false);
            _stateMachine.SwitchState(_pauseState);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void ResetPlayer()
        {
            _player.gameObject.SetActive(false);
            
            _player.transform.position = _playerInitPosition.position;
            _player.transform.rotation = _playerInitPosition.rotation;

            _player.gameObject.SetActive(true);
        }
        //--------------------------------------------------------------------------------------------------------------
        private void SpawnLevelItems()
        {
            for (int i = 0; i < _riffleSpawns.Length; i++)
            {
                Instantiate(_riflePrefab, _riffleSpawns[i].position, quaternion.identity);
            }
            
            for (int i = 0; i < _minigunSpawns.Length; i++)
            {
                Instantiate(_minigunPrefab, _minigunSpawns[i].position, quaternion.identity);
            }
            
            for (int i = 0; i < _riffleAmmoSpawns.Length; i++)
            {
                Instantiate(_rifleAmmoPrefab, _riffleAmmoSpawns[i].position, quaternion.identity);
            }
            
            for (int i = 0; i < _minigunAmmoSpawns.Length; i++)
            {
                Instantiate(_minigunAmmoPrefab, _minigunAmmoSpawns[i].position, quaternion.identity);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}