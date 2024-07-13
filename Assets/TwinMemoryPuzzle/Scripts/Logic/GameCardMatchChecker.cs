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
    public class GameCardMatchChecker : MonoBehaviour, ICardObserver
    {
        [SerializeField] private LevelSetup levelSetup;
        // Start is called before the first frame update
        void Start()
        {
            GameStateManager.Instance.OnStateChanged += HandleOnStateChanged;
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
        }
        
        void OnDestroy()
        {
            GameStateManager.Instance.OnStateChanged -= HandleOnStateChanged;
        }
    }
}
