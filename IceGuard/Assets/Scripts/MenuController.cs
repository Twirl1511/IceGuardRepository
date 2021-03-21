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
    //[SerializeField] private GameObject Cells;
    [Header("Repair Drone")]
    [SerializeField] private float timeBeforeRepairMin;
    [SerializeField] private float timeBeforeRepairMax;
    [SerializeField] private GameObject RepairDrone;
    [SerializeField] private float ProgressionTime;

    private enum DroneStates
    {
        Ready,
        NotReady
    }
    private DroneStates DroneState;
    
    private void Start()
    {
        DroneState = DroneStates.NotReady;
        /// сделать логику паузы !!!!!!!!!!
        StartTipsPanel.SetActive(false);
        if (_firstGame)
        {
            StartTipsPanel.SetActive(true);
            UIManager.GameState = UIManager.GameStates.Pause;
            _firstGame = false;
        }
        else
        {
            DroneState = DroneStates.Ready;
            CreatePlayer();
            MeteoriteController.SetActive(true);
            GameUI.SetActive(true);
            //Cells.SetActive(true);
            StartTipsPanel.SetActive(false);
            Time.timeScale = 1;
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

        List<Cell> PropperCellsArray = new List<Cell>();
        foreach(var e in CellController.CellDouble)
        {
            if(e.currentState == Cell.State.Clear)
            {
                PropperCellsArray.Add(e);
            }
        }
        int randomCell = Random.Range(0, PropperCellsArray.Count);

        float x = PropperCellsArray[randomCell].transform.position.x;
        float z = PropperCellsArray[randomCell].transform.position.z;

        GameObject.Instantiate(Resources.Load(RepairDrone.name), new Vector3(x, 0, z), Quaternion.identity);
    }

    public void OnClickRestart()
    {
        PlayerHitPoints.RestartHP();
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        SceneManager.LoadScene("TestScene");
        DayCounter.GetComponent<DayCounter>().timer = 0;
    }

    public void OnClickGO()
    {
        UIManager.GameState = UIManager.GameStates.Play;
        DroneState = DroneStates.Ready;
        CreatePlayer();
        MeteoriteController.SetActive(true);
        GameUI.SetActive(true);
        //Cells.SetActive(true);
        StartTipsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickExit()
    {
        Application.Quit();
    }


   
}
