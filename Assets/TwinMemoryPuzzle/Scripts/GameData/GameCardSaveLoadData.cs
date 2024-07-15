using System;
using System.Collections.Generic;
using System.IO;
using TwinMemoryPuzzle.Scripts.Constant;
using UnityEngine;


namespace TwinMemoryPuzzle.Scripts.GameData
{
    public class SaveGameEventArgs : EventArgs
    {
        public GameData GameSavedData { get; set; }
        // public string FilePath { get; set; }
    }
    public delegate void OnLoadGame(GameData _gameData);
    public class GameCardSaveLoadData : MonoBehaviour
    {
        public event EventHandler<SaveGameEventArgs> OnGameSavedDataEventHandler;

        public static GameCardSaveLoadData instance;
        
        public event OnLoadGame OnLoadGameUpdate;
        private void Awake()
        {
            instance = this;
        }

        // Method to invoke the event
        public void OnGameSaved(SaveGameEventArgs e)
        {
            OnGameSavedDataEventHandler?.Invoke(this, e);
            Debug.Log("SceneName : " + e.GameSavedData.CurrentLevelIndex);
            Debug.Log("Row : " + e.GameSavedData.RowGridLayout);
            Debug.Log("Col : " + e.GameSavedData.ColGridLayout);
            Debug.Log("Cards : " + e.GameSavedData.CardDatas.Count);
            Debug.Log("Score : " + e.GameSavedData.Score);
            Debug.Log("Match : " + e.GameSavedData.MatchScore);
            Debug.Log("Turn : " + e.GameSavedData.TurnScore);
            
            GameData gameData = e.GameSavedData;
            
            SavePath(gameData,GlobalConstant.FILE_SAVE_GAME_NAME);
        }
        
        public void SaveGame(GameData gameData)
        {
            OnGameSaved(new SaveGameEventArgs {GameSavedData = gameData});
        }

        public void SavePath(GameData gameData, string filename)
        {
            string path = GetSavingPath(filename);
            string json = JsonUtility.ToJson(gameData);
            File.WriteAllText(path, json);
            Debug.Log($"{path}");
        }

        public string GetSavingPath(string filename)
        {
            string directory;

        #if UNITY_ANDROID
                    directory = Application.persistentDataPath;
        #elif UNITY_IOS
                    directory = Application.persistentDataPath;
        #else
                    directory = Application.dataPath;
        #endif
            return Path.Combine(directory, filename);
        }
        public GameData LoadGame(string filename)
        {
            string path = GetSavingPath(filename);
        
            if (!File.Exists(path))
            {
                Debug.LogError("Save file not found!");
                return null;
            }

            string json = File.ReadAllText(path);
            GameData loadedData = JsonUtility.FromJson<GameData>(json);
            OnLoadGameUpdate?.Invoke(loadedData);
            return loadedData;
        }
    }

    [System.Serializable]
    public class GameData
    {
        public int CurrentLevelIndex;
        public int RowGridLayout;
        public int ColGridLayout;
        public int Score;
        public int MatchScore;
        public int TurnScore;
        public int MultiplyCombo;
        public List<CardData> CardDatas = new List<CardData>();
    }
    [Serializable]
    public class CardData
    {
        public int ID;
        public bool IsShow;
        public bool IsMatch;
        public string PositionName;
    }
}