using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // reference to player
    private Shooter enemyController;
    private float maxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInParent<Shooter>();
        maxHealth = enemyController.GetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        // get player health
        float currentHealth = enemyController.GetCurrentHealth();
        
        // calculate health bar's new scale and pos
        float scale = currentHealth / maxHealth;
        
        // apply transformations
        transform.localScale = new Vector3(scale, 1, 1);
    }
}