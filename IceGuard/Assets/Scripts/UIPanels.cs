using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanels : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _settingsPanel;
    void Start()
    {
        
    }

    public void OnClickScorePanel()
    {
        _startPanel.SetActive(false);
        _scorePanel.SetActive(true);
    }
    public void OnClickSettingsPanel()
    {
        _startPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }
    public void OnClickSettingsPanelFromGame()
    {
        UIManager.GameState = UIManager.GameStates.Pause;
        Time.timeScale = 0;
        _startPanel.SetActive(false);
        _settingsPanel.SetActive(true);
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
    }
}
