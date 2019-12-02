using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("References to UI Menus")]
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    [Header("Game State")]
    public bool paused;
    public bool gameover; 
    private int level = 1;
    private bool created;

    private void Awake()
    {
        // reset game time
        Time.timeScale = 1.0f;

        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pause();
    }

    public void setLevel(int newLevel)
    {
        level = newLevel > 0 ? newLevel : level;
    }

    public int getLevel()
    {
        return level;
    }

    public void gameOver()
    {
        gameover = true; 
        FindObjectOfType<AudioManager>().StopCurrent(); 
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        // show game over menu and set time scale to 0
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void pause()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            // Pause Game and set time scale to 0
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
