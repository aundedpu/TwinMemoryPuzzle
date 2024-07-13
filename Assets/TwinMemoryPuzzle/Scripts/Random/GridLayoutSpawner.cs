using UnityEngine;
using UnityEngine.UI;

namespace TwinMemoryPuzzle.Scripts.Random
{
    public class GridLayoutSpawner : MonoBehaviour
    {
        [SerializeField] GameObject slotPrefab; 
        [SerializeField] private GridLayoutGroup gridLayoutPanel;
    
        public GameObject[][] SpawnerGridSlot(int rows,int cols)
        {
            GameObject[][] slots = new GameObject[rows][];
        
            gridLayoutPanel.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            gridLayoutPanel.constraintCount = rows;
        
            for (int i = 0; i < rows; i++)
            {
                slots[i] = new GameObject[cols];

                for (int j = 0; j < cols; j++)
                {
                    GameObject newSlot = Instantiate(slotPrefab, gridLayoutPanel.transform);
                    newSlot.name = $"{i},{j}";
                    slots[i][j] = newSlot;
                }
            }
        
            return slots;
        }
    }
}
