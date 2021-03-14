using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSscript : MonoBehaviour
{
    public int FPS;

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = FPS;
    }
}
