using System;
using TwinMemoryPuzzle.Scripts.GameData;
using TwinMemoryPuzzle.Scripts.Logic;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Score
{
    public interface IScoreUpdater 
    {
        void UpdatePoint();
        int Point { get; set; } 
        event Action OnUpdate; 
    }
    [RequireComponent(typeof(ScoreUpdater))]
    [RequireComponent(typeof(TurnUpdater))]
    [RequireComponent(typeof(MatchUpdater))]
    public class GameScore : MonoBehaviour
    {
        [SerializeField] private GameCardMatchChecker gameCardMatchChecker;
        
        [SerializeField] private ScoreUpdater scoreUpdater;
        [SerializeField] private MatchUpdater matchUpdater;
        [SerializeField] private TurnUpdater turnUpdater;
        
        [SerializeField] private int scoreIncrement;
        [SerializeField] private int matchIncrement;
        [SerializeField] private int turnIncrement;
        
        public Action<int> OnScoresUpdated;
        public Action<int> OnMatchUpdated;
        public Action<int> OnTurnsUpdated;
        
        [SerializeField] private GameCardSaveLoadData gameCardSaveLoadData;
        void Start()
        {
            scoreUpdater = GetComponent<ScoreUpdater>();
            matchUpdater = GetComponent<MatchUpdater>();
            turnUpdater = GetComponent<TurnUpdater>();
            
            gameCardMatchChecker.OnScoreUpdate += ScoreUpdater;
            gameCardMatchChecker.OnMatchComplete += MatchUpdater;
            gameCardMatchChecker.OnTurnUpdate += TurnUpdate;

            scoreUpdater.OnUpdate += GetUpdateScore;
            matchUpdater.OnUpdate += GetUpdateMatchScore;
            turnUpdater.OnUpdate  += GetUpdateTurnScore;
            gameCardSaveLoadData.OnGameSavedDataEventHandler += HandleGameSaved;
        }
        private void GetUpdateTurnScore()
        {
             Debug.Log($"turn : {turnUpdater.Point} ");  
             OnTurnsUpdated?.Invoke(turnUpdater.Point);
        }
        private void GetUpdateMatchScore()
        {
            Debug.Log($"match : {matchUpdater.Point} ");  
            OnMatchUpdated?.Invoke(matchUpdater.Point);
        }
        private void GetUpdateScore()
        {
            Debug.Log($"score : {scoreUpdater.Point} ");  
            OnScoresUpdated?.Invoke(scoreUpdater.Point);
        }
        private void HandleGameSaved(object _sender, SaveGameEventArgs _e)
        {
            _e.GameSavedData.Score = scoreUpdater.Point;
            _e.GameSavedData.TurnScore = turnUpdater.Point;
            _e.GameSavedData.MatchScore = matchUpdater.Point;
        }

        private void ScoreUpdater() => scoreUpdater.UpdatePoint(scoreIncrement);
        private void TurnUpdate() => turnUpdater.UpdatePoint(turnIncrement);
        private void MatchUpdater() => matchUpdater.UpdatePoint(matchIncrement);
    }

    
    
    
    
    
    
}
