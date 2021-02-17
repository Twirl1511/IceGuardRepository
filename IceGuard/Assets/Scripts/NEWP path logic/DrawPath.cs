using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    public static List<LinePathCellState> PathStates = new List<LinePathCellState>();
    public static Vector3[] adjacentPositionsForCell = new Vector3[4];

    private void Start()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log(gameObject.name);
    }

    //public bool IsCellAdjacentToPrevoiusOne(Vector3 currentPosition)
    //{
    //    adjacentPositionsForCell[0] = previousCell + Vector3.left;
    //    adjacentPositionsForCell[1] = previousCell + Vector3.left * -1;
    //    adjacentPositionsForCell[2] = previousCell + Vector3.forward;
    //    adjacentPositionsForCell[3] = previousCell + Vector3.forward * -1;

    //    foreach (var e in adjacentPositionsForCell)
    //    {
    //        if (e == currentPosition) return true;
    //    }

    //    return false;
    //}
}
