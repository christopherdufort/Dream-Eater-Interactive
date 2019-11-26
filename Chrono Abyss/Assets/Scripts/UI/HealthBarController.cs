using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // reference to player
    private PlayerController playerController;
    private int maxHealth;
    private RectTransform rectTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        maxHealth = playerController.getMaxHealth();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // get player health
        int currentHealth = playerController.getCurrentHealth();
        
        // calculate health bar's new scale and pos
        float scale = (float)currentHealth / maxHealth;
        float pos = calculateBarPos(scale);
        
        // apply transformations
        rectTransform.localScale = new Vector3(scale, 1, 1);
        rectTransform.position = new Vector3(pos + 100, rectTransform.position.y, rectTransform.position.z);
    }

    private float calculateBarPos(float barScale)
    {
        return (barScale * 60) - 20;
    }
}
