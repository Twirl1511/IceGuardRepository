using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private Text Counter;
    [SerializeField] private int SecondsInDay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Counter.text = (Time.time / SecondsInDay).ToString("0");
    }
}
