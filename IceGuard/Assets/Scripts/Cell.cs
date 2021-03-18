using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public State currentState;
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

    public void SetNumber(int num)
    {
        number = num;
    }
    public int GetNumber()
    {
        return number;
    }


    
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("MeteoriteIsComming"))
        {
            currentState = State.MeteoriteIsComming;
        }
        else
        if (other.CompareTag("Player"))
        {
            currentState = State.PlayerOcupied;
        }
        else
        if (other.CompareTag("Mine"))
        {
            currentState = State.Mine;
        }
        else

        if (other.CompareTag("RepairBeam"))
        {
            currentState = State.RepairBeam;
        }
        else
        {
            currentState = State.Clear;
        }




        //    switch (other.tag)
        //{
        //    case "PlyaerOcupied":
        //        currentState = State.PlayerOcupied;
        //        break;
        //    case "ForceField":
        //        currentState = State.ForceField;
        //        break;
        //    case "MeteoriteIsComming":
        //        currentState = State.MeteoriteIsComming;
        //        break;
        //    case "RepairBeam":
        //        currentState = State.RepairBeam;
        //        break;
        //    default:
        //        currentState = State.Clear;
        //        break;
        //}
    }

}
