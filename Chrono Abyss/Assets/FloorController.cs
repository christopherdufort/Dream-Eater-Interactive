﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorController : MonoBehaviour
{
    private Text floorText;
    private GameController gameController;
    private GameController[] gameControllers;

    // Start is called before the first frame update
    void Start()
    {
        floorText = GetComponent<Text>();
        // Use the correct gamecontroller instance
        gameControllers = FindObjectsOfType<GameController>();
        if (gameControllers.Length == 1)
        {
            gameController = FindObjectOfType<GameController>();
        }
        else if (gameControllers[0].initializationTime > gameControllers[1].initializationTime)
        {
            gameController = gameControllers[1];
        }
        else
        {
            gameController = gameControllers[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameController.getLevel().ToString();
        floorText.text = gameController.getLevel().ToString();
    }
}
