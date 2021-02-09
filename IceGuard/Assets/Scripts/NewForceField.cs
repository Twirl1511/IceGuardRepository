using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewForceField : MonoBehaviour
{
    public float seconds = 30;
    void Start()
    {
        StartCoroutine(LaterDestroy());
    }

    IEnumerator LaterDestroy()
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
            Destroy(this.gameObject);
        }
    }
   
}
