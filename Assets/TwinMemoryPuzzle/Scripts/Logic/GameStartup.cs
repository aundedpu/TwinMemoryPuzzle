using System;
using TwinMemoryPuzzle.Scripts.Level;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Logic
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private StartGameButton startGameButton;
        [SerializeField] private LevelSetup levelsetup;
        [SerializeField] private LoadLevelSetup loadLevelsetup;
        
        // Start is called before the first frame update
        void Start()
        {
            Initializer();
        }

        private void Initializer()
        {
            if (GameMode.CurrentGameMode == GameMode.StartGameMode.NewGame)
            {
                startGameButton.ButtonStart.gameObject.SetActive(true);   
            }
            else if (GameMode.CurrentGameMode == GameMode.StartGameMode.LoadGame)
            {
                startGameButton.ButtonStart.gameObject.SetActive(false);
                loadLevelsetup.LoadGameSetUp();
            }
        }
        
    }
}
