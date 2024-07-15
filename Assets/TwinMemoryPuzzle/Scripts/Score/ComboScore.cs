using System;
using TwinMemoryPuzzle.Scripts.Logic;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Score
{
    public class ComboScore : MonoBehaviour
    {
        private int multiplyScore ;
        [SerializeField] private GameCardMatchChecker gameCardMatchChecker;
 
        // Start is called before the first frame update
        void Start()
        {
            gameCardMatchChecker.OnMatchComplete += IncreaseComboMultiplyScore;
            gameCardMatchChecker.OnMatchFailure += DecreaseComboMultiplyScore;
        }

        private void DecreaseComboMultiplyScore()
        {
            Debug.Log("MUlti: " +multiplyScore);
            multiplyScore = 0;
        }

        private void IncreaseComboMultiplyScore()
        {
            Debug.Log("MUlti: " +multiplyScore);
            multiplyScore++;
        }

        private void OnDestroy()
        {
            gameCardMatchChecker.OnMatchComplete -= IncreaseComboMultiplyScore;
            gameCardMatchChecker.OnMatchFailure -= DecreaseComboMultiplyScore;
        }
        
        public int MultiPlyScore
        {
            get => multiplyScore;
            set => multiplyScore = value;
        }
    }
}
