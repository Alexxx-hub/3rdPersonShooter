using UnityEngine;
using TMPro;

namespace _Project.Scripts.Services
{
    public class HUDService : MonoBehaviour
    {
        [Header("Level Stats")]
        [SerializeField] private TextMeshProUGUI _enemyCounter;
        [SerializeField] private TextMeshProUGUI _winCounter;
        [SerializeField] private TextMeshProUGUI _loseCounter;
        
        [Header("WeaponStats")]
        [SerializeField] private TextMeshProUGUI _ammoCount;
        [SerializeField] private TextMeshProUGUI _clipStat;

        private int _enemyMaxCount;
        //--------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            PlayerSignals.onFire += UpdateClipStat;
            PlayerSignals.onAmmoUpdate += UpdateAmmoStat;
        }
        //--------------------------------------------------------------------------------------------------------------
        private void OnDisable()
        {
            PlayerSignals.onFire -= UpdateClipStat;
            PlayerSignals.onAmmoUpdate -= UpdateAmmoStat;
        }
        //--------------------------------------------------------------------------------------------------------------
        public void Init(int enemyCount)
        {
            _enemyMaxCount = enemyCount;
            _winCounter.text = $"Wins : 0";
            _loseCounter.text = $"Lose : 0";
            _enemyCounter.text = $"Enemy : {_enemyMaxCount}/{_enemyMaxCount}";
            _ammoCount.text = $"0";
            _clipStat.text = $"0 / 0";
        }
        //--------------------------------------------------------------------------------------------------------------
        public void UpdateEnemyCount(int enemyCurrentCount)
        {
            _enemyCounter.text = $"Enemy : {enemyCurrentCount}/{_enemyMaxCount}";
        }
        //--------------------------------------------------------------------------------------------------------------
        public void UpdateWins(int wins)
        {
            _winCounter.text = $"Wins : {wins}";
        }
        //--------------------------------------------------------------------------------------------------------------
        public void UpdateLose(int lose)
        {
            _loseCounter.text = $"Lose : {lose}";
        }
        //--------------------------------------------------------------------------------------------------------------
        public void ResetHUD()
        {
            _enemyCounter.text = $"Enemy : {_enemyMaxCount}/{_enemyMaxCount}";
            _winCounter.text = $"Wins : 0";
            _loseCounter.text = $"Lose : 0";
            _ammoCount.text = $"0";
            _clipStat.text = $"0 / 0";
        }
        //--------------------------------------------------------------------------------------------------------------
        private void UpdateAmmoStat(int ammo)
        {
            _ammoCount.text = $"{ammo}";
        }
        //--------------------------------------------------------------------------------------------------------------
        private void UpdateClipStat(int clipSize, int currentAmo)
        {
            _clipStat.text = $"{currentAmo} / {clipSize}";
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}