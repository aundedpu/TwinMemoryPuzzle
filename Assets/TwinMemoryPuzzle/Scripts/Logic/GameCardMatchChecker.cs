using System;
using System.Collections.Generic;
using TwinMemoryPuzzle.Scripts.Audio;
using TwinMemoryPuzzle.Scripts.Level;
using TwinMemoryPuzzle.Scripts.Score;
using TwinMemoryPuzzle.Scripts.State;
using TwinMemoryPuzzle.Utility;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Logic
{
    public interface ICardObserver
    {
        void OnCardActionOccurred();
        void UpdateCardStatus(Card.Card card);

    }

    public interface ICardActionBroadcaster
    {
        void RegisterCardObserver(ICardObserver observer);
        void RemoveCardObserver(ICardObserver observer);
        void NotifyCardObserversOfAction();
    }
    public delegate void CardSelected(Card.Card selectedCard);
    public delegate void ScoreUpdate();
    public delegate void MatchComplete();
    public delegate void MatchFailure ();
    public delegate void TurnUpdate();
    public delegate void CompleteLevel();
    public class GameCardMatchChecker : MonoBehaviour, ICardObserver
    {
        [SerializeField] private LevelSetup levelSetup;
        [SerializeField] private LoadLevelSetup loadLevelSetup;
        [SerializeField] private GameScore gameScore; 
        
        private List<Card.Card> selectedCards  = new List<Card.Card>();
        public event CardSelected OnCardSelected;
        public event ScoreUpdate OnScoreUpdate;
        public event MatchComplete OnMatchComplete;
        public event MatchFailure OnMatchFailure;
        public event TurnUpdate OnTurnUpdate;
        public event CompleteLevel OnCompleteLevel;
        
        // Start is called before the first frame update
        void Start()
        {
            GameEventState.Instance.OnStateChanged += HandleOnStateChanged;
        }

        private void HandleOnStateChanged(IGameState state)
        {
            if (state is GamePreState)
            {
                Debug.Log("Ready " + levelSetup.GetCardsInScene().Count);
                foreach (Card.Card card in levelSetup.GetCardsInScene())
                {
                    card.RegisterCardObserver(this);
                }
            }
        }
        
        public void OnCardActionOccurred()
        {
            
        }

        public void UpdateCardStatus(Card.Card card)
        {
            if (GameState.instance.GetState() is not GamePlayState) return;
            Debug.Log($"card id {card.ID}");
            CardWasSelected(card);
        }
        
        private void CardWasSelected(Card.Card selectedCard)
        {
            if(selectedCards.Count > 2) return;
            
            OnCardSelected?.Invoke(selectedCard);
            selectedCards.Add(selectedCard);
            
            if(selectedCards.Count == 2)
            {
                CheckMatch();
                OnTurnUpdate?.Invoke();
            }
        }

        private void CheckMatch()
        {
            if(selectedCards[0].ID == selectedCards[1].ID)
            {
                Debug.Log("Cards Match!");
                AudioManager.instance.PlayFxSound(1);
                OnMatchComplete?.Invoke();
                OnScoreUpdate?.Invoke();
                DelayedInvoker.InvokeAfterDelay(.25f, () => {
                    selectedCards[0].MatchComplete();
                    selectedCards[1].MatchComplete();
                    selectedCards.Clear();
                });
                //Check Complete Game
                CheckCompleteGame();
            }
            else
            {
                Debug.Log("Not Cards Match!");
                OnMatchFailure?.Invoke();
                DelayedInvoker.InvokeAfterDelay(.25f, () => {
                    selectedCards[0].CloseCard();
                    selectedCards[1].CloseCard();
                    selectedCards.Clear();
                });
            }
        }

        private void CheckCompleteGame()
        {
            if (GameMode.CurrentGameMode == GameMode.StartGameMode.NewGame)
            {
                int allMatchComplete = ((levelSetup.Rows) * (levelSetup.Cols))/2;
                if (gameScore.MatchScoreUpdater.Point >= allMatchComplete)
                {
                    GameState.instance.SetState(new GameCompleteState());
                    OnCompleteLevel?.Invoke();
                }
            }
            else if (GameMode.CurrentGameMode == GameMode.StartGameMode.LoadGame)
            {
                int allMatchComplete = ((loadLevelSetup.Rows) * (loadLevelSetup.Cols))/2;
                if (gameScore.MatchScoreUpdater.Point >= allMatchComplete)
                {
                    GameState.instance.SetState(new GameCompleteState());
                    OnCompleteLevel?.Invoke();
                }
            }
        }

        void OnDestroy()
        {
            GameEventState.Instance.OnStateChanged -= HandleOnStateChanged;
        }
        
        public List<Card.Card> SelectedCards
        {
            get => selectedCards;
            set => selectedCards = value;
        }
    }
}
