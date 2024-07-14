using System;
using TwinMemoryPuzzle.Scripts.Score;
using UnityEngine;
using UnityEngine.UI;

namespace TwinMemoryPuzzle.Scripts.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private GameScore gameScore;
        [SerializeField] private Text scoreText;  
        [SerializeField] private Text matchText; 
        [SerializeField] private Text turnText; 
        void Start()
        {
            gameScore.OnScoresUpdated += UpdateScoreTextUi;
            gameScore.OnTurnsUpdated += UpdateTurnTextUi;
            gameScore.OnMatchUpdated += UpdateMatchTextUi;
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

        private void OnDestroy()
        {
            gameScore.OnScoresUpdated -= UpdateScoreTextUi;
            gameScore.OnTurnsUpdated -= UpdateTurnTextUi;
            gameScore.OnMatchUpdated -= UpdateMatchTextUi;
        }
    }
}
