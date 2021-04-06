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
    }

    private void FixedUpdate()
    {
        //if (!isCollisioned)
        //{
        //    currentState = State.Clear;
        //}
        //if (currentState != State.Clear)
        //{
        //    isCollisioned = false;
        //}

    }


    private void OnTriggerStay(Collider other)
    {
        //isCollisioned = true;

        if (other.CompareTag("Player"))
        {
            currentState = State.PlayerOcupied;
        }
        //else
        //{
        //    if (other.CompareTag("Meteorite"))
        //    {
        //        currentState = State.MeteoriteIsComming;
        //        return;
        //    }
        //    else
        //    {
        //        if (other.CompareTag("Mine"))
        //        {
        //            currentState = State.Mine;
        //            return;
        //        }
        //        else
        //        {
        //            if (other.CompareTag("RepairBeam"))
        //            {
        //                currentState = State.RepairBeam;
        //            }
        //        }
        //    }
        //}
        


        //if (other.CompareTag("Player"))
        //{
        //    currentState = State.PlayerOcupied;
        //}
        //else
        //if (other.CompareTag("Meteorite"))
        //{
        //    currentState = State.MeteoriteIsComming;
        //}
        //else
        //if (other.CompareTag("Mine"))
        //{
        //    currentState = State.Mine;
        //}
        //else
        //if (other.CompareTag("RepairBeam"))
        //{
        //    currentState = State.RepairBeam;
        //}



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            currentState = State.Clear;
    }
    
}
