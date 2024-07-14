using System;
using System.Collections.Generic;
using System.Linq;
using TwinMemoryPuzzle.Scripts.Constant;
using TwinMemoryPuzzle.Scripts.GameData;
using TwinMemoryPuzzle.Scripts.Random;
using TwinMemoryPuzzle.Scripts.State;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace TwinMemoryPuzzle.Scripts.Level
{
    public class LevelSetup : MonoBehaviour
    {
        [SerializeField] private GridLayoutSpawner gridLayoutSpawner;
        [SerializeField] private CardRandomizerAndPlacer cardRandomizerAndPlacer;
        
        [SerializeField] private int rows ;
        [SerializeField] private int cols;
        [SerializeField] private List<Card.Card> cardsPrefabs;
        private List<Card.Card> cardsInScene;
        [SerializeField] private GameCardSaveLoadData gameCardSaveLoadData;
        public GameObject[][] Slots { get; set;}
        
        public event Action OnLevelSetupFinish;
        
        // Start is called before the first frame update
        void Start()
        {
            cardRandomizerAndPlacer.OnCardsฺGenerationFinish += (cards) =>
            {
                cardsInScene = cards;
                OnLevelSetupFinish?.Invoke();
            };
            GameEventState.Instance.OnStateChanged += HandleOnStateChanged;
            gameCardSaveLoadData.OnGameSavedDataEventHandler += HandleGameSaved;
        }

        private void HandleGameSaved(object _sender, SaveGameEventArgs _e)
        {
            Debug.Log($"Game state is open saved!");
            _e.GameSavedData.CurrentLevelIndex = SceneManager.GetSceneAt(1).buildIndex;
            _e.GameSavedData.RowGridLayout = rows;
            _e.GameSavedData.ColGridLayout = cols;
            
            var gameData = _e.GameSavedData;
            for (int i = 0; i < cardsInScene.Count(); i++)
            {
                var card = cardsInScene[i];
                CardData cardData = new CardData
                {
                    ID = card.ID,
                    IsShow = card.IsShow,
                    IsMatch = card.IsMatch
                };
                gameData.CardDatas.Add(cardData);
            }
        }

        private void HandleOnStateChanged(IGameState IgameState)
        {
            if (IgameState is IntroState)
            {
                Slots = gridLayoutSpawner.SpawnerGridSlot(rows,cols);
                cardRandomizerAndPlacer.RandomizeAndPlace(cardsPrefabs,Slots);
            }
            
            if (IgameState is GamePreState)
            {
                foreach (var card in cardsInScene)
                {
                    card.CloseCard();
                }
            }
        }

        private void OnDestroy()
        {
            GameEventState.Instance.OnStateChanged -= HandleOnStateChanged;
            gameCardSaveLoadData.OnGameSavedDataEventHandler -= HandleGameSaved;
        }

        public List<Card.Card> GetCardsInScene() => cardsInScene;
    }
}
