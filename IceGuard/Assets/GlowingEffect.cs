using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GlowingEffect : MonoBehaviour
{
    public static GlowingEffect singleton;
    [SerializeField] private Renderer rend1;
    [SerializeField] private Renderer rend2;
    [SerializeField] private float _intensity;
    [SerializeField] private float _time;

    private void Start()
    {
        singleton = this;
    }

    public void ChangeColor()
    {
        rend1.material.DOFloat(_intensity, "Intensity", _time).From(0);
        rend2.material.DOFloat(_intensity, "Intensity", _time).From(0);
        StartCoroutine(BackToDefault());
    }

    IEnumerator BackToDefault()
    {
        yield return new WaitForSeconds(_time);
        rend1.material.DOFloat(0, "Intensity", _time);
        rend2.material.DOFloat(0, "Intensity", _time);
    }


}
