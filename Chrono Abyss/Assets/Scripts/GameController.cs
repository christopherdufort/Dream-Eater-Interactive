using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject pauseScreen;

    private int level = 1;
    private bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        pause();
    }

    public void setLevel(int newLevel)
    {
        level = newLevel;
    }

    public int getLevel()
    {
        return level;
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
