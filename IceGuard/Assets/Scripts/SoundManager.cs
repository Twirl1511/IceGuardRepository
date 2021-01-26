using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    public enum Soundtest
    {
        PlayerMove,
        PlayerBlocked,
        ForceFieldCreate,
        ForceFieldDestroy,
        ForceFieldDestroySoon,
        MeteoriteAlert,
        MeteoriteMove,
        MeteoriteCrashForceField,
        MeteoriteCrashPlayer,
        MeteoriteCrashEarth,
        GameOver
    }
   
    public static void PlaySound()
    {
        
    }


}
