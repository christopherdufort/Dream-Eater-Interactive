using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuController : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void quitGame()
    {
        // Close the Game editor (Used in developing the game)
        EditorApplication.isPlaying = false;

        // Close the game application (used when demoing the game)
        Application.Quit();
    }
    
}
