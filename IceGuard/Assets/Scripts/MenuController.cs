using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;
    private void Update()
    {
        if(PlayerHitPoints.HitPoints <= 0)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnClickRestart()
    {
        PlayerHitPoints.HitPoints = 3;
        Time.timeScale = 1;
        SceneManager.LoadScene("TestScene");
        GameOverPanel.SetActive(false);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
