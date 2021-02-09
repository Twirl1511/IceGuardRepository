using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewForceField : MonoBehaviour
{
    public static float seconds = 30;
    void Start()
    {
        StartCoroutine(LaterDestroy(seconds));
    }

    IEnumerator LaterDestroy(float seconds = 30)
    {
        PlayerControllerDrawPath.RemoveNullForceFields();
        yield return new WaitForSeconds(seconds);
        PlayerControllerDrawPath.RemoveNullForceFields();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHitPoints.HitPoints--;
            PlayerControllerDrawPath.RemoveNullForceFields();
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Meteorite"))
        {
            StartCoroutine(LaterDestroy(1));
            
        }
    }
}
