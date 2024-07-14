using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace TwinMemoryPuzzle.Scripts.GameData
{
    public class SaveGameEventArgs : EventArgs
    {
        public GameData GameSavedData { get; set; }
        // public string FilePath { get; set; }
    }

    public class GameCardSaveLoadData : MonoBehaviour
    {
        public event EventHandler<SaveGameEventArgs> OnGameSavedDataEventHandler;

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
            
            SavePath(gameData,"savegame.json");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                SaveGame(new GameData());
            }
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

        private string GetSavingPath(string filename)
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
        public List<CardData> CardDatas = new List<CardData>();
    }
    [Serializable]
    public class CardData
    {
        public int ID;
        public bool IsShow;
        public bool IsMatch;
    }
}