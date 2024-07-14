using System;
using System.Collections.Generic;
using TwinMemoryPuzzle.Scripts.Logic;
using TwinMemoryPuzzle.Scripts.State;
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
    
    public class Card : MonoBehaviour, ICard, ICardActionBroadcaster , IPointerClickHandler
    {
        [SerializeField] private Image cardImage;
        public GameObject cardGoPrefabs;
        [SerializeField] private GameObject hideCardBackground;
        [SerializeField] private int id;
        
        [SerializeField]
        private List<ICardObserver> _observers = new List<ICardObserver>();
        private List<ICardObserver> cardObservers = new List<ICardObserver>();
        private void Start()
        {
            if (_observers == null)
            {
                Debug.Log("_observers is null");
                _observers = new List<ICardObserver>();
                Debug.Log(_observers.Count);
            }
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
            hideCardBackground.SetActive(false);
        }

        public void CloseCard()
        {
            IsShow = false;
            hideCardBackground.SetActive(true);
        }
        
        public void HideCard()
        {
            gameObject.SetActive(false);
        }

        public void MatchComplete()
        {
            HideCard();
            IsMatch = true;
        }

        public void RegisterCardObserver(ICardObserver observer) => _observers?.Add(observer);

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