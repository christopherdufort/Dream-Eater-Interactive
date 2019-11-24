using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoController : MonoBehaviour
{
    private Text ammoText;
    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        ammoText = GetComponent<Text>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = playerController.getCurrentAmmo() + "/" + playerController.getMaxAmmo();
    }
}
