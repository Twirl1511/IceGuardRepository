using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeteoritTimer : MonoBehaviour
{
    
    private float currentTime;
    /// <summary>
    /// рандомное время падения которое мы получаем при генерации метеоритов
    /// </summary>
    public float timetoFall;
    public TextMesh timer;
    private bool IsTimerActive;
    public Collider Collider;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private GameObject Mine;

    private bool readyToStrike = false;

    void Start()
    {
        Explosion.SetActive(false);
        IsTimerActive = false;
        /// включаем объект текс меша через 1 секунду после создания метеорита
        StartCoroutine(WaitToCreateTimer());
        currentTime = timetoFall + 2;
        /// уничтожаем объект метеорита после истечения timeToFall
        StartCoroutine(LaterDestroy());
        StartCoroutine(ActivateCollider());
        StartCoroutine(CheckMeteoriteKillEarth());
    }

    private void FixedUpdate()
    {
        if (timer.gameObject.activeSelf && IsTimerActive == false)
        {
            StartCoroutine(CurrentTimeMinusOne());
        }

    }

    private IEnumerator WaitToCreateTimer()
    {
        yield return new WaitForSeconds(1f);
        timer.gameObject.SetActive(true);
        timer.text = timetoFall.ToString();
    }

    private IEnumerator CurrentTimeMinusOne()
    {
        IsTimerActive = true;
        yield return new WaitForSeconds(1f);
        ShowTimer();
        IsTimerActive = false;
    }
    private void ShowTimer()
    {
        timetoFall -= 1f;  
        if(timetoFall <= 0f)
        {
            timer.text = " ";
            
        }
        else
        {
            timer.text = timetoFall.ToString("#");
        }
    }

    private void OnDestroy()
    {
        MeteoriteController.meteoriteCounter--;
        //MeteoriteController.State = MeteoriteController.States.Ready;
        //Debug.Log($"метеорит уничтожен, осталось {MeteoriteController.meteoriteCounter}");
    }

    /// <summary>
    /// если метеорит врежется в Землю
    /// </summary>
    /// <returns></returns>
    IEnumerator LaterDestroy()
    {
        yield return new WaitForSeconds(currentTime);
        
        FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MeteoriteCrashEarth);
        Destroy(this.gameObject);
    }
    /// <summary>
    /// включаем коллайдер
    /// </summary>
    /// <returns></returns>
    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(currentTime - 0.5f);
        readyToStrike = true;
    }
    IEnumerator CheckMeteoriteKillEarth()
    {
        yield return new WaitForSeconds(currentTime);
        if (_isMeteoriteKillEarth)
        {
            PlayerHitPoints.MinusAllHP();
            Debug.Log("ударилось в Землю");
        }
    }

    private bool _isMeteoriteKillEarth = true;
    private bool _isMeteoriteDestroyed = false;
    private void OnTriggerStay(Collider other)
    {
        if (readyToStrike)
        {
            if (other.CompareTag("Player"))
            {
                Explosion.SetActive(true);
                _isMeteoriteKillEarth = false;
                PlayerHitPoints.MinusAllHP();
            }

            if (other.CompareTag(Mine.tag))
            {
                if (_isMeteoriteDestroyed)
                {
                    return;
                }
                Explosion.SetActive(true);
                _isMeteoriteKillEarth = false;
                _isMeteoriteDestroyed = true;
                Destroy(other.gameObject);
                /// ударилось в поле
            }

        }

        
        
    }
    
 

}
