using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    
    void Awake()
    {
        int x = Random.Range(-2, 3);
        int z = Random.Range(-3, 3);
        GameObject.Instantiate(Resources.Load("Player"), new Vector3(x, 0, z), Quaternion.identity);
    }

    
}
