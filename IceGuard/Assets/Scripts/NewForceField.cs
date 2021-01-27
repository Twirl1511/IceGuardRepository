using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewForceField : MonoBehaviour
{
    public float seconds = 20;
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
