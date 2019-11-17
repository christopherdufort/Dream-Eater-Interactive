using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            // Pause Game.
            gamePaused = !gamePaused;

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
