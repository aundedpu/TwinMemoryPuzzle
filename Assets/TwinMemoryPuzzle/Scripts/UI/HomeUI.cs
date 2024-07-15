
using TwinMemoryPuzzle.Scripts.Audio;
using TwinMemoryPuzzle.Scripts.Constant;
using TwinMemoryPuzzle.Scripts.GameData;
using TwinMemoryPuzzle.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace TwinMemoryPuzzle.Scripts.UI
{
    public class HomeUI : MonoBehaviour
    {
        [SerializeField] private Image popupImage;
        
        public void LoadSceneHome()
        {
            AudioManager.instance.PlayFxSound(0);
            GameCardSaveLoadData.instance.SaveGame(new GameData.GameData());
            SceneLoader.instance.LoadSceneWithFade(GlobalConstant.INDEX_SAVELOAD_SCENE);
        }
        public void ShowPopup()
        {
            AudioManager.instance.PlayFxSound(0);
            popupImage.gameObject.SetActive(true);
        }

        public void HidePopup()
        {
            AudioManager.instance.PlayFxSound(0);
            popupImage.gameObject.SetActive(false);
        }
    }
}
