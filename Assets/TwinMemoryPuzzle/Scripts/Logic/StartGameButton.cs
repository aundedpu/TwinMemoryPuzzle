using TwinMemoryPuzzle.Scripts.State;
using UnityEngine;
using UnityEngine.UI;


namespace TwinMemoryPuzzle.Scripts.Logic
{
    public class StartGameButton : MonoBehaviour
    {
        [SerializeField] private Button buttonStart;

        // Start is called before the first frame update
        void Start()
        {
            buttonStart.onClick.AddListener(StartGame);
        }
        private void StartGame()
        {
            buttonStart.gameObject.SetActive(false);
            GameState.instance.SetState(new IntroState());    
            Debug.Log($"Start");
        }
        
        public Button ButtonStart { 
            get => buttonStart;
            set => buttonStart = value;
        }
        
    }
}