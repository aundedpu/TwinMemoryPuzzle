using TwinMemoryPuzzle.Scripts.Audio;
using TwinMemoryPuzzle.Scripts.Constant;
using TwinMemoryPuzzle.Scripts.Logic;
using TwinMemoryPuzzle.Scripts.Utility;
using TwinMemoryPuzzle.Utility;
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
            DelayedInvoker.InvokeAfterDelay(.25f, () => {
                buttonComplete.gameObject.SetActive(true);
            });
            AudioManager.instance.PlayFxSound(2);
        }

        private void NexLevel()
        {
            AudioManager.instance.PlayFxSound(0);
            GameMode.CurrentGameMode = GameMode.StartGameMode.NewGame;
            GlobalConstant.INDEX_CURRENT_SCENE += 1;
            SceneLoader.instance.LoadSceneWithFade(GlobalConstant.INDEX_CURRENT_SCENE);
        }

    }
}
