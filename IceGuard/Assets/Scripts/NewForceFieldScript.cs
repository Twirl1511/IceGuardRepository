using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewForceFieldScript : MonoBehaviour
{
    public static List<GameObject> allForceFields = new List<GameObject>(36);
    public static float lifeTime = 60f;
    public static float TimeMultiplier;
    public float SecondsToDestroy;
    [SerializeField] private Collider Collider;
    [SerializeField] private Image Health;

    void Start()
    {
        TimeMultiplier = 1f;
        StartCoroutine(LaterColliderActivate());
        SecondsToDestroy = 0.00001f;
    }
    private void Update()
    {
        AdjustedLifeTimerNew();
        LifeTimer();
    }

    public void LifeTimer()
    {
        Health.fillAmount = 1 - (SecondsToDestroy / lifeTime);
        if (SecondsToDestroy >= lifeTime)
        {
            Debug.Log(allForceFields.Count);
            Destroy(this.gameObject);
        }
    }


    public void AdjustedLifeTimerNew()
    {
        if (allForceFields.Count == 1
            || allForceFields.Count == 0)
        {
            SecondsToDestroy += Time.deltaTime;
        }
        else
        {
            TimeMultiplier = Mathf.Pow(1.1f, allForceFields.Count - 1);
            SecondsToDestroy += Time.deltaTime * TimeMultiplier;
        }
    }

    IEnumerator LaterDestroy(float seconds = 40)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log(allForceFields.Count);
        Destroy(this.gameObject);
    }

    IEnumerator LaterColliderActivate()
    {
        yield return new WaitForSeconds(NewPlayerController.TimeToReachNextTile);
        Collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MinusHP);       /// ЗАМЕНИТЬ

            PlayerHitPoints.MinusHP();
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Meteorite"))
        {
            StartCoroutine(LaterDestroy(0));
        }
    }

    private void OnDestroy()
    {
        allForceFields.Remove(gameObject);
    }

    

}
