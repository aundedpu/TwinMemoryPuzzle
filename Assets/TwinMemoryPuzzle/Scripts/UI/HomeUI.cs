
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
            GameCardSaveLoadData.instance.SaveGame(new GameData.GameData());
            SceneLoader.instance.LoadSceneWithFade(GlobalConstant.INDEX_SAVELOAD_SCENE);
        }
        public void ShowPopup()
        {
            popupImage.gameObject.SetActive(true);
        }

        public void HidePopup()
        {
            popupImage.gameObject.SetActive(false);
        }
    }
}
