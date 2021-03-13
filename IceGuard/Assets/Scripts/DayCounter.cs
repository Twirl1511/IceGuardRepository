using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCounter : MonoBehaviour
{
    public Text Counter;
    [SerializeField] private int SecondsInDay;
    public float timer;
    void Start()
    {
        timer = 0f;
    }
    void Update()
    {
        timer += Time.deltaTime;
        Counter.text = (timer / SecondsInDay).ToString("0");
    }
}
