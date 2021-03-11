using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewForceFieldScript : MonoBehaviour
{
    public static List<GameObject> allForceFields = new List<GameObject>(36);
    public static float lifeTime = 60f;
    public static float NewLifeTime;
    public float SecondsToDestroy;
    [SerializeField] private Collider Collider;
    [SerializeField] private Image Health;

    void Start()
    {
        StartCoroutine(LaterColliderActivate());
        NewLifeTime = lifeTime;
        SecondsToDestroy = 0.0001f;
    }
    private void Update()
    {
        AdjustedLifeTimer();
        LifeTimer();
    }

    public void LifeTimer()
    {
        SecondsToDestroy += Time.deltaTime;
        Health.fillAmount = 1 - (SecondsToDestroy / NewLifeTime);
        if (SecondsToDestroy >= NewLifeTime)
        {
            Debug.Log(allForceFields.Count);
            Destroy(this.gameObject);
        }
    }

    public void AdjustedLifeTimer()
    {
        if (allForceFields.Count == 1
            || allForceFields.Count == 0)
        {
            NewLifeTime = lifeTime;
        }
        else
        {
            NewLifeTime = Mathf.Pow(0.9f, allForceFields.Count - 1) * lifeTime;
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
        yield return new WaitForSeconds(NewPlayerController.TimeToReachNextTile + 0.5f);
        Collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MinusHP);       /// ЗАМЕНИТЬ
            PlayerHitPoints.HitPoints--;
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Meteorite"))
        {
            StartCoroutine(LaterDestroy(1));
        }
    }

    private void OnDestroy()
    {
        allForceFields.Remove(gameObject);
    }

    

}
