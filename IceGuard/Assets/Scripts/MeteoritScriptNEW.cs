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


    private void Start()
    {
        player = new Player();
        meteoriteOne = new MeteoritePlace();
        meteoriteTwo = new MeteoritePlace();
        rows = CellController.CellDouble.GetUpperBound(0) + 1;
        columns = CellController.CellDouble.Length / rows;
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


    public void CreateRandomMeteoriteTimers(int maxSeconds = 21)
    {
        meteoriteOne.timer = Random.Range(5, maxSeconds-5);
        meteoriteTwo.timer = 20 - meteoriteOne.timer;
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
            Debug.Log("перебираем первый");
        }
        while (!IsDistanceCorrect(player, meteoriteOne, out Y1));

        do
        {
            CreateSecondMeteorite();
            Debug.Log("перебираем второй");
        }
        while (!IsDistanceCorrect(player, meteoriteTwo, out Y2));


        float B1 = meteoriteOne.timer + meteoriteTwo.timer - Y1;
        float B2 = meteoriteOne.timer + meteoriteTwo.timer - Y2;

        float P = DistanceBtwMeteorites(meteoriteOne, meteoriteTwo);

        if(B1 - P >= 8f && B2 - P >= 8f)
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
        meteorite.GetComponent<DestroyMeteoritTimer>().currentSeconds = meteoriteOne.timer;

        StartCoroutine(NextMeteoriteAfter(Random.Range(0.5f, 3f)));
    }
    private IEnumerator NextMeteoriteAfter(float sec)
    {
        yield return new WaitForSeconds(sec);
        float x = CellController.CellDouble[meteoriteTwo.rowPosition, meteoriteTwo.columnPosition].transform.position.x;
        float z = CellController.CellDouble[meteoriteTwo.rowPosition, meteoriteTwo.columnPosition].transform.position.z;
        Vector3 position = new Vector3(x, 0.08f, z);
        GameObject meteorite = GameObject.Instantiate(Resources.Load("TargetTest"), position, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
        meteorite.GetComponent<DestroyMeteoritTimer>().currentSeconds = meteoriteTwo.timer;
    }
}
