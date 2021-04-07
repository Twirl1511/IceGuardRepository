using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCounter : MonoBehaviour
{
    public Text Counter;
    [SerializeField] private int SecondsInDay;
    [HideInInspector] public int timer;
    void Start()
    {
        InvokeRepeating(nameof(DayCount), 1, SecondsInDay);
    }

    private void DayCount()
    {
        timer++;
        Counter.text = timer.ToString();
    }
}
