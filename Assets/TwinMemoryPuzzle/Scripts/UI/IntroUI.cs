using TwinMemoryPuzzle.Scripts.Constant;
using TwinMemoryPuzzle.Scripts.Logic;
using TwinMemoryPuzzle.Scripts.Utility;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.UI
{
    public class IntroUI : MonoBehaviour
    {
        public void Play()
        {
            GameMode.CurrentGameMode = GameMode.StartGameMode.NewGame;
            SceneLoader.instance.LoadSceneWithFade(GlobalConstant.INDEX_CURRENT_SCENE);
        }

        public void LoadGame()
        {
            GameMode.CurrentGameMode = GameMode.StartGameMode.LoadGame;
            SceneLoader.instance.LoadSceneWithFade(GlobalConstant.INDEX_CURRENT_SCENE);
        }
        
        public void Exit() => Application.Quit();
    }
}
