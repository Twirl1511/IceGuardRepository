using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritScriptNEW : MonoBehaviour
{

    public Player player;
    public MeteoritePlace meteoriteOne;
    public MeteoritePlace meteoriteTwo;
    private int rows;
    private int columns;
    [SerializeField] private int minTimeToCrash = 5;
    [SerializeField] private int maxTimeToCrash = 16;
    [SerializeField] private float Delay = 8f;
    [SerializeField] private float DelayBtwNewMeteorites = 15f;
    private bool _areMeteoritesReady = true;

    private void Start()
    {
        player = new Player();
        meteoriteOne = new MeteoritePlace();
        meteoriteTwo = new MeteoritePlace();
        rows = CellController.CellDouble.GetUpperBound(0) + 1;
        columns = CellController.CellDouble.Length / rows;
        TestBUTTON();
    }
    
    public IEnumerator DelayBtwMeteorites()
    {
        _areMeteoritesReady = false;
        yield return new WaitForSeconds(DelayBtwNewMeteorites);
        TestBUTTON();
        _areMeteoritesReady = true;
    }
    private void Update()
    {
        if (_areMeteoritesReady)
        {
            StartCoroutine(DelayBtwMeteorites());
        }
    }
    public void TestBUTTON()
    {
        
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                if(CellController.CellDouble[i,j].currentState == Cell.State.PlayerOcupied)
                {
                    player.rowPosition = i;
                    player.columnPosition = j;

                }
            }
        }
        RandomMeteoritePosition();
    }


    public void CreateRandomMeteoriteTimers()
    {
        meteoriteOne.timer = Random.Range(minTimeToCrash, maxTimeToCrash);
        meteoriteTwo.timer = Random.Range(minTimeToCrash, maxTimeToCrash);
    }

    public void CreateFirstMeteorite()
    {
        meteoriteOne.rowPosition = Random.Range(0, 6);
        meteoriteOne.columnPosition = Random.Range(0, 6);
    }
    public void CreateSecondMeteorite()
    {
        meteoriteTwo.rowPosition = Random.Range(0, 6);
        meteoriteTwo.columnPosition = Random.Range(0, 6);
    }

    public bool AreMeteoritesOnSmaePostion()
    {
        if(meteoriteOne.rowPosition == meteoriteTwo.rowPosition && meteoriteOne.columnPosition == meteoriteTwo.columnPosition)
        {
            return true;
        }
        return false;
    }

    public bool IsDistanceCorrect(Player player, MeteoritePlace meteorite, out float seconds)
    {
        seconds = 0f;
        int rowABS = Mathf.Abs(player.rowPosition - meteorite.rowPosition);
        int columnABS = Mathf.Abs(player.columnPosition - meteorite.columnPosition);
        if(((float)(rowABS + columnABS)/2) + .5f >= 2)
        {
            seconds = ((float)(rowABS + columnABS) / 2) + .5f;
            return true;
        }
        return false;
    }

    public void RandomMeteoritePosition()
    {
        CreateRandomMeteoriteTimers();


        float Y1 = 0;
        float Y2 = 0;

        do
        {
            CreateFirstMeteorite();
        }
        while (!IsDistanceCorrect(player, meteoriteOne, out Y1));

        do
        {
            CreateSecondMeteorite();
        }
        while (!IsDistanceCorrect(player, meteoriteTwo, out Y2) && AreMeteoritesOnSmaePostion());


        float B1 = meteoriteOne.timer + meteoriteTwo.timer - Y1;
        float B2 = meteoriteOne.timer + meteoriteTwo.timer - Y2;

        float P = DistanceBtwMeteorites(meteoriteOne, meteoriteTwo);

        if(B1 - P >= Delay && B2 - P >= Delay)
        {
            CreateRealMeteorites();
        }
        else
        {
            /// по новой запускаем этот же метод
            RandomMeteoritePosition();
        }




    }

    public float DistanceBtwMeteorites(MeteoritePlace meteoriteOne, MeteoritePlace meteoriteTwo)
    {
        float P = Mathf.Abs(meteoriteOne.rowPosition - meteoriteTwo.rowPosition) + Mathf.Abs(meteoriteOne.columnPosition - meteoriteTwo.columnPosition);

        return P;
    }
    public void CreateRealMeteorites()
    {
        float x = CellController.CellDouble[meteoriteOne.rowPosition, meteoriteOne.columnPosition].transform.position.x;
        float z = CellController.CellDouble[meteoriteOne.rowPosition, meteoriteOne.columnPosition].transform.position.z;
        Vector3 position = new Vector3(x, 0.08f, z);
        GameObject meteorite = GameObject.Instantiate(Resources.Load("TargetTest"), position, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
        meteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = meteoriteOne.timer;

        
        x = CellController.CellDouble[meteoriteTwo.rowPosition, meteoriteTwo.columnPosition].transform.position.x;
        z = CellController.CellDouble[meteoriteTwo.rowPosition, meteoriteTwo.columnPosition].transform.position.z;
        position = new Vector3(x, 0.08f, z);
        meteorite = GameObject.Instantiate(Resources.Load("TargetTest"), position, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
        meteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = meteoriteTwo.timer;
    }
    
        
    
}
