﻿using System.Collections;
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
    [SerializeField] private MeteoriteController _meteoriteController;
    [SerializeField] private GameObject GameUI;
    [Header("Repair Drone")]
    [SerializeField] private float timeBeforeRepairMin;
    [SerializeField] private float timeBeforeRepairMax;
    [SerializeField] private GameObject RepairDrone;
    [SerializeField] private float ProgressionTime;
    [Header("Tutorial")]
    [SerializeField] private TutorialScript tutorialScript;
    [SerializeField] private GameObject TipsPanel;
    [SerializeField] private Text TipsText;


    private enum DroneStates
    {
        Ready,
        NotReady
    }
    private DroneStates DroneState;
    
    private void Start()
    {
        DroneState = DroneStates.NotReady;
        StartTipsPanel.SetActive(false);
        TipsPanel.SetActive(false);
        if (!TutorialScript.isTutorialFinished)
        {
            Debug.Log("проверяем первая ли игра при старте скрипта меню контрол");
            StartTipsPanel.SetActive(true);
            UIManager.GameState = UIManager.GameStates.Pause;
            _firstGame = false;
            PlayStages();
        }
        else
        {
            DroneState = DroneStates.Ready;
            CreatePlayer();
            _meteoriteController.gameObject.SetActive(true);
            GameUI.SetActive(true);
            StartTipsPanel.SetActive(false);
            Time.timeScale = 1;
        }
        
    }

    public void PlayStages()
    {
        if (StaticTutorialStage.Stage == StaticTutorialStage.TutorStages.First)
        {

            PlayerHitPoints.HitPoints = 2;
            UIManager.GameState = UIManager.GameStates.Play;
            GameUI.SetActive(true);
            StartTipsPanel.SetActive(false);
            Time.timeScale = 1;
            tutorialScript.FirstStep();
        }
        else if (StaticTutorialStage.Stage == StaticTutorialStage.TutorStages.Second)
        {

            StartTipsPanel.SetActive(false);
            Debug.Log("StartTipsPanel = " + StartTipsPanel.activeSelf);
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
            StartTipsPanel.SetActive(false);
            Time.timeScale = 1;
            tutorialScript.ThirdStep();
        }
    }

    private void Update()
    {
        if(PlayerHitPoints.GetHitPoints() <= 0)
        {
            
            GameOverPanel.SetActive(true);
            Days.text = DayCounter.GetComponent<DayCounter>().Counter.text;
            Time.timeScale = 0;
            if (!TutorialScript.isTutorialFinished)
            {
                TipsPanel.SetActive(true);

                if (StaticTutorialStage.Stage == StaticTutorialStage.TutorStages.First)
                {
                    TipsText.text = tutorialScript.FirstPositionFaile();
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
        /// запускаем лечащего дрона
        if(DroneState == DroneStates.Ready)
        {
            StartCoroutine(DroneAppearDelay());
        }
    }

    
    private void CreatePlayer()
    {
        int x = Random.Range(-2, 3);
        int z = Random.Range(-3, 3);

        GameObject.Instantiate(Resources.Load("Player"), new Vector3(x, 0, z), Quaternion.identity);
        GameObject.Instantiate(Resources.Load("PlayerGhost"), new Vector3(x, 0, z), Quaternion.identity);
    }

    IEnumerator DroneAppearDelay()
    {
        DroneState = DroneStates.NotReady;
        yield return new WaitForSeconds(Random.Range(timeBeforeRepairMin, timeBeforeRepairMax));
        timeBeforeRepairMin += ProgressionTime;
        timeBeforeRepairMax += ProgressionTime;
        CreateRepairTarget();
        DroneState = DroneStates.Ready;
    }

    private void CreateRepairTarget()
    {
        

        List<Vector3> PropperCellsArray = new List<Vector3>();
        foreach(var e in CellController.CellDouble)
        {
            if(e.currentState == Cell.State.Clear)
            {
                PropperCellsArray.Add(e.transform.position);
            }
        }

        
        for(int i = 0; i < _meteoriteController.MeteoriteOcupiedPositions.Length; i++)
        {
            if (_meteoriteController.MeteoriteOcupiedPositions[i] != null)
            {
                PropperCellsArray.Remove(_meteoriteController.MeteoriteOcupiedPositions[i]);
            }
        }



        int randomCell = Random.Range(0, PropperCellsArray.Count);

        float x = PropperCellsArray[randomCell].x;
        float z = PropperCellsArray[randomCell].z;
        Vector3 position = new Vector3(x, 0, z);


        _meteoriteController.MeteoriteOcupiedPositions[_meteoriteController.MeteoriteCounter] = position;
        _meteoriteController.MeteoriteCounter++;
        if (_meteoriteController.MeteoriteCounter > _meteoriteController.MeteoriteOcupiedPositions.Length - 1)
            _meteoriteController.MeteoriteCounter = 0;





        GameObject.Instantiate(Resources.Load(RepairDrone.name), position, Quaternion.identity);
    }

    public void StartRealGame()
    {
        DroneState = DroneStates.Ready;
        _meteoriteController.gameObject.SetActive(true);
    }

    public void OnClickRestart()
    {
        if (TutorialScript.isTutorialFinished)
        {
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
            DroneState = DroneStates.Ready;
            CreatePlayer();
            _meteoriteController.gameObject.SetActive(true);
            GameUI.SetActive(true);
            StartTipsPanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            PlayerHitPoints.HitPoints = 2;
            UIManager.GameState = UIManager.GameStates.Play;
            GameUI.SetActive(true);
            StartTipsPanel.SetActive(false);
            Time.timeScale = 1;
            tutorialScript.FirstStep();
        }

    }

    public void OnClickExit()
    {
        Application.Quit();
    }


   
}
