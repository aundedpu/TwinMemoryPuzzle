using System;
using System.Collections.Generic;
using TwinMemoryPuzzle.Scripts.Random;
using UnityEngine;
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
        public event Action OnLevelSetupFinish;
        
        // Start is called before the first frame update
        void Start()
        {
            cardRandomizerAndPlacer.OnCardsฺGenerationFinish += (cards) =>
            {
                cardsInScene = cards;
                Debug.Log($"card in scene : {cardsInScene.Count}");
                OnLevelSetupFinish?.Invoke();
            };
            
            var slots = gridLayoutSpawner.SpawnerGridSlot(rows,cols);
            cardRandomizerAndPlacer.RandomizeAndPlace(cardsPrefabs,slots);
        }
        
        public List<Card.Card> GetCardsInScene() => cardsInScene;
    }
}
