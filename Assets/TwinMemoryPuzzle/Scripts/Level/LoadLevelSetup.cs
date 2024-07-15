using System.Collections.Generic;
using System.Linq;
using TwinMemoryPuzzle.Scripts.Constant;
using TwinMemoryPuzzle.Scripts.GameData;
using TwinMemoryPuzzle.Scripts.Logic;
using TwinMemoryPuzzle.Scripts.Random;
using TwinMemoryPuzzle.Scripts.State;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Level
{
    public class LoadLevelSetup : MonoBehaviour
    {
        [SerializeField] private LevelSetup levelSetup;
        [SerializeField] private GridLayoutSpawner gridLayoutSpawner;
        [SerializeField] private List<Card.Card> cardsPrefab = new List<Card.Card>();
        [SerializeField] private GameCardMatchChecker gameCardMatchChecker;
    
        public void LoadGameSetUp()
        {
            GameData.GameData gameData = GameCardSaveLoadData.instance.LoadGame(GlobalConstant.FILE_SAVE_GAME_NAME);
            
            var slots = gridLayoutSpawner.SpawnerGridSlot(gameData.RowGridLayout,gameData.ColGridLayout);

            List<Card.Card> cardsInScene = levelSetup.GetCardsInScene();  
            cardsInScene?.Clear();
            
            //Load Card
            for (int i = 0; i < gameData.CardDatas.Count(); i++)
            {
                CardData cardData = gameData.CardDatas[i];
                GameObject cardGo =Instantiate(cardsPrefab[cardData.ID-1].cardGoPrefabs);
                Card.Card card = cardGo.GetComponent<Card.Card>();
                card.ID = cardData.ID;
                card.IsShow = cardData.IsShow;
                card.IsMatch = cardData.IsMatch;
                 
                GameObject slot = GameObject.Find(cardData.PositionName);
                card.transform.SetParent(slot.transform, false);
                if (card.IsShow)
                    card.ShowCard();
                else
                    card.CloseCard();
                if(card.IsMatch)
                    card.HideCard();
                // Load Card Select
                if(card.IsShow && !card.IsMatch)
                    gameCardMatchChecker.SelectedCards.Add(card);
                
                if (cardsInScene == null)
                {
                    cardsInScene = new List<Card.Card>();
                }
                cardsInScene?.Add(card);
            }
            levelSetup.SetCardsInScene(cardsInScene);
            GameState.instance.SetState(new GamePreState());
        }
        
    
    }
}
