using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // reference to player
    [SerializeField] private PlayerController playerController;
	[SerializeField] private int maxHealth;
    private RectTransform rectTransform;
    private RectTransform clockBar;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        maxHealth = playerController.getMaxHealth();
        rectTransform = GetComponent<RectTransform>();
        clockBar = GameObject.Find("ClockBar").GetComponent<RectTransform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // get player health
        int currentHealth = playerController.getCurrentHealth();
        
        // calculate health bar's new scale and pos
        float scale = currentHealth * 0.4545455f / maxHealth;

        //float pos = calculateBarPos(scale);

        // apply transformations
        rectTransform.localScale = new Vector3(scale, 0.4545455f, 1);
        //rectTransform.position = new Vector3(pos + 100, rectTransform.position.y, rectTransform.position.z);
    }
}
