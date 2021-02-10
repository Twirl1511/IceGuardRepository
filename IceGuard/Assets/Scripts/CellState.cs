﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellState : MonoBehaviour
{
    public PlayerControllerDrawPath playerControllerDrawPath;
    private bool isStartedFromAdjacentPosition = false;
    public static Vector3 previousCell;
    public static Vector3[] adjacentPositionsForCell = new Vector3[4];
    public static bool _stopPath = true;
    private void Start()
    {
        _stopPath = true;
    }
    private void OnMouseOver()
    {
        if (Input.touchCount == 1)
        {
            foreach (var e in PlayerControllerDrawPath.adjacentPositionsForStart)
            {
                if (e.x == transform.position.x && e.z == transform.position.z)
                {
                    isStartedFromAdjacentPosition = true;
                }
            }
            if(playerControllerDrawPath.queuePath.Count == 0 && isStartedFromAdjacentPosition && _stopPath)
            {
                Debug.Log("!!!!!!!");
                StartCoroutine(WaitFor(PlayerControllerDrawPath.TimeToReachNextTile));
                _stopPath = false;
                playerControllerDrawPath.queuePath.Enqueue(transform.position);
                PlayerControllerDrawPath.allPathPoints.Add(Instantiate(Resources.Load("pathPoint 1"), transform.position, Quaternion.identity) as GameObject);
                previousCell = transform.position;
                
            }
            if((!_stopPath || playerControllerDrawPath.queuePath.Count > 0) 
                && IsCellAdjacentToPrevoiusOne(transform.position))
            {
                
                playerControllerDrawPath.queuePath.Enqueue(transform.position);
                PlayerControllerDrawPath.allPathPoints.Add(Instantiate(Resources.Load("pathPoint"), transform.position, Quaternion.identity) as GameObject);
                previousCell = transform.position;
            }
            isStartedFromAdjacentPosition = false;
        }   
        
    }


 
    public IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _stopPath = true;
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
