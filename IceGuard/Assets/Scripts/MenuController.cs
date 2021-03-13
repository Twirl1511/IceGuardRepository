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
    public static bool _firstGame = true;
    [SerializeField] private GameObject StartTipsPanel;
    [SerializeField] private GameObject MeteoriteController;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject Cells;
    private void Start()
    {   
        /// сделать логику паузы !!!!!!!!!!
        StartTipsPanel.SetActive(false);
        if (_firstGame)
        {
            StartTipsPanel.SetActive(true);
            Time.timeScale = 0;
            _firstGame = false;
        }
        else
        {
            CreatePlayer();
            MeteoriteController.SetActive(true);
            GameUI.SetActive(true);
            Cells.SetActive(true);
        }
    }
    private void Update()
    {
        if(PlayerHitPoints.GetHitPoints() <= 0)
        {
            GameOverPanel.SetActive(true);
            Days.text = DayCounter.GetComponent<DayCounter>().Counter.text;
            Time.timeScale = 0;
        }
    }

    private void CreatePlayer()
    {
        int x = Random.Range(-2, 3);
        int z = Random.Range(-3, 3);

        GameObject.Instantiate(Resources.Load("Player"), new Vector3(x, 0, z), Quaternion.identity);
        GameObject.Instantiate(Resources.Load("PlayerGhost"), new Vector3(x, 0, z), Quaternion.identity);
    }

    public void OnClickRestart()
    {
        PlayerHitPoints.RestartHP();
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
