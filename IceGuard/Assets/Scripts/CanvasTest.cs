using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTest : MonoBehaviour
{
    public Text ForceFieldLifeTime;
    public Text NewMeteoriteTimer;

    private void Update()
    {
        ForceFieldLifeTime.text = NewForceField.seconds.ToString();
        NewMeteoriteTimer.text = MeteoritScriptNEW.DelayBtwNewMeteorites.ToString();
    }






    public void PlusForceFieldSecondsButton()
    {
        NewForceField.seconds++;
    }
    public void MinusForceFieldSecondsButton()
    {
        NewForceField.seconds--;
    }


    public void PlusMeteoriteTimer()
    {
        MeteoritScriptNEW.DelayBtwNewMeteorites++;
    }
    public void MinusMeteoriteTimerButton()
    {
        MeteoritScriptNEW.DelayBtwNewMeteorites--;
    }
}
