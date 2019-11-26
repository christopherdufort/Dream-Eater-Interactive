using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextController : MonoBehaviour
{
    private Text levelText;
    private GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        levelText = GetComponent<Text>();
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = gameController.getLevel() + "f";
    }
}
