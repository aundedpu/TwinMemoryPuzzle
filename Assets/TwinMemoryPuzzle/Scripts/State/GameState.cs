using System;
using System.Collections;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.State
{
    public class GameEventState
    {
        public static GameEventState Instance { get; } = new GameEventState();

        public event Action<IGameState> OnStateChanged;

        public void ChangeState(IGameState state)
        {
            OnStateChanged?.Invoke(state);
        }
    }
    public interface IGameState
    {
        IEnumerator Start();
    }
    
    public class GameState : MonoBehaviour
    {
        public static GameState instance;
        private IGameState currentState;
        
        void Start()
        {
            instance = this;
            SetState(new IntroState());
        }
        public void SetState(IGameState state)
        {
            currentState = state;
            StartCoroutine(currentState.Start());
            GameEventState.Instance.ChangeState(currentState);
        }
        public IGameState GetState() => currentState;

    }

    public class IntroState : IGameState
    {
        public IEnumerator Start()
        {
            Debug.Log("Entering Intro State");
            // Wait for 3 seconds before starting the game
            yield return new WaitForSeconds(3f);
            Debug.Log("Intro State Finished");
            GameState.instance.SetState(new GamePreState());
        }
    }
    
    public class GamePreState : IGameState
    {
        public IEnumerator Start()
        {
            Debug.Log("Pre Gameplay State");
            yield return new WaitForSeconds(2f);
            GameState.instance.SetState(new GamePlayState());
        }
    }
    
    
    public class GamePlayState : IGameState
    {
        public IEnumerator Start()
        {
            Debug.Log("Entering Gameplay State");
            yield break;
        }
    }
}
