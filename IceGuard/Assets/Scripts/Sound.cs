using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string elementName;
    public SoundName name;
    public AudioClip audioClip;
    [Range(0f,1f)] public float volume;
    [HideInInspector] public AudioSource source;
    public bool loop;
    public enum SoundName
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

    

}
