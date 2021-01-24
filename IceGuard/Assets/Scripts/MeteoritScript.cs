using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritScript : MonoBehaviour
{
    public static int MAX_NUMBER_OF_METEORITS = 1;
    public GameObject[] AllCells = new GameObject[36];

    // Update is called once per frame
    void Update()
    {
        if (MAX_NUMBER_OF_METEORITS < 1)
        {
            int randomPosition = Random.Range(0, 36);
        }
    }
}
