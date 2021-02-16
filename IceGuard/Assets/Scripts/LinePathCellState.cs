using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePathCellState : MonoBehaviour
{

    public bool IsPathDrown;

    void Start()
    {
        IsPathDrown = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            IsPathDrown = true;
        }
        else
        {
            IsPathDrown = false;
        }
    }
}
