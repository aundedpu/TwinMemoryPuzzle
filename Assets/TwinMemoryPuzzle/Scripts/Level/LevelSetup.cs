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
        public GameObject[][] Slots { get; set;}
        public event Action OnLevelSetupFinish;
        
        [SerializeField] private LoadLevelSetup loadLevelSetup ;
        
        // Start is called before the first frame update
        void Start()
        {
            cardRandomizerAndPlacer.OnCardsฺGenerationFinish += (cards) =>
            {
                cardsInScene = cards;
                OnLevelSetupFinish?.Invoke();
            };
            GameEventState.Instance.OnStateChanged += HandleOnStateChanged;
            GameCardSaveLoadData.instance.OnGameSavedDataEventHandler += HandleGameSaved;
        }

        private void HandleGameSaved(object _sender, SaveGameEventArgs _e)
        {
            Debug.Log($"Game state is open saved!");
            _e.GameSavedData.CurrentLevelIndex = SceneManager.GetSceneAt(0).buildIndex;
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
                    IsMatch = card.IsMatch,
                    PositionName = card.transform.parent.name
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

        public void LoadGameSetUp()
        {
            GameData.GameData gameData = GameCardSaveLoadData.instance.LoadGame(GlobalConstant.FILE_SAVE_GAME_NAME);
            
              Slots = gridLayoutSpawner.SpawnerGridSlot(gameData.RowGridLayout,gameData.ColGridLayout);
             
             
             
             cardsInScene?.Clear();
             //Load Card
             for (int i = 0; i < gameData.CardDatas.Count(); i++)
             {
                 CardData cardData = gameData.CardDatas[i];
                 GameObject cardGo =Instantiate(loadLevelSetup.cardsPrefab[cardData.ID-1].cardGoPrefabs);
                 Card.Card card = cardGo.GetComponent<Card.Card>();
                 card.ID = cardData.ID;
                 card.IsShow = cardData.IsShow;
                 card.IsMatch = cardData.IsMatch;
                 
                 GameObject slot = GameObject.Find(cardData.PositionName);
                 card.transform.SetParent(slot.transform, false);
                 if(card.IsShow)
                     card.ShowCard();
                 else
                     card.CloseCard();
                 if(card.IsMatch)
                     card.HideCard();
                 if (cardsInScene == null)
                 {
                     cardsInScene = new List<Card.Card>();
                 }
                 cardsInScene?.Add(card);
             }
             
             
        }

        private void OnDestroy()
        {
            GameEventState.Instance.OnStateChanged -= HandleOnStateChanged;
            GameCardSaveLoadData.instance.OnGameSavedDataEventHandler -= HandleGameSaved;
        }

        public List<Card.Card> GetCardsInScene() => cardsInScene;
    }
}
