using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCounter : MonoBehaviour
{
    [SerializeField] public Text Counter;
    [SerializeField] private int SecondsInDay;
    public float timer;
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Counter.text = (timer / SecondsInDay).ToString("0");
    }
}
