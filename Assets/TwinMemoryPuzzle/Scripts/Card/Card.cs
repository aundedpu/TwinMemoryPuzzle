using System;
using System.Collections.Generic;
using TwinMemoryPuzzle.Scripts.Logic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TwinMemoryPuzzle.Scripts.Card
{
    public interface ICard
    {
        bool IsMatched { get; set; }
        Sprite Image { get; set; }
        void ShowCard();
        void HideCard();
        bool Compare(ICard otherCard);
    }

    public class Card : MonoBehaviour, ICard, ICardActionBroadcaster , IPointerClickHandler
    {
        [SerializeField] private Image cardImage;
        public GameObject cardBackground;
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
            get { return id; }
            set { id = value; }
        }

        public Sprite Image { get; set; }
        public bool IsMatched { get; set; }

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
            cardImage.sprite = Image;
            Debug.Log($"Show card: {ID}");
            NotifyCardObserversOfAction();
            // cardBackground.SetActive(false);
        }

        public void HideCard()
        {
            NotifyCardObserversOfAction();
            // cardBackground.SetActive(true);
        }

        public bool Compare(ICard otherCard)
        {
            // Specifically, using object/memory reference which will be unique for each card.
            return this == otherCard;
        }

        public void RegisterCardObserver(ICardObserver observer)
        {
            _observers?.Add(observer);
        }

        public void RemoveCardObserver(ICardObserver observer)
        {
            _observers?.Remove(observer);
        }
        

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