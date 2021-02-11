using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkyBox : MonoBehaviour
{
    [SerializeField] private float SkyBoxSpeed;


    private void Start()
    {
        RenderSettings.skybox.SetFloat("_Rotation", 200);
    }
    // Update is called once per frame
    void Update()
    {
        float rotation = 200 + (Time.time * SkyBoxSpeed);
        RenderSettings.skybox.SetFloat("_Rotation", rotation);
    }
}
