using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeteoritTimer : MonoBehaviour
{
    public float seconds = 13;
    void Start()
    {
        StartCoroutine(LaterDestroy());
    }

    IEnumerator LaterDestroy()
    {
        yield return new WaitForSeconds(seconds);
        FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MeteoriteCrashEarth);
        Destroy(this.gameObject);
    }
  
}
