using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutSpawner : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab; 
    [SerializeField] private int rows ;
    [SerializeField] private int cols;
    [SerializeField] private GridLayoutGroup gridLayoutPanel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnerSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnerSlot()
    {
        gridLayoutPanel.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayoutPanel.constraintCount = rows;
        int count = rows * cols;
        for (int i = 0; i < count; i++)
        {
            GameObject slot = Instantiate(slotPrefab, transform, true);
        }

    }
}
