using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class TutorialScript : MonoBehaviour
{
    public static bool isTutorialFinished;
    [SerializeField] private GameObject Player; 

    void Start()
    {
        isTutorialFinished = false;
        StreamWriter writer = new StreamWriter(Application.dataPath + "/saveFile.json");
        writer.WriteLine(isTutorialFinished);
        writer.Close();


        StreamReader reader = new StreamReader(Application.dataPath + "/saveFile.json");
        string s = reader.ReadToEnd();
        isTutorialFinished = bool.TryParse(s, out isTutorialFinished);
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
        Player.transform.position = new Vector3(0, 0, -2);
    }




}
