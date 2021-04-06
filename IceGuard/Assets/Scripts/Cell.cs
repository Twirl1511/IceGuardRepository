using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell : MonoBehaviour
{
    public State currentState;

    private bool isCollisioned = false;
    public enum State
    {
        Clear,
        PlayerOcupied,
        MeteoriteIsComming,
        Mine,
        RepairBeam
    }
    private int number;

    private void Start()
    {
        currentState = State.Clear;
        InvokeRepeating(nameof(SetClear), 1, 1);
    }

    private void FixedUpdate()
    {
        

    }

    private void SetClear()
    {
        if (!isCollisioned)
        {
            currentState = State.Clear;
        }
        if (currentState != State.Clear)
        {
            isCollisioned = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isCollisioned = true;

        if (other.CompareTag("Player"))
        {
            currentState = State.PlayerOcupied;
        }
        else
        {
            if (other.CompareTag("Mine"))
            {
                currentState = State.Mine;
                return;
            }
            
        }

    }

  
    
}
