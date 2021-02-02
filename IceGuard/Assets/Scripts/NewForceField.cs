using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewForceField : MonoBehaviour
{
    public float seconds = 30;
    private float _timer = 0;
    [SerializeField] private float HowLongToStayToTakeDamage = 2f;
    void Start()
    {
        StartCoroutine(LaterDestroy());
    }

    IEnumerator LaterDestroy()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHitPoints.HitPoints--;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _timer += Time.deltaTime;
            if (_timer >= HowLongToStayToTakeDamage)
            {
                PlayerHitPoints.HitPoints--;
                _timer = 0;
            } 
        }
    }
}
