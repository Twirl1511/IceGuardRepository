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
    [HideInInspector] public float TutorialTime;
    [SerializeField] private GameObject _dotOnEarth;

    void Start()
    {
        timeToLive = Random.Range(timeToLiveMin, timeToLiveMax+1);
        timeToLive += 1;
        TutorialTime = timeToLive;
        timerText.text = timeToLive.ToString();
        StartCoroutine(LaterDestroy());
        StartCoroutine(ActivateCollider());
    }

    private void Update()
    {
        if (UIManager.GameState == UIManager.GameStates.Play)
        {
            if (!IsTimerActive)
            {
                StartCoroutine(CurrentTimeMinusOne());
            }
        }
    }



    private void SpawnDotOnEarth()
    {
        Vector3 position = new Vector3(0,0,0);
        Instantiate(_dotOnEarth, position, Quaternion.identity);
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

    private void OnTriggerStay(Collider other)
    {
        if (readyToRepair)
        {
            //Debug.Log("readyToRepair = true");
            if (other.CompareTag("Player"))
            {
                //Debug.Log(" коснулось плеера, чиним");
                PlayerHitPoints.RestartHP();
            }
        }
    }
}
