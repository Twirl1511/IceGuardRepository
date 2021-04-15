using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GlowingEffect : MonoBehaviour
{
    //[SerializeField] private Material _playerMaterial;
    Renderer rend;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        InvokeRepeating(nameof(ChangeColor), 1, 2);
    }

    private void ChangeColor()
    {
        //print(rend.material.shader.FindPropertyIndex("Metallic"));
        //_playerMaterial.SetFloat(7, 8);
        ////_playerMaterial.DOFloat(8, "_Intensity", 1);
        //print(Shader.PropertyToID("Intensity"));

    }


}
