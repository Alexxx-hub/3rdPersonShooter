using System;

namespace _Project.Scripts
{
    public static class GameSignals
    {
        public static event Action onEnemyKilled;
        public static event Action onPlayerDie;
        public static event Action onGameRestart;
        //--------------------------------------------------------------------------------------------------------------
        public static void EnemyDie()
        {
            onEnemyKilled?.Invoke();
        }
        //--------------------------------------------------------------------------------------------------------------
        public static void PlayerDie()
        {
            onPlayerDie?.Invoke();
        }
        //--------------------------------------------------------------------------------------------------------------
        public static void RestartGame()
        {
            onGameRestart?.Invoke();
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}