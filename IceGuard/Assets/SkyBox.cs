using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float x = 0;
    void Start()
    {

    }

    private void FixedUpdate()
    {
        x += Time.deltaTime * _speed;
        RenderSettings.skybox.SetFloat("_Rotation", x);
    }
}
