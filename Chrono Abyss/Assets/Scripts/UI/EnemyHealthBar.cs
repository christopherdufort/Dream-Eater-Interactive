using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private float maxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = enemy.GetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        // get player health
        float currentHealth = enemy.GetCurrentHealth();
        
        // calculate health bar's new scale and pos
        float scale = currentHealth / maxHealth;
        
        // apply transformations
        transform.localScale = new Vector3(scale, 1, 1);
    }
}