using System;
using System.Collections;
using TwinMemoryPuzzle.Scripts.Controller;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.State
{
    public class GameStateManager
    {
        public static GameStateManager Instance { get; } = new GameStateManager();

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
        
    }

    public class IntroState : IGameState
    {
        public IEnumerator Start()
        {
            Debug.Log("Entering Intro State");
            // Wait for 3 seconds before starting the game
            yield return new WaitForSeconds(3f);
            Debug.Log("Intro State Finished");
            GameController.instance.SetState(new GamePreState());
        }
    }
    
    public class GamePreState : IGameState
    {
        public IEnumerator Start()
        {
            Debug.Log("Pre Gameplay State");
            yield return new WaitForSeconds(1f);
            GameController.instance.SetState(new GamePlayState());
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
