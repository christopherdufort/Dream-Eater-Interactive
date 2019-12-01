using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private String currentScene;

    // Start is called before the first frame update
    void Awake()
    {
        currentScene = ""; 

        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sounds)
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
        CheckScene();
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

    public void CheckScene()
    {
        if (!currentScene.Equals(SceneManager.GetActiveScene().name))
        {
            if(currentScene.Length != 0)
                Stop(currentScene); 

            currentScene = SceneManager.GetActiveScene().name;


            if (currentScene == "StartMenu")
                Play("StartMenu");

            if (currentScene == "DungeonFloor")
                Play("DungeonFloor");

            if (currentScene == "IceFloor")
                Play("IceFloor");

            if (currentScene == "LavaFloor")
                Play("LavaFloor");

            if (currentScene == "DesertFloor")
                Play("DesertFloor");

            if (currentScene == "BossFightScene")
                Play("BossFightScene");

        }
    }
}
