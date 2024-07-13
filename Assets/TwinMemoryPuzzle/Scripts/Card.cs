using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace TwinMemoryPuzzle
{
    public interface ICard
    {
        bool IsMatched { get; set; }
        Sprite Image { get; set; }
        void ShowCard();
        void HideCard();
        bool Compare(ICard otherCard);
    }

    public class Card : MonoBehaviour, ICard, IPointerClickHandler
    {
        [SerializeField] private Image cardImage;
        [SerializeField] private GameObject cardBack;
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
            cardBack.SetActive(false);
        }
    
        public void HideCard()
        {
            cardBack.SetActive(true);
        }

        public bool Compare(ICard otherCard)
        {
            // Specifically, using object/memory reference which will be unique for each card.
            return this == otherCard;
        }
    }
}