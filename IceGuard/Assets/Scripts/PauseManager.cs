using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameUI;

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
        _gameUI.SetActive(false);
        GameState = GameStates.Pause;
        Time.timeScale = 0;
        
        _pausePanel.SetActive(true);
    }

    public void PAUSEoff()
    {
        _gameUI.SetActive(true);
        GameState = GameStates.Play;
        Time.timeScale = 1;
        
        _pausePanel.SetActive(false);
    }

    public void ResetTutorial()
    {
        PlayerPrefs.DeleteAll();
    }
}
