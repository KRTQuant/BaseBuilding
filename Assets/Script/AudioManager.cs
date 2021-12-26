using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Audio_Sound[] sounds;
    public static AudioManager instance;

    private void Awake() {

        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return ;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Audio_Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop  = s.loop;
        }
    }

    private void Start() {
        Play("MainMenu");
    }

    public void Play(string name) {
        Audio_Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name) {
        Audio_Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
