using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [Header("References to UI Menus")]
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    private int level = 1;
    private FloorTheme theme;

    private void Awake()
    {
        // reset game time
        Time.timeScale = 1.0f;
        
        // choose a theme for the floor
        theme = (FloorTheme) Random.Range(0, 3);
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
        // show game over menu and set time scale to 0
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void pause()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            // Pause Game and set time scale to 0
            pauseScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}

enum FloorTheme
{
    Lava,
    Ice,
    Dungeon,
    Desert
}
