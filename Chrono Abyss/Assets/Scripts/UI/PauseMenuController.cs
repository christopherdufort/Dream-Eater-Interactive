﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void resumeGame()
    {
        // turn off canvas and set time scale to 1.0
        FindObjectOfType<PlayerController>().resuming = true;
        FindObjectOfType<PlayerController>().StartCoroutine("ResumeDelay");
        Time.timeScale = 1.0f;
        FindObjectOfType<GameController>().paused = false;
        gameObject.SetActive(false);
    }

    public void quitToMenu()
    {
        FindObjectOfType<AudioManager>().StopCurrent();
        FindObjectOfType<AudioManager>().Stop("PlayerDeath");
        SceneManager.LoadScene("StartMenu");
    }
}
