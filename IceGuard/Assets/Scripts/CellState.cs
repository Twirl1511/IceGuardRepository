﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellState : MonoBehaviour
{
    private bool isStartedFromAdjacentPosition = false;
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
                Instantiate(Resources.Load("pathPoint"), transform.position, Quaternion.identity);
            }else if(PlayerControllerDrawPath.queuePath.Count > 0)
            {
                PlayerControllerDrawPath.queuePath.Enqueue(transform.position);
                Instantiate(Resources.Load("pathPoint"), transform.position, Quaternion.identity);
            }
        }   
    }

}
