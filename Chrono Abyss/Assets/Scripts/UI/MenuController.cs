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
        //Have GameController choose a random floor type from the enum list, then load the scene associated with that theme.
        SceneManager.LoadScene("DungeonFloor");
        //SceneManager.LoadScene("LavaFloor");
    }

    public void quitGame()
    {
        // Close the Game editor (Used in developing the game)
        EditorApplication.isPlaying = false;

        // Close the game application (used when demoing the game)
        Application.Quit();
    }
    
}
