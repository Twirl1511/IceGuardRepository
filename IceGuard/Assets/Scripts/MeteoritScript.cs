using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritScript : MonoBehaviour
{

    [SerializeField] private float TimeToSpawnNeMeteorite = 10;
    private float _time;
    public GameObject[] AllCells = new GameObject[36];

    private void Start()
    {
        _time = 9;
    }
    private void FixedUpdate()
    {
        _time += Time.deltaTime;
    }
    void Update()
    {
        if (_time >= TimeToSpawnNeMeteorite)
        {
            _time = 0;
            FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MeteoriteAlert);
            int random = Random.Range(0, 36);
            Vector3 position = new Vector3(AllCells[random].transform.position.x, 0.08f, AllCells[random].transform.position.y);
            GameObject.Instantiate(Resources.Load("MeteoritTimer"), position, Quaternion.Euler(-90, 0, 45));
        }
    }


    
}
