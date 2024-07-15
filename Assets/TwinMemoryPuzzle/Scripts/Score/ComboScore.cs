using System;
using TwinMemoryPuzzle.Scripts.GameData;
using TwinMemoryPuzzle.Scripts.Logic;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Score
{
    public class ComboScore : MonoBehaviour
    {
        private int multiplyScore ;
        [SerializeField] private GameCardMatchChecker gameCardMatchChecker;

        private void Awake()
        {
            GameCardSaveLoadData.instance.OnLoadGameUpdate += LoadScoreData;
        }
        // Start is called before the first frame update
        void Start()
        {
            GameCardSaveLoadData.instance.OnGameSavedDataEventHandler += HandleGameSaved;
            gameCardMatchChecker.OnMatchComplete += IncreaseComboMultiplyScore;
            gameCardMatchChecker.OnMatchFailure += DecreaseComboMultiplyScore;
        }

        private void HandleGameSaved(object _sender, SaveGameEventArgs _e)
        {
            _e.GameSavedData.MultiplyCombo = multiplyScore;
        }

        private void DecreaseComboMultiplyScore()
        {
            Debug.Log("MUlti: " +multiplyScore);
            multiplyScore = 0;
        }

        private void IncreaseComboMultiplyScore()
        {
            multiplyScore++;
        }
        
        private void LoadScoreData(GameData.GameData _gamedata)
        {
            multiplyScore = _gamedata.MultiplyCombo;
        }

        private void OnDestroy()
        {
            gameCardMatchChecker.OnMatchComplete -= IncreaseComboMultiplyScore;
            gameCardMatchChecker.OnMatchFailure -= DecreaseComboMultiplyScore;
            GameCardSaveLoadData.instance.OnGameSavedDataEventHandler -= HandleGameSaved;
        }
        
        public int MultiPlyScore
        {
            get => multiplyScore;
            set => multiplyScore = value;
        }
    }
}
