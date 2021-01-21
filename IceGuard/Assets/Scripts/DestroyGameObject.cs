using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public float seconds = 3;
    void Start()
    {
        StartCoroutine(LaterDestroy());
    }

    IEnumerator LaterDestroy()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
  
}
