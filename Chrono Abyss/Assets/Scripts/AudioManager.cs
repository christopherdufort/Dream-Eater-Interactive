﻿using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; 


    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop; 
        }
    }

    private void Update()
    {
        setPitchEffects();
    }

    private void Start()
    {

        if(SceneManager.GetActiveScene().name == "StartMenu")
            Play("StartMenu");

        if (SceneManager.GetActiveScene().name == "DungeonFloor")
            Play("Dungeon");
    }

    private void setPitchEffects()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.loop != true)
                s.source.pitch = Mathf.Clamp(Time.timeScale, 0.4f, 1.0f);
        }
    }

    public void Play(string name)
    {
        Sound s  = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Pitch(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = pitch; 
    }
}
