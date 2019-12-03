using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OpenChest : MonoBehaviour
{
    private Animator anim;
    public GameObject[] powerups;
    bool lootInside;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lootInside = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Open");
            if (lootInside)
            {
                SpawnPowerUp();
                lootInside = false;
            }
        }

        
    }

    private void SpawnPowerUp()
    {
        Debug.Log("spawned powerup");
        int powerUpIndex = Random.Range(0, powerups.Length);
        GameObject powerup = Instantiate(powerups[powerUpIndex], transform.position, Quaternion.identity);
    }
}
