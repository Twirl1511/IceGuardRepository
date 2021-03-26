using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meteorite"))
        {
            if (PlayerHitPoints.HitPoints != 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
