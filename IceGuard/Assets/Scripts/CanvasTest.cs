using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTest : MonoBehaviour
{
    public Text ForceFieldLifeTime;
    public Text NewMeteoriteTimer;
    public Text PlayerSpeed;

    private void Update()
    {
        ForceFieldLifeTime.text = NewForceField.seconds.ToString();
        NewMeteoriteTimer.text = MeteoritScriptNEW.DelayBtwNewMeteorites.ToString();
        PlayerSpeed.text = PlayerControllerDrawPath.TimeToReachNextTile.ToString();
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



    public void MinusPlayerSpeed()
    {
        
        PlayerControllerDrawPath.TimeToReachNextTile -= 0.1f;
        
    }

    public void PlusPlayerSpeed()
    {
        PlayerControllerDrawPath.TimeToReachNextTile += 0.1f;
        
    }
}
