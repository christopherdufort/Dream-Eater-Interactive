using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorController : MonoBehaviour
{
    private Text floorText;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        floorText = GetComponent<Text>();
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        gameController.getLevel().ToString();
        floorText.text = gameController.getLevel().ToString();
    }
}
