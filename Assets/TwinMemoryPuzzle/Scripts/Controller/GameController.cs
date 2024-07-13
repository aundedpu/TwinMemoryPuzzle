using TwinMemoryPuzzle.Scripts.State;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Controller
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        private IGameState currentState;
        
        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            SetState(new IntroState());
        }
        
        public void SetState(IGameState state)
        {
            currentState = state;
            StartCoroutine(currentState.Start());
            GameStateManager.Instance.ChangeState(currentState);
        }

        public IGameState GetState() => currentState;
        
        private void StateCompleted()
        {
            
        }
    }
}
