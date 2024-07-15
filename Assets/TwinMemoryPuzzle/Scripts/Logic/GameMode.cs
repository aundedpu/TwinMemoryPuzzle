using System;
using TwinMemoryPuzzle.Scripts.Constant;
using TwinMemoryPuzzle.Scripts.Utility;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Logic
{
    public class GameMode : MonoBehaviour
    {
        public static GameMode instance;
        private void Awake()
        {
            instance = this;
        }

        public enum StartGameMode
        {
            NewGame,
            LoadGame
        }
        
        public static StartGameMode CurrentGameMode;
        
        
    }
}
