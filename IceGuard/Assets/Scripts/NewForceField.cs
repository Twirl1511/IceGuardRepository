using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewForceField : MonoBehaviour
{
    public static float lifeTime = 60f;
    public static float NewLifeTime;
    public float SecondsToDestroy;
    
    void Start()
    {
        NewLifeTime = lifeTime;
        SecondsToDestroy = 0;
        //StartCoroutine(LaterDestroy(SecondsToDestroy));
    }
    private void Update()
    {
        AdjustedLifeTimer();
        LifeTimer();
    }


    public void LifeTimer()
    {
        SecondsToDestroy += Time.deltaTime;
        if (SecondsToDestroy >= NewLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void AdjustedLifeTimer()
    {
        
        if (PlayerControllerDrawPath.allForceFields.Count == 1 
            || PlayerControllerDrawPath.allForceFields.Count == 0)
        {
            NewLifeTime = lifeTime;
        }else
        {
            NewLifeTime = Mathf.Pow(0.9f, PlayerControllerDrawPath.allForceFields.Count - 1) * lifeTime;
        } 
    }

    IEnumerator LaterDestroy(float seconds = 40)
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
