using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using Random = UnityEngine.Random;

public enum FloorTheme
{
    Lava,       //0
    Ice,        //1
    Dungeon,    //2
    Desert      //3
}


public class MenuController : MonoBehaviour
{
    private String randomFloorScene;

    private void Awake()
    {
        if (FindObjectOfType<PlayerController>())
        {
            Destroy(FindObjectOfType<PlayerController>().gameObject);
        }
        if (FindObjectOfType<GameController>())
        {
            Destroy(FindObjectOfType<GameController>().gameObject);
        } 
        if (FindObjectOfType<CameraController>())
        {
            Destroy(FindObjectOfType<CameraController>().gameObject);
        }
    }

    private void Start()
    {
        // Choose a random theme for the floor -> imagine an array FloorTheme[position]
        randomFloorScene = ((FloorTheme)Random.Range(0, 4)).ToString() + "Floor";
        Time.timeScale = 1.0f; 
    }

    public void startGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopCurrent();
        // May move this logic to the gamecontroller.
        Debug.Log("Current Floor Theme: " + randomFloorScene);
        SceneManager.LoadScene(randomFloorScene);
    }

    public void quitGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        // Close the Game editor (Used in developing the game)
        //EditorApplication.isPlaying = false;

        // Close the game application (used when demoing the game)
        Application.Quit();
    }
    
}