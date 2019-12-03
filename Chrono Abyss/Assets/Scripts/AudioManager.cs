using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; 


    // Start is called before the first frame update
    void Awake()
    {
 

        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.bypassEffects = s.bypassEffects; 
        }

        Play(SceneManager.GetActiveScene().name);

        int audioManagers = FindObjectsOfType<AudioManager>().Length;
        if (audioManagers != 1)
        {
            Destroy(gameObject);
        }
        // if more then one music player is in the scene
        //destroy ourselves
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        setPitchEffects();
    }

    private void setPitchEffects()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.loop != true && !s.source.name.ToString().Equals("PowerUp") && !s.source.name.ToString().Equals("PowerDown") && !s.source.name.ToString().Equals("PlayerHurt"))
                s.source.pitch = Mathf.Clamp(Time.timeScale, 0.4f, 1.0f);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {



        Play(SceneManager.GetActiveScene().name);

    }

    public void StopCurrent()
    {
        Stop(SceneManager.GetActiveScene().name);

    }

    public void Play(string name)
    {
        Sound s  = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void Pitch(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = pitch; 
    }
}
