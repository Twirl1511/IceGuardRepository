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

    public void MinusHP()
    {
        foreach (var e in HPs)
        {
            e.SetActive(false);
        }
        if (HitPoints - 1 < 0) return;
        HPs[HitPoints - 1].SetActive(true);
    }
    private void Update()
    {
        MinusHP();
    }



}
