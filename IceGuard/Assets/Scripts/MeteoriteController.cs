using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Meteorite
{
    [Range(1, 20)]
    public int AppearTimeMin;
    [Range(1, 20)]
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
    private bool _angleTimer = false;
    [SerializeField] private Cell[] AngelPositions;
    private Vector3 _currentAnglePosition;
    public enum States
    {
        Ready,
        Stop
    }
    public static States State;

    void Start()
    {
        State = States.Ready;
        meteoriteCounter = -1;
    }


    void Update()
    {

        if (meteoriteCounter < 0)
        {
            meteoriteCounter = 0;
        }
        if (meteoriteCounter < 3 && State == States.Ready)
        {
            State = States.Stop;
            CheckMeteorites();
        }


        if (_angleTimer)
        {
            _timer += Time.deltaTime;
        }

        
    }

  
    private void CheckMeteorites()
    {
        float randomAppearTime;
        switch (meteoriteCounter)
        {
            case 0:
                randomAppearTime = CalculateRandomAppearTime(meteoriteCounter);
                StartCoroutine(LaterMeteoriteStart(randomAppearTime));
                //meteoriteCounter++;
                //Debug.Log($"Через {randomAppearTime} спавним {meteoriteCounter} метеорит!");
                break;
            case 1:
                randomAppearTime = CalculateRandomAppearTime(meteoriteCounter);
                StartCoroutine(LaterMeteoriteStart(randomAppearTime));
                //meteoriteCounter++;
                //Debug.Log($"Через {randomAppearTime} спавним {meteoriteCounter} метеорит!");
                break;
            case 2:
                randomAppearTime = CalculateRandomAppearTime(meteoriteCounter);
                StartCoroutine(LaterMeteoriteStart(randomAppearTime));
                //meteoriteCounter++;
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

    IEnumerator LaterMeteoriteStart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CreateMeteorite(MeteoriteArray[meteoriteCounter], RandomPosition());
        //Debug.Log($"Метеорит № {counter + 1}");
        
    }

    private Vector3 RandomPosition()
    {
        List<Cell> PropperCellsArray = new List<Cell>();
        Debug.LogError($"Клетки которые исключаем");
        foreach (var e in CellController.CellDouble)
        {
            if (e.currentState == Cell.State.Clear || e.currentState == Cell.State.Mine)
            {
                PropperCellsArray.Add(e);
            }
            else
            {
                Debug.LogError($"{e.name}  {e.currentState}");
            }
        }

        

        if (_timer >= timerForAnglePositions)
        {
            _timer = 0;
            _angleTimer = false;
        }
        else
        {
            foreach(var e in AngelPositions)
            {
                PropperCellsArray.Remove(e);
            }
        }


        int randomCell = Random.Range(0, PropperCellsArray.Count);

        float x = PropperCellsArray[randomCell].transform.position.x;
        float z = PropperCellsArray[randomCell].transform.position.z;

        Vector3 position = new Vector3(x, 0, z);
        //Debug.LogError($"Метеорит спавнится на {PropperCellsArray[randomCell].name}  {PropperCellsArray[randomCell].currentState}");


        if (!_angleTimer)
        {
            foreach (var e in AngelPositions)
            {
                if (position.x == e.transform.position.x && position.z == e.transform.position.z)
                {
                    _angleTimer = true;
                }
            }
        }





        return position;
    }

    


    private void CreateMeteorite(Meteorite meteorite, Vector3 position)
    {
        float timeToFall = Random.Range(meteorite.BoomTimeMin, meteorite.BoomTimeMax + 1); /// значения в интах, поэтому +1 чтобы в инспекторе проще было
        int addSecondsToBoom = Random.Range(5,11); /// перестраховка от бага, что иногда не определяется положения игрока
        foreach (var e in CellController.CellDouble)
        {
            if (e.currentState == Cell.State.PlayerOcupied)
            {
                Debug.Log(e.name);
                float x = Mathf.Abs(e.transform.position.x - position.x);
                float z = Mathf.Abs(e.transform.position.z - position.z);
                addSecondsToBoom = Mathf.RoundToInt((x + z) * NewPlayerController.TimeToReachNextTile) + 1;
                Debug.Log($"спавним метеорит № {meteoriteCounter + 1}");
            }
            else
            {

            }
        }

        GameObject newMeteorite = GameObject.Instantiate(Resources.Load(MeteoriteTarget.name), position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        newMeteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = timeToFall + addSecondsToBoom;
        meteoriteCounter++;
        State = States.Ready;

        Debug.Log($"addSecondsToBoom = {timeToFall} + {addSecondsToBoom}" );
    }

}
