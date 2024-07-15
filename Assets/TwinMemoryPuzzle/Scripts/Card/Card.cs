using System;
using System.Collections.Generic;
using TwinMemoryPuzzle.Scripts.Logic;
using TwinMemoryPuzzle.Scripts.State;
using TwinMemoryPuzzle.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TwinMemoryPuzzle.Scripts.Card
{
    public interface ICard
    {
        bool IsShow { get; set; }
        bool IsMatch { get; set; }
        Sprite Image { get; set; }
        void ShowCard();
        void CloseCard();
        void HideCard();
        void MatchComplete();

    }
    
    [RequireComponent(typeof(CardAnimation))]
    public class Card : MonoBehaviour, ICard, ICardActionBroadcaster , IPointerClickHandler
    {
        [SerializeField] private Image cardImage;
        public GameObject cardGoPrefabs;
        [SerializeField] private GameObject hideCardBackground;
        [SerializeField] private int id;
        
        [SerializeField]
        private List<ICardObserver> _observers = new List<ICardObserver>();
        private List<ICardObserver> cardObservers = new List<ICardObserver>();

        [SerializeField] private CardAnimation cardAnimation;

        void Start()
        {
            cardAnimation = GetComponent<CardAnimation>();
        }

        public int ID
        {
            get => id;
            set => id = value;
        }

        public bool IsShow { get; set; }
        public bool IsMatch { get; set; }
        public Sprite Image { get; set; }

        public Card(Sprite cardImage)
        {
            this.Image = cardImage;
        }

        // This method gets automatically called when the card is clicked.
        public void OnPointerClick(PointerEventData eventData)
        {
            ShowCard();
        }
        public void ShowCard()
        {
            if(GameState.instance.GetState() is not GamePlayState) 
                return;
            if(IsShow) 
                return;
            IsShow = true;
            NotifyCardObserversOfAction();
            cardAnimation.CardAnimator.enabled = true;
            if(cardAnimation)
                cardAnimation.Show();
            else
                hideCardBackground.SetActive(false);
        }

        public void CloseCard()
        {
            cardAnimation.CardAnimator.enabled = true;
            IsShow = false;
            if (cardAnimation)
            { 
                cardAnimation.Hide();
            }
        }

        public void ForceCloseCard()
        {
            cardAnimation.CardAnimator.enabled = false;
            hideCardBackground.SetActive(true);
            DelayedInvoker.InvokeAfterDelay(.25f, () => {
                // cardAnimation.CardAnimator.enabled = true;
            });
        }
        
        public void ForceHideCard()
        {
            gameObject.SetActive(false);
        }
        
        public void HideCard()
        {
                DelayedInvoker.InvokeAfterDelay(.25f, () => {
                    gameObject.SetActive(false);
                });
        }

        public void MatchComplete()
        {
            HideCard();
            IsMatch = true;
        }

        public void RegisterCardObserver(ICardObserver observer)
        {
            if (_observers == null)
            {
                Debug.Log("_observers is null");
                _observers = new List<ICardObserver>();
                Debug.Log(_observers.Count);
            }
            _observers?.Add(observer);
        }

        public void RemoveCardObserver(ICardObserver observer) => _observers?.Remove(observer);

        public void NotifyCardObserversOfAction()
        {
            foreach (ICardObserver observer in _observers)
            {
                observer?.OnCardActionOccurred();
                observer?.UpdateCardStatus(this);
            }
        }
    }
}