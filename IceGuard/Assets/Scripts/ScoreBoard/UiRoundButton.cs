using UnityEngine;
using UnityEngine.UI;

public class UiRoundButton : MonoBehaviour
{

    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }


}
