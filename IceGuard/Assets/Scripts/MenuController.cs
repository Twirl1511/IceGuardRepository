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
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private MeteoriteController _meteoriteController;
    [SerializeField] private GameObject GameUI;
    [Header("Tutorial")]
    [SerializeField] private TutorialScript tutorialScript;
    [SerializeField] private GameObject TipsPanel;
    [SerializeField] private Text TipsText;


    private void Start()
    {
        GameUI.SetActive(false);
        Time.timeScale = 0;
        RepairDroneController.singleton.DroneState = RepairDroneController.DroneStates.NotReady;
        _startPanel.SetActive(false);
        TipsPanel.SetActive(false);
        if (!TutorialScript.isTutorialFinished)
        {
            Debug.Log("проверяем первая ли игра при старте скрипта меню контрол");
            _startPanel.SetActive(true);
            UIManager.GameState = UIManager.GameStates.Pause;
            _firstGame = false;
            PlayStages();
        }
        else
        {
            RepairDroneController.singleton.DroneState = RepairDroneController.DroneStates.Ready;
            CreatePlayer();
            _meteoriteController.gameObject.SetActive(true);
            GameUI.SetActive(true);
            _startPanel.SetActive(false);
            Time.timeScale = 1;
        }
        
    }

    private void PlayStages()
    {
        if (StaticTutorialStage.Stage == StaticTutorialStage.TutorStages.First)
        {

            PlayerHitPoints.HitPoints = 2;
            UIManager.GameState = UIManager.GameStates.Play;
            GameUI.SetActive(true);
            _startPanel.SetActive(false);
            Time.timeScale = 1;
            tutorialScript.FirstStep();
        }
        else if (StaticTutorialStage.Stage == StaticTutorialStage.TutorStages.Second)
        {

            _startPanel.SetActive(false);
            Debug.Log("StartTipsPanel = " + _startPanel.activeSelf);
            PlayerHitPoints.HitPoints = 2;
            UIManager.GameState = UIManager.GameStates.Play;
            GameUI.SetActive(true);

            Time.timeScale = 1;
            tutorialScript.SecondStep();
        }
        else if (StaticTutorialStage.Stage == StaticTutorialStage.TutorStages.Third)
        {
            PlayerHitPoints.HitPoints = 2;
            UIManager.GameState = UIManager.GameStates.Play;
            GameUI.SetActive(true);
            _startPanel.SetActive(false);
            Time.timeScale = 1;
            tutorialScript.ThirdStep();
        }
    }

    private void Update()
    {
        if(PlayerHitPoints.GetHitPoints() <= 0)
        {
            GameUI.SetActive(false);
            GameOverPanel.SetActive(true);
            Days.text = DayCounter.GetComponent<DayCounter>().Counter.text;
            Time.timeScale = 0;
            if (!TutorialScript.isTutorialFinished)
            {
                TipsPanel.SetActive(true);

                if (StaticTutorialStage.Stage == StaticTutorialStage.TutorStages.First)
                {
                    TipsText.text = tutorialScript.FailedStage();
                }
                if (StaticTutorialStage.Stage == StaticTutorialStage.TutorStages.Second)
                {
                    TipsText.text = tutorialScript.SecondPositionFaile();
                }
                if (StaticTutorialStage.Stage == StaticTutorialStage.TutorStages.Third)
                {
                    tutorialScript.isRepairDroneReleased = true;
                    tutorialScript.timer = 0;
                    TipsText.text = tutorialScript.ThirdPositionFaile();
                }


            }
        }
        
    }

    
    private void CreatePlayer()
    {
        int x = Random.Range(-2, 3);
        int z = Random.Range(-3, 3);

        GameObject.Instantiate(Resources.Load("Player"), new Vector3(x, 0, z), Quaternion.identity);
        GameObject.Instantiate(Resources.Load("PlayerGhost"), new Vector3(x, 0, z), Quaternion.identity);
    }


    public void StartRealGame()
    {
        RepairDroneController.singleton.DroneState = RepairDroneController.DroneStates.Ready;
        _meteoriteController.gameObject.SetActive(true);
    }

    public void OnClickRestart()
    {
        if (TutorialScript.isTutorialFinished)
        {
            GameUI.SetActive(true);
            PlayerHitPoints.RestartHP();
            Time.timeScale = 1;
            GameOverPanel.SetActive(false);
            SceneManager.LoadScene("TestScene");
            DayCounter.GetComponent<DayCounter>().timer = 0;
        }
        else
        {
            SceneManager.LoadScene("TestScene");
        }
    }


    

    public void OnClickGO()
    {
        if (TutorialScript.isTutorialFinished)
        {
            UIManager.GameState = UIManager.GameStates.Play;
            RepairDroneController.singleton.DroneState = RepairDroneController.DroneStates.Ready;
            CreatePlayer();
            _meteoriteController.gameObject.SetActive(true);
            GameUI.SetActive(true);
            _startPanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            PlayerHitPoints.HitPoints = 2;
            UIManager.GameState = UIManager.GameStates.Play;
            GameUI.SetActive(true);
            _startPanel.SetActive(false);
            Time.timeScale = 1;
            tutorialScript.FirstStep();
        }

    }

    public void OnClickExit()
    {
        Application.Quit();
    }


   
}
