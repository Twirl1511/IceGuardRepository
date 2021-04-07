using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairDroneController : MonoBehaviour
{
    public static RepairDroneController singleton;
    [Header("Repair Drone")]
    [SerializeField] public float timeBeforeRepairMin;
    [SerializeField] public float timeBeforeRepairMax;
    [SerializeField] public GameObject RepairDrone;
    [SerializeField] public float ProgressionTime;
    [Header("Other")]
    [SerializeField] public MeteoriteController _meteoriteController;

    public enum DroneStates
    {
        Ready,
        NotReady
    }
    public DroneStates DroneState;

    private void Start()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    
    

    private void Update()
    {
        /// запускаем лечащего дрона
        if (DroneState == DroneStates.Ready)
        {
            StartCoroutine(DroneAppearDelay());
        }
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
        foreach (var e in CellController.CellDouble)
        {
            if (e.currentState == Cell.State.Clear)
            {
                PropperCellsArray.Add(e.transform.position);
            }
        }


        for (int i = 0; i < _meteoriteController.MeteoriteOcupiedPositions.Length; i++)
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




}
