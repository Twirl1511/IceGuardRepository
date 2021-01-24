using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellState : MonoBehaviour
{
    private bool isStartedFromAdjacentPosition = false;
    public static Vector3 previousCell;
    public static Vector3[] adjacentPositionsForCell = new Vector3[4];
    private bool _stopPath = false;
    private void OnMouseEnter()
    {
        if (Input.touchCount == 1)
        {
            foreach (var e in PlayerControllerDrawPath.adjacentPositions)
            {
                if (e.x == transform.position.x && e.z == transform.position.z)
                {
                    isStartedFromAdjacentPosition = true;
                }
            }
            if(PlayerControllerDrawPath.queuePath.Count == 0 && isStartedFromAdjacentPosition)
            {
                PlayerControllerDrawPath.queuePath.Enqueue(transform.position);
                PlayerControllerDrawPath.allPathPoints.Add(Instantiate(Resources.Load("pathPoint 1"), transform.position, Quaternion.identity) as GameObject);
                previousCell = transform.position;
                
            }
            else if(PlayerControllerDrawPath.queuePath.Count > 0 && IsCellAdjacentToPrevoiusOne(transform.position))
            {
                PlayerControllerDrawPath.queuePath.Enqueue(transform.position);
                PlayerControllerDrawPath.allPathPoints.Add(Instantiate(Resources.Load("pathPoint"), transform.position, Quaternion.identity) as GameObject);
                previousCell = transform.position;
            }
            isStartedFromAdjacentPosition = false;
        }   
        
    }
    private void OnMouseDown()
    {
        if (Input.touchCount == 1 && PlayerControllerDrawPath.queuePath.Count > 0)
        {

            foreach (var e in PlayerControllerDrawPath.queuePath)
            {
                if (e == transform.position)
                {
                    PlayerControllerDrawPath.StopPosition = transform.position;
                }
                
            }
            

        }
    }



    public bool IsCellAdjacentToPrevoiusOne(Vector3 currentPosition)
    {
        adjacentPositionsForCell[0] = previousCell + Vector3.left;
        adjacentPositionsForCell[1] = previousCell + Vector3.left * -1;
        adjacentPositionsForCell[2] = previousCell + Vector3.forward;
        adjacentPositionsForCell[3] = previousCell + Vector3.forward * -1;

        foreach(var e in adjacentPositionsForCell)
        {
            if (e == currentPosition) return true;
        }

        return false;
    }
}
