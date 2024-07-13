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

    public class Card : MonoBehaviour, ICard, IPointerClickHandler
    {
        [SerializeField] private Image cardImage;
        public GameObject cardBackground;
        [SerializeField] private int id;

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
            cardBackground.SetActive(false);
        }

        public void HideCard()
        {
            cardBackground.SetActive(true);
        }

        public bool Compare(ICard otherCard)
        {
            // Specifically, using object/memory reference which will be unique for each card.
            return this == otherCard;
        }
    }
}