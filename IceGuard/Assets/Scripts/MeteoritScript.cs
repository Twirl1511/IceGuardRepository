using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritScript : MonoBehaviour
{
    public static int MAX_NUMBER_OF_METEORITS = 1;
    public static int CURRENT_NUMBER_OF_METEORITS = 0;
    public GameObject[] AllCells = new GameObject[36];


    void Update()
    {
        if (CURRENT_NUMBER_OF_METEORITS < MAX_NUMBER_OF_METEORITS)
        {
            FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MeteoriteAlert);
            int random = Random.Range(0, 36);
            Vector3 position = new Vector3(AllCells[random].transform.position.x, 0.08f, AllCells[random].transform.position.y);
            GameObject.Instantiate(Resources.Load("MeteoritTimer"), position, Quaternion.Euler(-90, 0, 45));
        }
    }
}
