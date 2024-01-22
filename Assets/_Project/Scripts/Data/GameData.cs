using System;

namespace _Project.Scripts.Data
{
    [Serializable]
    public class GameData
    {
        public int totalWins, totalLose, totalEnemyKilled;
        //--------------------------------------------------------------------------------------------------------------
        public GameData()
        {
            totalWins = 0;
            totalLose = 0;
            totalEnemyKilled = 0;
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}