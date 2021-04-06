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
[System.Serializable]
public class AdditionalMeteorite
{
    [Range(1, 50)]
    public int BoomTimeMin;
    [Range(1, 50)]
    public int BoomTimeMax;

    [Range(1, 200)]
    public int FirstAppearTime;
    [Range(1, 100)]
    public int NotAppearTImeZone;
    [Range(1, 50)]
    public int AppearTimeMinus;
    [Range(1, 100)]
    public int AppearTimeMinimum;
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

    [HideInInspector] public Vector3[] MeteoriteOcupiedPositions = new Vector3[8];
    [HideInInspector] public int MeteoriteCounter = 0;
    public enum States
    {
        Ready,
        Stop
    }
    public static States State;
    [SerializeField] private AdditionalMeteorite additionalMeteorite;
    private float additionalMeteoriteTimer = 0;
    private bool addMeteorReady = true;
    

    void Start()
    {
        State = States.Ready;
        meteoriteCounter = -1;
    }


    void Update()
    {
        additionalMeteoriteTimer += Time.deltaTime;


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

        AdditionalMeteoriteCheck();
    }


    private void AdditionalMeteoriteCheck()
    {
        if(additionalMeteoriteTimer >= additionalMeteorite.FirstAppearTime)
        {
            addMeteorReady = true;
            additionalMeteoriteTimer = 0;
            additionalMeteorite.FirstAppearTime -= additionalMeteorite.AppearTimeMinus;
            if(additionalMeteorite.FirstAppearTime < additionalMeteorite.AppearTimeMinimum)
            {
                additionalMeteorite.FirstAppearTime = additionalMeteorite.AppearTimeMinimum;
            }
        }

        if(additionalMeteoriteTimer >= additionalMeteorite.NotAppearTImeZone && addMeteorReady)
        {
            addMeteorReady = false;
            int randomTime = Random.Range(0, additionalMeteorite.FirstAppearTime - additionalMeteorite.NotAppearTImeZone - 1);
            StartCoroutine(LaterStartAddMeteorite(randomTime));
            Debug.LogError("Дали разрешение на запуск доп метеорита через: " + randomTime);
        }

    }
    IEnumerator LaterStartAddMeteorite(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CreateAdditionalMeteorite(additionalMeteorite, RandomPosition());
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
    }

    private Vector3 RandomPosition()
    {

        List<Vector3> PropperCellsArray = new List<Vector3>();

        foreach (var e in CellController.CellDouble)
        {

            PropperCellsArray.Add(e.transform.position);
            

        }
        print("Before = " + PropperCellsArray.Count);

        for (int i = 0; i< MeteoriteOcupiedPositions.Length; i++)
        {
            
            if(MeteoriteOcupiedPositions[i] != null)
            {

                Vector3 positionToRemove = new Vector3(MeteoriteOcupiedPositions[i].x, PropperCellsArray[0].y, MeteoriteOcupiedPositions[i].z);
                PropperCellsArray.Remove(positionToRemove);
                //PropperCellsArray.Remove(PropperCellsArray[1]);
            }
        }

        print("After = " + PropperCellsArray.Count);






        //    List<Cell> PropperCellsArray = new List<Cell>();
        ///*Debug.LogError($"Клетки которые исключаем");*/ /// после else
        //foreach (var e in CellController.CellDouble)
        //{
        //    if (e.currentState == Cell.State.Clear || e.currentState == Cell.State.Mine)
        //    {
        //        PropperCellsArray.Add(e);
        //    }
        //    else
        //    {
        //        //Debug.LogError($"Клетки которые исключаем");
        //        //Debug.LogError($"{e.name}  {e.currentState}");
        //    }
        //}



        //if (_timer >= timerForAnglePositions)
        //{
        //    _timer = 0;
        //    _angleTimer = false;
        //}
        //else
        //{
        //    foreach(var e in AngelPositions)
        //    {
        //        PropperCellsArray.Remove(e);
        //    }
        //}


        int randomCell = Random.Range(0, PropperCellsArray.Count);

        float x = PropperCellsArray[randomCell].x;
        float z = PropperCellsArray[randomCell].z;

        Vector3 position = new Vector3(x, 0, z);
        //Debug.LogError($"Метеорит спавнится на {PropperCellsArray[randomCell].name}  {PropperCellsArray[randomCell].currentState}");


        //if (!_angleTimer)
        //{
        //    foreach (var e in AngelPositions)
        //    {
        //        if (position.x == e.transform.position.x && position.z == e.transform.position.z)
        //        {
        //            _angleTimer = true;
        //        }
        //    }
        //}

        MeteoriteOcupiedPositions[MeteoriteCounter] = position;
        MeteoriteCounter++;
        if (MeteoriteCounter > MeteoriteOcupiedPositions.Length - 1)
            MeteoriteCounter = 0;



        return position;
    }

    


    private void CreateMeteorite(Meteorite meteorite, Vector3 position)
    {
        float timeToFall = Random.Range(meteorite.BoomTimeMin, meteorite.BoomTimeMax + 1); /// значения в интах, поэтому +1 чтобы в инспекторе проще было
        int addSecondsToBoom = 6; /// перестраховка от бага, что иногда не определяется положения игрока
        foreach (var e in CellController.CellDouble)
        {
            if (e.currentState == Cell.State.PlayerOcupied)
            {
                //Debug.Log(e.name);
                float x = Mathf.Abs(e.transform.position.x - position.x);
                float z = Mathf.Abs(e.transform.position.z - position.z);
                addSecondsToBoom = Mathf.RoundToInt((x + z) * NewPlayerController.TimeToReachNextTile) + 1;
                break;
                //Debug.LogError($"спавним метеорит № {meteoriteCounter + 1}");
            }
        }

        GameObject newMeteorite = GameObject.Instantiate(Resources.Load(MeteoriteTarget.name), position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

        //_meteoriteOcupiedPositions[_meteoriteCounter] = position;
        //_meteoriteCounter++;
        //if (_meteoriteCounter > _meteoriteOcupiedPositions.Length - 1)
        //    _meteoriteCounter = 0;

        newMeteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = timeToFall + addSecondsToBoom;
        meteoriteCounter++;
        State = States.Ready;

        //Debug.Log($"addSecondsToBoom = {timeToFall} + {addSecondsToBoom}" );
    }


    private void CreateAdditionalMeteorite(AdditionalMeteorite addMeteorite, Vector3 position)
    {
        float timeToFall = Random.Range(addMeteorite.BoomTimeMin, addMeteorite.BoomTimeMax + 1); /// значения в интах, поэтому +1 чтобы в инспекторе проще было

        //Debug.LogError($"спавним ДOOOOOOOOOOOOOП метеорит № {meteoriteCounter + 1}");
        GameObject newMeteorite = GameObject.Instantiate(Resources.Load(MeteoriteTarget.name), position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        newMeteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = timeToFall;

        

        /// костыль, дабы общее количество метеоритов не менялось после смерти дополнительного
        StartCoroutine(AdjactMeteoritesNumber(timeToFall + 2));
    }

    /// <summary>
    /// костыль, дабы общее количество метеоритов не менялось после смерти дополнительного
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator AdjactMeteoritesNumber(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        meteoriteCounter++;
    }

}
