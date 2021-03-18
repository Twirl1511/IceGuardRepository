using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Meteorite
{
    [Range(0, 20)]
    public int AppearTimeMin;
    [Range(0, 20)]
    public int AppearTimeMax;
    [Range(0, 20)]
    public int BoomTimeMin;
    [Range(0, 20)]
    public int BoomTimeMax;

}


public class MeteoriteController : MonoBehaviour
{
    public static int meteoriteCounter;

    [SerializeField] private GameObject MeteoriteTarget;
    [SerializeField] private Meteorite[] MeteoriteArray;
    [SerializeField] private float timerForAnglePositions;
    private float _timer;
    private Vector3[] AngelPositions = new Vector3[4] {
        new Vector3(3,0,2), 
        new Vector3(-2, 0, 2),
        new Vector3(-2, 0, -3),
        new Vector3(3, 0, -3)};
 
    private enum States
    {
        Ready,
        Stop
    }
    private States State;

    void Start()
    {
        State = States.Ready;
        meteoriteCounter = 0;
    }


    void Update()
    {
        if(meteoriteCounter < 0)
        {
            meteoriteCounter = 0;
        }

        if(meteoriteCounter < 3 && State == States.Ready)
        {
            State = States.Stop;
            CheckMeteorites();
        }
        _timer += Time.deltaTime;
    }

  
    private void CheckMeteorites()
    {
        float randomAppearTime;
        switch (meteoriteCounter)
        {
            case 0:
                randomAppearTime = CalculateRandomAppearTime(meteoriteCounter);
                StartCoroutine(LaterMeteoriteStart(randomAppearTime, meteoriteCounter));
                meteoriteCounter++;
                //Debug.Log($"Через {randomAppearTime} спавним {meteoriteCounter} метеорит!");
                break;
            case 1:
                randomAppearTime = CalculateRandomAppearTime(meteoriteCounter);
                StartCoroutine(LaterMeteoriteStart(randomAppearTime, meteoriteCounter));
                meteoriteCounter++;
                //Debug.Log($"Через {randomAppearTime} спавним {meteoriteCounter} метеорит!");
                break;
            case 2:
                randomAppearTime = CalculateRandomAppearTime(meteoriteCounter);
                StartCoroutine(LaterMeteoriteStart(randomAppearTime, meteoriteCounter));
                meteoriteCounter++;
                //Debug.Log($"Через {randomAppearTime} спавним {meteoriteCounter} метеорит!");
                break;
            default:
                break;
        }
    }

    private float CalculateRandomAppearTime(int counter)
    {
        return Random.Range(MeteoriteArray[counter].AppearTimeMin, MeteoriteArray[counter].AppearTimeMax);
    }

    IEnumerator LaterMeteoriteStart(float seconds, int counter)
    {
        yield return new WaitForSeconds(seconds);
        CreateMeteorite(MeteoriteArray[counter], RandomPosition());
    }

    private Vector3 RandomPosition()
    {
        List<Cell> PropperCellsArray = new List<Cell>();
        foreach (var e in CellController.CellDouble)
        {
            if (e.currentState == Cell.State.Clear || e.currentState == Cell.State.ForceField)
            {
                PropperCellsArray.Add(e);
            }
        }
        if(_timer >= timerForAnglePositions)
        {
            _timer = 0;

        }

        int randomCell = Random.Range(0, PropperCellsArray.Count);

        float x = PropperCellsArray[randomCell].transform.position.x;
        float z = PropperCellsArray[randomCell].transform.position.z;

        Vector3 position = new Vector3(x, 0, z);

        foreach(var e in AngelPositions)
        {
            if(position == e)
            {
                // запускаем таймер
            }
        }

        return position;
    }

    


    private void CreateMeteorite(Meteorite meteorite, Vector3 position)
    {
        float timeToFall = Random.Range(meteorite.BoomTimeMin, meteorite.BoomTimeMax + 1); /// значения в интах, поэтому +1 чтобы в инспекторе проще было
        int addSecondsToBoom = 0;

        foreach (var e in CellController.CellDouble)
        {
            if (e.currentState == Cell.State.PlayerOcupied)
            {
                float x = Mathf.Abs(e.transform.position.x - position.x);
                float z = Mathf.Abs(e.transform.position.z - position.z);
                addSecondsToBoom = Mathf.RoundToInt((x + z + 1) * NewPlayerController.TimeToReachNextTile);
                Debug.Log("addSecondsToBoom = " + addSecondsToBoom);

            }
        }
        State = States.Ready;
        GameObject newMeteorite = GameObject.Instantiate(Resources.Load(MeteoriteTarget.name), position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        newMeteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = timeToFall + addSecondsToBoom;
        Debug.Log("спавним метеорит");
        
    }

}
