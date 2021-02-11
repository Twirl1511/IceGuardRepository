using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewForceField : MonoBehaviour
{
    public static float seconds = 40;
    void Start()
    {
        StartCoroutine(LaterDestroy(seconds));
    }

    IEnumerator LaterDestroy(float seconds = 30)
    {
        
        yield return new WaitForSeconds(seconds);
        
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHitPoints.HitPoints--;
            
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Meteorite"))
        {
            StartCoroutine(LaterDestroy(1));
            
        }
    }
}
