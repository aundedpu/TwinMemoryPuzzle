using System;
using System.Collections.Generic;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Random
{
    public class CardRandomizerAndPlacer : MonoBehaviour
    {
        public event Action<List<Card.Card>> OnCardsฺGenerationFinish;
        public void RandomizeAndPlace(List<Card.Card> cards, GameObject[][] slots)
        {
            List<Card.Card> cardsInScene =  new List<Card.Card>();
            List<GameObject> availableSlots=new List<GameObject>();
            // Fill the availableSlots list with all slots.
            for(int i=0;i<slots.Length;i++)
            {
                for(int j=0;j<slots[i].Length;j++)
                {
                    availableSlots.Add(slots[i][j]);
                }
            }
            int numPairs = availableSlots.Count / 2;
            Debug.Log(numPairs);
            
            // Iterate over number of pairs to create.
            for(int pair=0; pair < numPairs; pair++)
            {
                // For each pair, choose a random card
                int randomCardIndex = UnityEngine.Random.Range(0, cards.Count);
                Card.Card card = cards[randomCardIndex];

                // Place two instances of the card in random slots.
                for(int i=0;i<2;i++)
                {
                    GameObject cardInstance=Instantiate(card.cardGoPrefabs);
                    cardInstance.GetComponent<Card.Card>().ID = card.ID;
                
                    // Choose a random slot for placement.
                    int randomSlotIndex= UnityEngine.Random.Range(0,availableSlots.Count);

                    // Set the cardInstance to be a child of the slot
                    cardInstance.transform.SetParent(availableSlots[randomSlotIndex].transform, false);

                    // Remove the slot from availableSlots, so it can't be used again.
                    availableSlots.RemoveAt(randomSlotIndex);
                    
                    //Add CardInScene
                    cardsInScene.Add(cardInstance.GetComponent<Card.Card>());
                }
            } 
            OnCardsฺGenerationFinish?.Invoke(cardsInScene);
        }
        
    }
}