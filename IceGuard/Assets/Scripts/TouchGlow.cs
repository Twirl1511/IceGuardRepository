using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchGlow : MonoBehaviour
{

    public Image Pointer;
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            Pointer.gameObject.SetActive(true);
            Pointer.transform.position = Input.mousePosition;
        }
        else
        {
            Pointer.gameObject.SetActive(false);
        }
        
    }
}
