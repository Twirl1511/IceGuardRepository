using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class TutorialScript : MonoBehaviour
{
    public static bool isTutorialFinished;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject PlayerGhost;
    [SerializeField] private GameObject MeteoriteTarget;
    [SerializeField] private float FirstMeteoriteTimer;
    public string[] TutorTipsArray;
    GameObject player;
    GameObject playerGhost;
    GameObject meteorite;
    public Cell FirstMeteoriteCell;
    [SerializeField] private GameObject GameOverPanel;

    //public enum TutorLevels
    //{
    //    First,
    //    Second,
    //    Third,
    //    Finished
    //}

    //public TutorLevels Level;

    void Start()
    {
        isTutorialFinished = false;
        StreamWriter writer = new StreamWriter(Application.dataPath + "/saveFile.json");
        writer.WriteLine(isTutorialFinished);
        writer.Close();


        StreamReader reader = new StreamReader(Application.dataPath + "/saveFile.json");
        isTutorialFinished = System.Convert.ToBoolean(reader.ReadToEnd());
        Debug.Log(isTutorialFinished);
        reader.Close();


        if (isTutorialFinished)
        {
            Debug.Log("включаем основную игру");
        }
        else
        {
            /// запускаем туторку
            
        }
    }

    void Update()
    {

        
        
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
    }

    public string FirstPositionFaile()
    {
        string s = TutorTipsArray[0];
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
        string s = TutorTipsArray[0];
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
        string s = TutorTipsArray[0];
        if (player.transform.position == new Vector3(0, 0, -2))
        {
            return TutorTipsArray[3];
        }
        if (player.transform.position == new Vector3(0, 0, 1))
        {
            return TutorTipsArray[4];
        }
        if (player.transform.position != new Vector3(0, 0, -2) && PlayerHitPoints.HitPoints == 0)
        {
            return TutorTipsArray[5];
        }
        return s;
    }




}
