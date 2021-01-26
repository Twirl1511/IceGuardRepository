using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeteoritTimer : MonoBehaviour
{
    public float seconds = 13;
    void Start()
    {
        MeteoritScript.CURRENT_NUMBER_OF_METEORITS = 1;
        StartCoroutine(LaterDestroy());
    }

    IEnumerator LaterDestroy()
    {
        yield return new WaitForSeconds(seconds);
        MeteoritScript.CURRENT_NUMBER_OF_METEORITS = 0;
        FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MeteoriteCrashEarth);
        Destroy(this.gameObject);
    }
  
}
