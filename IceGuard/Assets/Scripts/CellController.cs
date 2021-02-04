using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public Cell[] Cells;
    public static Cell[,] CellDouble = new Cell[6,6];
    private void Awake()
    {
        int j = 0;
        int k = 0;
        for (int i = 0; i < Cells.Length; i++)
        {
            if(i % 6 == 0 && i != 0)
            {
                j++;
                k = 0;
            }
            CellDouble[j, k] = Cells[i];
            //Debug.Log($"CellDouble[{j}, {k}]" + CellDouble[j, k]);
            k++;
        }   



    }



}
