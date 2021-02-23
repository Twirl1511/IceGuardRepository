using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldAnimations : MonoBehaviour
{
    [SerializeField] private GameObject ForceFieldBase;
    [SerializeField] private GameObject ForceFielAnimatonStart;
    [SerializeField] private GameObject ForceFielAnimatonIdle;
    public float AnimationLifeTime = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        
        ForceFieldBase.SetActive(true);
        ForceFielAnimatonStart.SetActive(true);
        StartCoroutine(DelayActivation(AnimationLifeTime, ForceFielAnimatonStart, false));
        StartCoroutine(DelayActivation(AnimationLifeTime, ForceFielAnimatonIdle, true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DelayActivation(float seconds, GameObject gameObject, bool state)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(state);
    }
}
