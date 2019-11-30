using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using Random = UnityEngine.Random;

public class MenuController : MonoBehaviour
{
    private String theme;

    private void Awake()
    {
        Destroy(FindObjectOfType<PlayerController>().gameObject);
        Destroy(FindObjectOfType<GameController>().gameObject);
    }

    private void Start()
    {
        // choose a theme for the floor
        theme = ((FloorTheme) Random.Range(0, 3)).ToString() + "floor";
        
    }

    public void startGame()
    {
        //Have GameController choose a random floor type from the enum list, then load the scene associated with that theme.
        SceneManager.LoadScene(theme);
    }

    public void quitGame()
    {
        // Close the Game editor (Used in developing the game)
        EditorApplication.isPlaying = false;

        // Close the game application (used when demoing the game)
        Application.Quit();
    }
    
}

enum FloorTheme
{
    Lava,
    Ice,
    Dungeon,
    Desert
}
