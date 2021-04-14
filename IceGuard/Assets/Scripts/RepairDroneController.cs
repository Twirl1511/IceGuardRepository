using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;
using DG.Tweening;


public class RepairDroneController : MonoBehaviour
{
    public static RepairDroneController singleton;
    [Header("Repair Drone")]
    [SerializeField] public float timeBeforeRepairMin;
    [SerializeField] public float timeBeforeRepairMax;
    [SerializeField] public GameObject RepairDrone;
    [SerializeField] public float ProgressionTime;
    [Header("MeteoriteController")]
    [SerializeField] public MeteoriteController _meteoriteController;
    [Header("Laser")]
    [SerializeField] private GameObject _laser;
    [SerializeField] private float _laserLength;
    private GameObject _target;
    private float _timeToRepair;

    public enum DroneStates
    {
        Ready,
        NotReady
    }
    public DroneStates DroneState;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
        _laser.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(_target != null)
        {
            _laser.transform.LookAt(_target.transform, Vector3.forward);
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





        _target = GameObject.Instantiate(Resources.Load(RepairDrone.name), position, Quaternion.identity) as GameObject;
        
        _laser.SetActive(true);
        StartCoroutine(LaterLaserAnimation());
    }

    

    
    IEnumerator LaterLaserAnimation()
    {
        yield return new WaitForSeconds(1);
        _timeToRepair = _target.GetComponent<RepairTargetScript>().TimeThenHeal - 1;
        yield return new WaitForSeconds(_timeToRepair);
        _laser.transform.GetChild(0).DOScaleY(_laserLength, 0.7f);
        yield return new WaitForSeconds(1);
        _laser.transform.GetChild(0).DOScaleY(0.05f, 0.1f);
        ////yield return new WaitForSeconds(1.5f);
        _laser.SetActive(false);
    }

}
