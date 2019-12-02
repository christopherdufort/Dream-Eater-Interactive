using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDamage : MonoBehaviour
{
    public int damage = 1;

    private bool canDamage = true;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (canDamage && other.gameObject.CompareTag("Player"))
        {
            int playerHealth = other.gameObject.GetComponent<PlayerController>().getCurrentHealth();
            other.gameObject.GetComponent<PlayerController>().setCurrentHealth(playerHealth - damage);
            
            // delay so player isnt immediately killed
            StartCoroutine("DamageDelay");
        }
    }


    IEnumerator DamageDelay()
    {
        canDamage = false;
        yield return new WaitForSecondsRealtime(2f);
        canDamage = true;
    }
}
