using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairTargetScript : MonoBehaviour
{
    [SerializeField] private int timeToLiveMin;
    [SerializeField] private int timeToLiveMax;
    private float timeToLive;
    private bool IsTimerActive = false;
    [SerializeField] TextMesh timerText;
    [SerializeField] private Collider collider;
    private bool readyToRepair = false;

    void Start()
    {
        timeToLive = Random.Range(timeToLiveMin, timeToLiveMax+1);
        timeToLive += 1;
        timerText.text = timeToLive.ToString();
        StartCoroutine(LaterDestroy());
        StartCoroutine(ActivateCollider());
    }

    private void Update()
    {
        if (!IsTimerActive)
        {
            StartCoroutine(CurrentTimeMinusOne());
        }
    }

    IEnumerator LaterDestroy()
    {
        yield return new WaitForSeconds(timeToLive + 1);
        GameObject.Destroy(gameObject);
    }

    private IEnumerator CurrentTimeMinusOne()
    {
        IsTimerActive = true;
        yield return new WaitForSeconds(1f);
        timeToLive -= 1;
        timerText.text = timeToLive.ToString();
        IsTimerActive = false;
    }

    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(timeToLive);
        readyToRepair = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (readyToRepair)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHitPoints.RestartHP();
            }
        }
    }
}
