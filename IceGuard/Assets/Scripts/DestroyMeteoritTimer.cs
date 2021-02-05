﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeteoritTimer : MonoBehaviour
{
    
    public float currentTime;
    /// <summary>
    /// рандомное время падения которое мы получаем при генерации метеоритов
    /// </summary>
    public float timetoFall;
    public TextMesh timer;
    private bool IsTimerActive;

    void Start()
    {
        IsTimerActive = false;
        /// включаем объект текс меша через 1 секунду после создания метеорита
        StartCoroutine(WaitToCreateTimer());
        currentTime = timetoFall + 2;
        /// уничтожаем объект метеорита после истечения timeToFall
        StartCoroutine(LaterDestroy());
        StartCoroutine(ActivateCollider());
        StartCoroutine(CheckMeteoriteKillEarth());
    }

    private void Update()
    {
        if (timer.gameObject.activeSelf && IsTimerActive == false)
        {
            
            StartCoroutine(CerrentTimeMinusOne());
        }
        
    }

    private IEnumerator WaitToCreateTimer()
    {
        yield return new WaitForSeconds(1f);
        timer.gameObject.SetActive(true);
        timer.text = timetoFall.ToString();
    }

    private IEnumerator CerrentTimeMinusOne()
    {
        IsTimerActive = true;
        yield return new WaitForSeconds(1f);
        ShowTimer();
        IsTimerActive = false;
    }
    private void ShowTimer()
    {
        timetoFall -= 1f;  
        if(timetoFall <= 0.5f)
        {
            timer.text = "Boom!";
        }
        else
        {
            timer.text = timetoFall.ToString("#");
        }
    }
    
    
    
    IEnumerator LaterDestroy()
    {
        yield return new WaitForSeconds(currentTime);
        FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MeteoriteCrashEarth);
        Destroy(this.gameObject);
    }

    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(currentTime -1f);
        gameObject.GetComponent<Collider>().enabled = true;
    }
    IEnumerator CheckMeteoriteKillEarth()
    {
        yield return new WaitForSeconds(currentTime - 0.5f);
        if (_isMeteoriteKillEarth)
        {
            PlayerHitPoints.HitPoints = 0;
            Debug.Log("ударилось в Землю");
        }
    }

    private bool _isMeteoriteKillEarth = true;
    private bool _isMeteoriteDestroed = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isMeteoriteKillEarth = false;
            PlayerHitPoints.HitPoints = 0;
            Debug.Log("ударило по плееру " + PlayerHitPoints.HitPoints);
        }
        if (other.CompareTag("ForceField"))
        {
            if (_isMeteoriteDestroed) return;
            _isMeteoriteKillEarth = false;
            _isMeteoriteDestroed = true;
            Vibrator.Vibrate();
            Debug.Log("ударилось в поле");
            
        }
    }
    
 

}
