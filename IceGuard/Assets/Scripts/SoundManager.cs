using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    //public enum SoundName
    //{
    //    PlayerMove,
    //    PlayerBlocked,
    //    ForceFieldCreate,
    //    ForceFieldDestroy,
    //    ForceFieldDestroySoon,
    //    MeteoriteAlert,
    //    MeteoriteMove,
    //    MeteoriteCrashForceField,
    //    MeteoriteCrashPlayer,
    //    MeteoriteCrashEarth,
    //    GameOver
    //}

    private void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }
    public void PlaySound(Sound.SoundName name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source.isPlaying)
        {
            return;
        }
        s.source.Play();
    }
    public void PlaySoundOneShot(Sound.SoundName name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.PlayOneShot(s.audioClip);
    }

    private void Start()
    {
        FindObjectOfType<SoundManager>().PlaySound(Sound.SoundName.Ambience);
    }
}
