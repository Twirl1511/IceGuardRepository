﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MeteoritScriptNEW : MonoBehaviour
//{
//    public bool RUNmeteorites;
//    public Player player;
//    public MeteoritePlace meteoriteOne;
//    public MeteoritePlace meteoriteTwo;
//    private int rows;
//    private int columns;
//    [SerializeField] private int minTimeToCrash = 8;
//    [SerializeField] private int maxTimeToCrash = 16;
//    [SerializeField] private float Delay = 8f;
//    [SerializeField] private float DelayRangeMin = 8f;
//    [SerializeField] private float DelayRangeMax = 14f;
//    private bool _areMeteoritesReady = true;
//    [SerializeField] private GameObject MeteoriteTarget;

//    private void Start()
//    {
//        player = new Player();
//        meteoriteOne = new MeteoritePlace();
//        meteoriteTwo = new MeteoritePlace();
//        rows = CellController.CellDouble.GetUpperBound(0) + 1;
//        columns = CellController.CellDouble.Length / rows;
//        CreateMeteorites();
//    }
    
//    public IEnumerator DelayBtwMeteorites()
//    {
//        _areMeteoritesReady = false;
//        yield return new WaitForSeconds(Random.Range(DelayRangeMin, DelayRangeMax));
//        CreateMeteorites();
//        _areMeteoritesReady = true;
//    }
//    private void Update()
//    {
//        if (_areMeteoritesReady)
//        {
//            StartCoroutine(DelayBtwMeteorites());
//        }
//    }
//    public void CreateMeteorites()
//    {
        
//        for (int i = 0; i < rows; i++)
//        {
//            for(int j = 0; j < columns; j++)
//            {
//                if(CellController.CellDouble[i,j].currentState == Cell.State.PlayerOcupied)
//                {
//                    player.rowPosition = i;
//                    player.columnPosition = j;
//                }
//            }
//        }
//        RandomMeteoritePosition();
//    }


//    public void CreateRandomMeteoriteTimers()
//    {
//        meteoriteOne.timer = Random.Range(minTimeToCrash, maxTimeToCrash);
//        meteoriteTwo.timer = Random.Range(minTimeToCrash, maxTimeToCrash);
//    }

//    public void CreateFirstMeteorite()
//    {
//        meteoriteOne.rowPosition = Random.Range(0, 6);
//        meteoriteOne.columnPosition = Random.Range(0, 6);
//    }
//    public void CreateSecondMeteorite()
//    {
//        meteoriteTwo.rowPosition = Random.Range(0, 6);
//        meteoriteTwo.columnPosition = Random.Range(0, 6);
//        if (AreMeteoritesOnSmaePostion())
//        {
//            CreateSecondMeteorite();
//        }
//    }

//    public bool AreMeteoritesOnSmaePostion()
//    {
//        if(meteoriteOne.rowPosition == meteoriteTwo.rowPosition && meteoriteOne.columnPosition == meteoriteTwo.columnPosition)
//        {
//            return true;
//        }
//        return false;
//    }

//    public bool IsDistanceCorrect(Player player, MeteoritePlace meteorite, out float seconds)
//    {
//        seconds = 0f;
//        int rowABS = Mathf.Abs(player.rowPosition - meteorite.rowPosition);
//        int columnABS = Mathf.Abs(player.columnPosition - meteorite.columnPosition);
//        if(((float)(rowABS + columnABS)/2) + .5f >= 2)
//        {
//            seconds = ((float)(rowABS + columnABS) / 2) + .5f;
//            return true;
//        }
//        return false;
//    }

//    public void RandomMeteoritePosition()
//    {
//        CreateRandomMeteoriteTimers();


//        float Y1 = 0;
//        float Y2 = 0;

//        do
//        {
//            CreateFirstMeteorite();
//        }
//        while (!IsDistanceCorrect(player, meteoriteOne, out Y1) );

//        do
//        {
//            CreateSecondMeteorite();
//        }
//        while (!IsDistanceCorrect(player, meteoriteTwo, out Y2) );


//        float B1 = meteoriteOne.timer + meteoriteTwo.timer - Y1;
//        float B2 = meteoriteOne.timer + meteoriteTwo.timer - Y2;

//        float P = DistanceBtwMeteorites(meteoriteOne, meteoriteTwo);

//        if(B1 - P >= Delay && B2 - P >= Delay)
//        {
//            if (RUNmeteorites)
//            {
//                CreateRealMeteorites();
//            }
            
//            SetDefaultMeteoritePositions();
//        }
//        else
//        {
//            /// по новой запускаем этот же метод
//            RandomMeteoritePosition();
//        }
//    }

//    public void SetDefaultMeteoritePositions()
//    {
//        meteoriteOne.rowPosition = 0;
//        meteoriteTwo.rowPosition = 0;
//        meteoriteOne.columnPosition = 0;
//        meteoriteTwo.columnPosition = 0;
//    }

//    public float DistanceBtwMeteorites(MeteoritePlace meteoriteOne, MeteoritePlace meteoriteTwo)
//    {
//        float P = Mathf.Abs(meteoriteOne.rowPosition - meteoriteTwo.rowPosition) + Mathf.Abs(meteoriteOne.columnPosition - meteoriteTwo.columnPosition);

//        return P;
//    }
//    public void CreateRealMeteorites()
//    {
//        float x = CellController.CellDouble[meteoriteOne.rowPosition, meteoriteOne.columnPosition].transform.position.x;
//        float z = CellController.CellDouble[meteoriteOne.rowPosition, meteoriteOne.columnPosition].transform.position.z;
//        Vector3 position = new Vector3(x, 0, z);
//        GameObject meteorite = GameObject.Instantiate(Resources.Load(MeteoriteTarget.name), position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
//        meteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = meteoriteOne.timer;

        
//        x = CellController.CellDouble[meteoriteTwo.rowPosition, meteoriteTwo.columnPosition].transform.position.x;
//        z = CellController.CellDouble[meteoriteTwo.rowPosition, meteoriteTwo.columnPosition].transform.position.z;
//        position = new Vector3(x, 0, z);
//        meteorite = GameObject.Instantiate(Resources.Load(MeteoriteTarget.name), position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
//        meteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = meteoriteTwo.timer;
//    }
    
        
    
//}
