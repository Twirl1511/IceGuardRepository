using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;

    public enum GameStates
    {
        Pause,
        Play
    }
    public static GameStates GameState;

    private void Start()
    {
        GameState = GameStates.Play;
    }

    public void PAUSEon()
    {
        
        GameState = GameStates.Pause;
        Time.timeScale = 0;
        
        PausePanel.SetActive(true);
    }

    public void PAUSEoff()
    {
        
        GameState = GameStates.Play;
        Time.timeScale = 1;
        
        PausePanel.SetActive(false);
    }

    public void ResetTutorial()
    {
        PlayerPrefs.DeleteAll();
    }
}
