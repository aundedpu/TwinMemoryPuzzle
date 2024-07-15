using System;
using System.Collections.Generic;
using TwinMemoryPuzzle.Scripts.Level;
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
    public delegate void TurnUpdate();
    public class GameCardMatchChecker : MonoBehaviour, ICardObserver
    {
        [SerializeField] private LevelSetup levelSetup;
        
        private List<Card.Card> selectedCards  = new List<Card.Card>();
        public event CardSelected OnCardSelected;
        public event ScoreUpdate OnScoreUpdate;
        public event MatchComplete OnMatchComplete;
        public event TurnUpdate OnTurnUpdate;
        
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

        private void Update()
        {
            Debug.Log($"Card Was Select : {selectedCards.Count}");
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
                OnMatchComplete?.Invoke();
                OnScoreUpdate?.Invoke();
                DelayedInvoker.InvokeAfterDelay(.25f, () => {
                    selectedCards[0].MatchComplete();
                    selectedCards[1].MatchComplete();
                    selectedCards.Clear();
                });
            }
            else
            {
                Debug.Log("Not Cards Match!");
                DelayedInvoker.InvokeAfterDelay(.25f, () => {
                    selectedCards[0].CloseCard();
                    selectedCards[1].CloseCard();
                    selectedCards.Clear();
                });
                
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
