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
        
        public void AddPoint(int point) {
            int oldPoint = Point;
            Point += point;
            OnScoresAnimatorUpdated?.Invoke(oldPoint, Point);
        }
        
        public void UpdatePoint()
        {
            OnUpdate?.Invoke();
        }

        public int Point { get; set; }
        
        public event Action OnUpdate;
        public Action<int, int> OnScoresAnimatorUpdated;
    }
    
    
}
