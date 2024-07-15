using System;
using TwinMemoryPuzzle.Scripts.Audio;
using TwinMemoryPuzzle.Scripts.Constant;
using TwinMemoryPuzzle.Scripts.Logic;
using TwinMemoryPuzzle.Scripts.Utility;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.UI
{
    public class IntroUI : MonoBehaviour
    {
        void Awake()
        {
            AudioManager.instance.PlayBgSound();    
        }

        public void Play()
        {
            AudioManager.instance.PlayFxSound(0);
            GameMode.CurrentGameMode = GameMode.StartGameMode.NewGame;
            SceneLoader.instance.LoadSceneWithFade(GlobalConstant.INDEX_CURRENT_SCENE);
        }

        public void LoadGame()
        {
            AudioManager.instance.PlayFxSound(0);
            GameMode.CurrentGameMode = GameMode.StartGameMode.LoadGame;
            SceneLoader.instance.LoadSceneWithFade(GlobalConstant.INDEX_CURRENT_SCENE);
        }
        
        public void Exit()
        {
            AudioManager.instance.PlayFxSound(0);
            Application.Quit();
        }
    }
}
