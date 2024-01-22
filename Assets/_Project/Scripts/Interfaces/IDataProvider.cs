using _Project.Scripts.Data;

namespace _Project.Scripts.Interfaces
{
    public interface IDataProvider
    {
        public void LoadData(GameData data);
        public void SaveData(ref GameData data);
    }
}