using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanels : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _resumeButtonSettingsPanel;
    [SerializeField] private GameObject _backButtonSettingsPanel;
    [SerializeField] private GameObject _resumeFromStartPanel;
    [SerializeField] private GameObject _gameUI;

    public void OnClickScorePanel()
    {
        _startPanel.SetActive(false);
        _scorePanel.SetActive(true);
    }
    public void OnClickScorePanelFromGameOver()
    {
        _gameOverPanel.SetActive(false);
        _scorePanel.SetActive(true);
    }
    public void OnClickSettingsPanel()
    {
        _startPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }
    public void OnClickSettingsFromGame()
    {
        _resumeButtonSettingsPanel.SetActive(true);
        _backButtonSettingsPanel.SetActive(false);
        _settingsPanel.SetActive(true);

        PauseManager.GameState = PauseManager.GameStates.Pause;
        Time.timeScale = 0;
    }

    public void OnClickResumeFromSettings()
    {
        _resumeButtonSettingsPanel.SetActive(false);
        _backButtonSettingsPanel.SetActive(true);
        _settingsPanel.SetActive(false);

        PauseManager.GameState = PauseManager.GameStates.Play;
        Time.timeScale = 1;
    }

    public void OnClickBackFromScorePanel()
    {
        _startPanel.SetActive(true);
        _scorePanel.SetActive(false);
    }
    public void OnClickBackFromSettingsPanel()
    {
        _startPanel.SetActive(true);
        _settingsPanel.SetActive(false);
    }

    public void OnClickHome(GameObject panel)
    {
        panel.SetActive(false);
        _startPanel.SetActive(true);
        
        if(PlayerHitPoints.HitPoints > 0)
        {
            _resumeFromStartPanel.SetActive(true);
        }
        else
        {
            _resumeFromStartPanel.SetActive(false);
        }
    }
    public void OnClickResumeFromStart()
    {
        _startPanel.SetActive(false);
        _gameUI.SetActive(true);
        PauseManager.GameState = PauseManager.GameStates.Play;
        Time.timeScale = 1;
    }
}
