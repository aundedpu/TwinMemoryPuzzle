using System;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Score
{
    public class ScoreUpdater : MonoBehaviour ,IScoreUpdater
    {
        public void UpdatePoint(int value)
        {
            Point += value;
            UpdatePoint();
        }
        
        public void UpdatePoint()
        {
            OnUpdate?.Invoke();
        }

        public int Point { get; set; }
        
        public event Action OnUpdate;
    }
}
