using System;
using System.IO;
using UnityEngine;

namespace _Project.Scripts.Data
{
    public class FileDataHandler
    {
        private string _dataDirPath = "";
        private string _dataFileName = "";
        //--------------------------------------------------------------------------------------------------------------
        public FileDataHandler(string dataDirPath, string dataFileName)
        {
            _dataDirPath = dataDirPath;
            _dataFileName = dataFileName;
        }
        //--------------------------------------------------------------------------------------------------------------
        public GameData Load()
        {
            string fullPath = Path.Combine(_dataDirPath, _dataFileName);
            GameData loadedData = null;

            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";

                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader streamReader = new StreamReader(stream))
                        {
                            dataToLoad = streamReader.ReadToEnd();
                        }
                    }

                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Erro when trying to load data to file: " + fullPath + "\n" + e);
                }
            }

            return loadedData;
        }
        //--------------------------------------------------------------------------------------------------------------
        public void Save(GameData data)
        {
            string fullPath = Path.Combine(_dataDirPath, _dataFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                string dataToStore = JsonUtility.ToJson(data, true);

                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter streamWriter = new StreamWriter(stream))
                    {
                        streamWriter.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Erro when trying to save data to file: " + fullPath + "\n" + e);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}