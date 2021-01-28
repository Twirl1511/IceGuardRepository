using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeteoritTimer : MonoBehaviour
{
    public float seconds = 13;
    void Start()
    {
        StartCoroutine(LaterDestroy());
        StartCoroutine(ActivateCollider());
        StartCoroutine(CheckMeteoriteKillEarth());
    }

    
    IEnumerator LaterDestroy()
    {
        yield return new WaitForSeconds(seconds);
        FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MeteoriteCrashEarth);
        Destroy(this.gameObject);
    }

    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(seconds -2f);
        gameObject.GetComponent<Collider>().enabled = true;
    }
    IEnumerator CheckMeteoriteKillEarth()
    {
        yield return new WaitForSeconds(seconds - 1f);
        if (_isMeteoriteKillEarth)
        {
            PlayerHitPoints.HitPoints = 0;
            Debug.Log("ударилось в Землю");
        }
    }

    private bool _isMeteoriteKillEarth = true;
    private bool _isMeteoriteDestroed = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isMeteoriteKillEarth = false;
            PlayerHitPoints.HitPoints = 0;
            Debug.Log("ударило по плееру " + PlayerHitPoints.HitPoints);
        }
        if (other.CompareTag("ForceField"))
        {
            if (_isMeteoriteDestroed) return;
            _isMeteoriteKillEarth = false;
            _isMeteoriteDestroed = true;
            Vibrator.Vibrate();
            Debug.Log("ударилось в поле");
            
        }
    }
    
 

}
