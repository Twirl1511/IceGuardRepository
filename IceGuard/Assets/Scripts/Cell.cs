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
        ForceField,
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
        switch (other.tag)
        {
            case "Player":
                currentState = State.PlayerOcupied;
                break;
            case "ForceField":
                currentState = State.ForceField;
                break;
            case "MeteoriteIsComming":
                currentState = State.MeteoriteIsComming;
                break;
            default:
                currentState = State.Clear;
                break;
        }
    }

}
