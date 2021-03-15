using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSscript : MonoBehaviour
{
    public int FPS;
    private float fps;
    Rect fpsRect;
    GUIStyle style;
    private void Start()
    {
        GUI.color = Color.white;
        fpsRect = new Rect(10, 30, 400, 100);
        style = new GUIStyle();
        style.fontSize = 30;
        
        Application.targetFrameRate = FPS;
        StartCoroutine(FrameRate());
        
    }

    IEnumerator FrameRate()
    {
        while (true)
        {
            fps = 1 / Time.deltaTime;
            yield return new WaitForSeconds(1);
        }
    }

    private void OnGUI()
    {
        GUI.Label(fpsRect, "FPS: " + fps.ToString("0.0"));
    }
}
