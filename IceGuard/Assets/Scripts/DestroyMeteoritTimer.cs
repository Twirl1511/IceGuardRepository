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
    }

    
    IEnumerator LaterDestroy()
    {
        yield return new WaitForSeconds(seconds);
        FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MeteoriteCrashEarth);
        Destroy(this.gameObject);
    }

    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(seconds -1.1f);
        gameObject.GetComponent<Collider>().enabled = true;
    }

    private bool _kostyl = true;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHitPoints.HitPoints = 0;
            Debug.Log("ударило по плееру " + PlayerHitPoints.HitPoints);
        }
        if (other.CompareTag("ForceField"))
        {
            _kostyl = false;
            Debug.Log("ударилось в поле");
        }
        if (_kostyl)
        {
            PlayerHitPoints.HitPoints = 0;
            Debug.Log("ударилось в Землю");
        }
    }

}
