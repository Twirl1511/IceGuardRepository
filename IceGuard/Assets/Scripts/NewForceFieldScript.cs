﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewForceFieldScript : MonoBehaviour
{
    public static List<GameObject> allForceFields = new List<GameObject>(36);
    public static float lifeTime = 60f;
    public static float TimeMultiplier;
    public float SecondsToDestroy;
    [SerializeField] private Collider Collider;
    [SerializeField] private Image Health;
    [SerializeField] private float HealthScaleMultiplayer;
    [SerializeField] private GameObject _mine;
    [SerializeField] private float speedAppearMine;
    private Color _color;
    private bool _flag = true;

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
        if (_flag)
        {
            Health.fillAmount = 1 - (SecondsToDestroy / lifeTime);
        }

        if (Health.fillAmount <= 0.1f && _flag)
        {
            _flag = false;
            
            InvokeRepeating(nameof(Puslate), 0, 1);
            InvokeRepeating(nameof(LowerLastHealth), 0.5f, 1);
            Destroy(this.gameObject, 2.5f);
        }
    }
    private void LowerLastHealth()
    {
        Health.fillAmount -= 0.033f;
    }
    private void Puslate()
    {
        Vector3 scale = new Vector3(_mine.transform.localScale.x * HealthScaleMultiplayer, _mine.transform.localScale.y * HealthScaleMultiplayer, _mine.transform.localScale.z * HealthScaleMultiplayer);
        _mine.transform.DOScale(scale, 0.5f).From();
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
            TimeMultiplier = Mathf.Pow(speedAppearMine, allForceFields.Count - 1);
            SecondsToDestroy += Time.deltaTime * TimeMultiplier;
        }
    }

    IEnumerator LaterDestroy(float seconds = 40)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Destroy(this.gameObject);
    }

    IEnumerator LaterColliderActivate()
    {
        yield return new WaitForSecondsRealtime(0.1f);
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
        //if (other.CompareTag("Meteorite"))
        //{
        //    StartCoroutine(LaterDestroy(0));
        //}
    }

    private void OnDestroy()
    {
        allForceFields.Remove(gameObject);
    }

    

}
