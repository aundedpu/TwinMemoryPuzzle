using System.Collections.Generic;
using TwinMemoryPuzzle.Scripts.Level;
using TwinMemoryPuzzle.Scripts.State;
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
    public class GameCardMatchChecker : MonoBehaviour, ICardObserver
    {
        [SerializeField] private LevelSetup levelSetup;
        
        private List<Card.Card> selectedCards  = new List<Card.Card>();
        public event CardSelected OnCardSelected;
        
        // Start is called before the first frame update
        void Start()
        {
            GameEventState.Instance.OnStateChanged += HandleOnStateChanged;
        }

        private void HandleOnStateChanged(IGameState state)
        {
            if (state is GamePreState)
            {
                foreach (Card.Card card in levelSetup.GetCardsInScene())
                {
                    card.RegisterCardObserver(this);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
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
            Debug.Log($"card id {card.ID}");
            CardWasSelected(card);
        }
        
        private void CardWasSelected(Card.Card selectedCard)
        {
            OnCardSelected?.Invoke(selectedCard);
            selectedCards.Add(selectedCard);
            
            if(selectedCards.Count == 2)
            {
                CheckMatch();
                selectedCards.Clear();
            }
        }

        private void CheckMatch()
        {
            if(selectedCards[0].ID == selectedCards[1].ID)
            {
                Debug.Log("Cards Match!");
                selectedCards.Clear();
            }
            else
            {
                Debug.Log("Not Cards Match!");
            }
        }

        void OnDestroy()
        {
            GameEventState.Instance.OnStateChanged -= HandleOnStateChanged;
        }
    }
}
