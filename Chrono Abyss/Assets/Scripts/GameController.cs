using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerData playerData { get; private set; }

    // Sole self reference
    private static GameController gameController;
    public float initializationTime;

    [Header("References to UI Menus")]
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    [Header("Game State")]
    public bool paused;
    public bool gameover;
    public bool inMenu;
    public int goldCollected = 0;
    public int level = 1;
    private bool newGame;
    public int invincibilityCount = 0;
    public int ricochetBulletCount = 0;
    public int infiniteAmmoCount = 0;
    public int spreadShotCount = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        initializationTime = Time.realtimeSinceStartup;

        Debug.Log("The GameController is awake");
        // reset game time
        Time.timeScale = 1.0f;
    }

    //Load from save state
    public void OnEnable()
    {
        Debug.Log("Attempting to Load Game...");
        // Load existing Game
        playerData = PlayerPersistence.LoadData();

        if(playerData.PlayerLevel < 1)
        {
            Debug.Log("No save found..starting a new save");
            // Create new Data
            playerData = new PlayerData(true);
            // Save new Game
            PlayerPersistence.SaveData(playerData);
            playerData = PlayerPersistence.LoadData();
        }
        Debug.Log("GameController finished enabling");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameController Started");
    }

    //Save to save state
    public void OnDisable()
    {
        //PlayerPersistence.SaveData(this.playerData);
    }

    // Update is called once per frame
    void Update()
    {
        pause();
        BlockedByMenu();
    }

    public void setLevel(int newLevel)
    {
        level = newLevel > 0 ? newLevel : level;
        Debug.Log("Game is now in level " + level);
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

    public void setInMenu(bool isInMenu)
    {
        inMenu = isInMenu;
    }

    private void BlockedByMenu()
    {
       if (inMenu)
        {
            Time.timeScale = 0.0f;
        }
    }

    public int GetCurrentCoins()
    {
        return goldCollected;
    }
}
