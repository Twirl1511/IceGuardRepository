using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPathPoint : MonoBehaviour
{
    public float seconds = 0.5f;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LaterDestroy());
        }
    }
    IEnumerator LaterDestroy()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
  
}
