using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public void resumeGame()
    {
        // turn off canvas and set time scale to 1.0
    }

    public void quitToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
