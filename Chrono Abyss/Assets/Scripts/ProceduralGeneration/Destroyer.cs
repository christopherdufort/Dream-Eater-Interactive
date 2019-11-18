using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Purpose for this class is to prevent anything from spawning ontop of the player in the starting room.
 * Also served the purpose of preventing the starting room from being closed off.
 * 
 */
public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomSpawnPoint"))
        {
            other.GetComponent<RoomSpawner>().preventSpawn();
            Destroy(other.gameObject);
        }
    }
}
