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
    GameObject meteorite;

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
        
        Vector3 position = new Vector3(0, 0, -2);
        player = Instantiate(Resources.Load(Player.name), position, Quaternion.identity) as GameObject;
        Instantiate(Resources.Load(PlayerGhost.name), position, Quaternion.identity);
        position = new Vector3(0, 0, 1);
        meteorite = Instantiate(Resources.Load(MeteoriteTarget.name), position, Quaternion.identity) as GameObject;
        meteorite.GetComponent<DestroyMeteoritTimer>().timetoFall = FirstMeteoriteTimer;
    }

    public string FirstPositionFaile()
    {
        string s = TutorTipsArray[0];
        if (player.transform.position == new Vector3(0, 0, -2))
        {
            return TutorTipsArray[0];
        }
        if (player.transform.position == new Vector3(0, 0, 1))
        {
            return TutorTipsArray[1];
        }
        if (player.transform.position != new Vector3(0, 0, -2) && PlayerHitPoints.HitPoints == 0)
        {
            return TutorTipsArray[2];
        }
        return s;
    }




}
