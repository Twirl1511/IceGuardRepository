﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TutorialScript : MonoBehaviour
{
    public static bool isTutorialFinished;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject PlayerGhost;
    [SerializeField] private GameObject MeteoriteTarget;
    [SerializeField] private GameObject RepairDrone;
    [SerializeField] private float FirstMeteoriteTimer;
    public string[] TutorTipsArray;
    GameObject player;
    GameObject playerGhost;
    GameObject meteorite;
    GameObject repairDrone;
    public Cell FirstMeteoriteCell;
    [SerializeField] private GameObject GameOverPanel;
    public float timer;
    public bool isRepairDroneReleased = false;
    private float RepairDroneTimer;
    [SerializeField] private MenuController menuController;
    [Header("LaserTutorial")]
    [SerializeField] private GameObject _laser;
    private GameObject _target;
    private float _timeToRepair;

    void Start()
    {
        isTutorialFinished = System.Convert.ToBoolean(PlayerPrefs.GetString("save", "false"));
    }
    private void FixedUpdate()
    {
        if (_target != null)
        {
            _laser.transform.LookAt(_target.transform, Vector3.forward);
        }
    }
    void Update()
    {
        if(isRepairDroneReleased)
        {
            timer += Time.deltaTime;
            if(timer >= RepairDroneTimer)
            {
                isRepairDroneReleased = false;
                if (PlayerHitPoints.HitPoints < 3)
                {
                    PlayerHitPoints.HitPoints = 0;
                }
                else
                {
                    isTutorialFinished = true;
                    PlayerPrefs.SetString("save", isTutorialFinished.ToString());
                    menuController.StartRealGame();
                }               
            }
        }   
    }
    
    public void FirstStep()
    {
        StaticTutorialStage.Stage = StaticTutorialStage.TutorStages.First;
        Vector3 position = new Vector3(0, 0, -2);
        player = Instantiate(Resources.Load(Player.name), position, Quaternion.identity) as GameObject;
        playerGhost = Instantiate(Resources.Load(PlayerGhost.name), position, Quaternion.identity) as GameObject;
        position = new Vector3(0, 0, 1);
        meteorite = Instantiate(Resources.Load(MeteoriteTarget.name), position, Quaternion.identity) as GameObject;
        meteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = FirstMeteoriteTimer;

        StartCoroutine(GoToSecondStep());
    }

    IEnumerator GoToSecondStep()
    {
        yield return new WaitForSeconds(8);
        if(PlayerHitPoints.HitPoints != 0)
        {
            SecondStep();
        }     
    }
    IEnumerator GoToThirdStep()
    {
        yield return new WaitForSeconds(9);
        if (PlayerHitPoints.HitPoints != 0)
        {
            ThirdStep();
        }
    }


    public void SecondStep()
    {
        StaticTutorialStage.Stage = StaticTutorialStage.TutorStages.Second;
        Vector3 position = new Vector3(1, 0, 1);
        try
        {
            player.SetActive(true);
        }
        catch
        {
            player = Instantiate(Resources.Load(Player.name), position, Quaternion.identity) as GameObject;
            playerGhost = Instantiate(Resources.Load(PlayerGhost.name), position, Quaternion.identity) as GameObject;
        }
        position = new Vector3(2, 0, -2);
        meteorite = Instantiate(Resources.Load(MeteoriteTarget.name), position, Quaternion.identity) as GameObject;
        meteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = 6;

        StartCoroutine(GoToThirdStep());
    }

    public void ThirdStep()
    {
        StaticTutorialStage.Stage = StaticTutorialStage.TutorStages.Third;
        Vector3 position = new Vector3(2, 0, -3);

        try
        {
            player.SetActive(true);
        }
        catch
        {
            player = Instantiate(Resources.Load(Player.name), position, Quaternion.identity) as GameObject;
            playerGhost = Instantiate(Resources.Load(PlayerGhost.name), position, Quaternion.identity) as GameObject;
        }

        List<Cell> PropperCells = new List<Cell>();

        foreach(var e in CellController.CellDouble)
        {
            if(e.currentState == Cell.State.Clear)
            {
                PropperCells.Add(e);
            }
        }

        int randomCell = Random.Range(0, PropperCells.Count);

        position = new Vector3(PropperCells[randomCell].transform.position.x, 0, PropperCells[randomCell].transform.position.z);

        repairDrone = Instantiate(Resources.Load(RepairDrone.name), position, Quaternion.identity) as GameObject;
        repairDrone.GetComponent<RepairTargetScript>().timeToLiveMin = 5;
        repairDrone.GetComponent<RepairTargetScript>().timeToLiveMax = 10;
        _target = repairDrone;
        StartCoroutine(TestWait(1));


        _laser.SetActive(true);
        StartCoroutine(LaterLaserAnimation());
    }


    IEnumerator LaterLaserAnimation()
    {
        yield return new WaitForSeconds(1);
        _timeToRepair = _target.GetComponent<RepairTargetScript>().TimeThenHeal - 1;
        yield return new WaitForSeconds(_timeToRepair);
        _laser.transform.GetChild(0).DOScaleY(1, 1);
        yield return new WaitForSeconds(1);
        _laser.transform.GetChild(0).DOScaleY(0.05f, 0.15f);
        yield return new WaitForSeconds(1.5f);
        _laser.SetActive(false);
    }


    IEnumerator TestWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        RepairDroneTimer = repairDrone.GetComponent<RepairTargetScript>().TimeThenHeal;
        _timeToRepair = RepairDroneTimer;
        Debug.LogError("TutorialTime = " + RepairDroneTimer);
        isRepairDroneReleased = true;
    }

    public string FailedStage()
    {
        string s = TutorTipsArray[Random.Range(0, TutorTipsArray.Length)];
        if (player.transform.position == new Vector3(0, 0, -2))
        {
            /// не сдвинулся с места
            return TutorTipsArray[0];
        }
        if (player.transform.position == new Vector3(0, 0, 1))
        {
            /// встал под метеорит
            return TutorTipsArray[1];
        }
        if (player.transform.position != new Vector3(0, 0, -2) && PlayerHitPoints.HitPoints == 0)
        {
            /// не перекрыл мишень, но сдвинулся с места
            return TutorTipsArray[2];
        }
        return s;
    }

    public string SecondPositionFaile()
    {
        string s = TutorTipsArray[Random.Range(0, TutorTipsArray.Length)];
        if (PlayerHitPoints.HitPoints == 0 && NewPlayerController.TutorialMinesCounter <= 1 && player.transform.position != new Vector3(2, 0, -2))
        {
            /// не загородил миной
            return TutorTipsArray[3];
        }
        if (player.transform.position == new Vector3(2, 0, -2) && PlayerHitPoints.HitPoints == 0)
        {
            /// встал под метеорит
            return TutorTipsArray[1];
        }
        if (PlayerHitPoints.HitPoints == 0 && NewPlayerController.TutorialMinesCounter > 1)
        {
            /// подорвался на минах
            return TutorTipsArray[4];
        }
        return s;
    }

    public string ThirdPositionFaile()
    {
        string s = TutorTipsArray[Random.Range(0, TutorTipsArray.Length)];
        if(PlayerHitPoints.HitPoints == 0 && NewPlayerController.TutorialMinesCounter > 1)
        {
            /// подорвался на минах
            return TutorTipsArray[4];
        }
        if (PlayerHitPoints.HitPoints == 0)
        {
            /// не подлечился
            return TutorTipsArray[5];
        }
        return s;
    }

}
