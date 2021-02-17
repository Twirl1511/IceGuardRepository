using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePathCellState : MonoBehaviour
{

    public bool IsPathDrawn;

    void Start()
    {
        IsPathDrawn = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            IsPathDrawn = true;
        }
        else
        {
            IsPathDrawn = false;
        }
    }
}
