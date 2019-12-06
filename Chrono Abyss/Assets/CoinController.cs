using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    private Text coinCountText;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        coinCountText = GetComponent<Text>();
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        coinCountText.text = gameController.GetCurrentCoins().ToString();
    }
}
