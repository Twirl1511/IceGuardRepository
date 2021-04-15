using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitPoints : MonoBehaviour
{
    public static PlayerHitPoints singleton;
    public static int HitPoints = 3;
    
    [SerializeField] private GameObject[] HPArray;



    void Start()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }

        HitPoints = 3;
        
        foreach (var e in HPArray)
        {
            e.SetActive(true);
        }
        
    }

    private void Update()
    {
        CheckHP();
        /// читерская кнопка лечилка
        if (Input.GetKey(KeyCode.P))
        {
            RestartHP();
        }
    }
    private void CheckHP()
    {
        if (HitPoints <= 0)
        {
            HitPoints = 0;
            foreach (var e in HPArray)
            {
                e.SetActive(false);
            }
        }

        if (HitPoints == 3)
        {
            foreach (var e in HPArray)
            {
                e.SetActive(true);
            }
        }
        else
        {
            HPArray[HitPoints].SetActive(false);
        }
    }
    public static void MinusHP()
    {
        HitPoints--;
    }
    public static void MinusAllHP()
    {
        HitPoints = 0;
    }
    public static void RestartHP()
    {
        GlowingEffect.singleton.ChangeColor();
        HitPoints = 3;
    }
    public static int GetHitPoints()
    {
        return HitPoints;
    }


}
