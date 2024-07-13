using System.Collections.Generic;
using TwinMemoryPuzzle.Scripts.Random;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Level
{
    public class LevelSetup : MonoBehaviour
    {
        [SerializeField] private GridLayoutSpawner gridLayoutSpawner;
        [SerializeField] private CardRandomizerAndPlacer cardRandomizerAndPlacer;
        
        [SerializeField] private int rows ;
        [SerializeField] private int cols;
        [SerializeField] private List<Card.Card> cards;
        
        // Start is called before the first frame update
        void Start()
        {
            var slots = gridLayoutSpawner.SpawnerGridSlot(rows,cols);
            cardRandomizerAndPlacer.RandomizeAndPlace(cards,slots);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
