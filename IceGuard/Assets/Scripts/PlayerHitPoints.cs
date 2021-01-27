using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitPoints : MonoBehaviour
{

    public static int HitPoints = 3;
    [SerializeField] private GameObject[] HPs;
    void Start()
    {
        HitPoints = 3;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var e in HPs)
        {
            e.SetActive(false);
        }
        HPs[HitPoints-1].SetActive(true);
       
    }

   
}
