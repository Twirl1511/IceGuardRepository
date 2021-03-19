using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell : MonoBehaviour
{
    public State currentState;

    private bool isCollisioned = false;
    public enum State
    {
        PlayerOcupied,
        MeteoriteIsComming,
        Mine,
        RepairBeam,
        Clear
    }
    private int number;

    private void Start()
    {
        currentState = State.Clear;
    }

    private void FixedUpdate()
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
            if (other.CompareTag("Meteorite"))
            {
                currentState = State.MeteoriteIsComming;
                Debug.Log("if (other.CompareTag(Meteorite))");
            }
            else
            {
                if (other.CompareTag("Mine"))
                {
                    currentState = State.Mine;
                }
                else
                {
                    if (other.CompareTag("RepairBeam"))
                    {
                        currentState = State.RepairBeam;
                    }
                }
            }
        }
        


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
        currentState = State.Clear;
    }
    
}
