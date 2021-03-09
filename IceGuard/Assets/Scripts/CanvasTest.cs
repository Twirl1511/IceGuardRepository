using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTest : MonoBehaviour
{
    public Text ForceFieldLifeTime;
    public Text ForceFieldLifeTimeCommon;
    public Text NewMeteoriteTimer;
    public Text PlayerSpeed;

    private void Update()
    {
        ForceFieldLifeTime.text = NewForceField.lifeTime.ToString();
        NewMeteoriteTimer.text = MeteoritScriptNEW.DelayBtwNewMeteorites.ToString();
        PlayerSpeed.text = NewPlayerController.TimeToReachNextTile.ToString();
        ForceFieldLifeTimeCommon.text = NewForceField.NewLifeTime.ToString();
    }






    public void PlusForceFieldSecondsButton()
    {
        NewForceField.lifeTime++;
    }
    public void MinusForceFieldSecondsButton()
    {
        NewForceField.lifeTime--;
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

        NewPlayerController.TimeToReachNextTile -= 0.1f;
        
    }

    public void PlusPlayerSpeed()
    {
        NewPlayerController.TimeToReachNextTile += 0.1f;
        
    }
}
