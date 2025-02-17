using System;
using TwinMemoryPuzzle.Scripts.GameData;
using TwinMemoryPuzzle.Scripts.Score;
using UnityEngine;
using UnityEngine.UI;

namespace TwinMemoryPuzzle.Scripts.UI
{
    [RequireComponent(typeof(ScoreAnimatorUpdate))]
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private GameScore gameScore;
        [SerializeField] private ScoreAnimatorUpdate scoreAnimatorUpdate;
        [SerializeField] private Text scoreText;  
        [SerializeField] private Text matchText; 
        [SerializeField] private Text turnText;
        void Awake()
        {
            GameCardSaveLoadData.instance.OnLoadGameUpdate += LoadScoreData;
        }

        void Start()
        {
            scoreAnimatorUpdate = GetComponent<ScoreAnimatorUpdate>();
            // gameScore.OnScoresUpdated += UpdateScoreTextUi;
            gameScore.ScoreUpdate.OnScoresAnimatorUpdated += UpdateScoreTextUi;
            gameScore.OnTurnsUpdated += UpdateTurnTextUi;
            gameScore.OnMatchUpdated += UpdateMatchTextUi;
        }

        private void UpdateScoreTextUi(int oldScore, int newScore)
        {
            scoreAnimatorUpdate.AnimateScore(oldScore,newScore,scoreText);
        }

        private void UpdateMatchTextUi(int score)
        {
            if(matchText)
                matchText.text = $"{score}";
        }

        private void UpdateTurnTextUi(int score)
        {
            if(turnText)
                turnText.text = $"{score}";
        }

        private void UpdateScoreTextUi(int score)
        {
            if(scoreText)
                scoreText.text = $"{score}";
        }
        
        private void LoadScoreData(GameData.GameData _gamedata)
        {
            gameScore.ScoreUpdate.Point = _gamedata.Score;
            gameScore.TurnScoreUpdater.Point = _gamedata.TurnScore;
            gameScore.MatchScoreUpdater.Point = _gamedata.MatchScore;
            
            UpdateScoreTextUi(gameScore.ScoreUpdate.Point);
            UpdateMatchTextUi(gameScore.MatchScoreUpdater.Point);
            UpdateTurnTextUi(gameScore.TurnScoreUpdater.Point);
        }

        private void OnDestroy()
        {
            gameScore.OnScoresUpdated -= UpdateScoreTextUi;
            gameScore.OnTurnsUpdated -= UpdateTurnTextUi;
            gameScore.OnMatchUpdated -= UpdateMatchTextUi;
            GameCardSaveLoadData.instance.OnLoadGameUpdate -= LoadScoreData;
        }
        
    }
}
