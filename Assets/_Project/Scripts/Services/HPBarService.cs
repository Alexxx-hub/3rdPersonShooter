using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Services
{
    public class HPBarService : MonoBehaviour
    {
        [SerializeField] private Image _hpBar;

        public void UpdateBar(float currentHelth, float maxHealth)
        {
            _hpBar.fillAmount = currentHelth / maxHealth;
        }
        
        public void ResetBar()
        {
            _hpBar.fillAmount = 1f;
        }
    }
}