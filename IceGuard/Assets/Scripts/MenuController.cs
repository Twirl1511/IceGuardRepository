using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Text Days;
    [SerializeField] private GameObject DayCounter;
    private void Update()
    {
        if(PlayerHitPoints.HitPoints <= 0)
        {
            GameOverPanel.SetActive(true);
            Days.text = DayCounter.GetComponent<DayCounter>().Counter.text;
            Time.timeScale = 0;
        }
    }

    public void OnClickRestart()
    {
        PlayerHitPoints.HitPoints = 3;
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        SceneManager.LoadScene("TestScene");
        DayCounter.GetComponent<DayCounter>().timer = 0;
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
