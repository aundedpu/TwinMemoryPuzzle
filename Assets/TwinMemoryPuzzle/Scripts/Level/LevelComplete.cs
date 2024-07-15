using TwinMemoryPuzzle.Scripts.Constant;
using TwinMemoryPuzzle.Scripts.Logic;
using TwinMemoryPuzzle.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace TwinMemoryPuzzle.Scripts.Level
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private GameCardMatchChecker gameCardMatchChecker;
        [SerializeField] private Button buttonComplete;
        
        // Start is called before the first frame update
        void Start()
        {
            gameCardMatchChecker.OnCompleteLevel += CompleteLevel;
            buttonComplete.onClick.AddListener(NexLevel);
        }

        private void CompleteLevel()
        {
            buttonComplete.gameObject.SetActive(true);
        }

        private void NexLevel()
        {
            GameMode.CurrentGameMode = GameMode.StartGameMode.NewGame;
            GlobalConstant.INDEX_CURRENT_SCENE += 1;
            SceneLoader.instance.LoadSceneWithFade(GlobalConstant.INDEX_CURRENT_SCENE);
        }

    }
}
