using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door
    public int openingDirection;

    private RoomTemplates roomTemplates;
    private int randRoomIndex;
    private bool hasSpawnedRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        // Tag attached to only one object, this object manages all rooms
        roomTemplates = GameObject.FindGameObjectWithTag("RoomTemplates").GetComponent<RoomTemplates>();

        // Invoke method with delay to avoid spawn collisions
        Invoke("SpawnRooms", Random.Range(0.1f, 0.2f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRooms()
    { 
        // Limit spawning to one room per room spawn point
        if (hasSpawnedRoom == false)
        {
            // Ensure the roomcount does not grow out of control
            if (roomTemplates.numberOfRooms >= roomTemplates.maxNumberOfOpenRooms)
            {
                Instantiate(roomTemplates.closedRoom, transform.position, Quaternion.identity);
            }
            else if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                randRoomIndex = Random.Range(0, roomTemplates.bottomRooms.Length);
                Instantiate(roomTemplates.bottomRooms[randRoomIndex], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                randRoomIndex = Random.Range(0, roomTemplates.topRooms.Length);
                Instantiate(roomTemplates.topRooms[randRoomIndex], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                randRoomIndex = Random.Range(0, roomTemplates.leftRooms.Length);
                Instantiate(roomTemplates.leftRooms[randRoomIndex], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                randRoomIndex = Random.Range(0, roomTemplates.rightRooms.Length);
                Instantiate(roomTemplates.rightRooms[randRoomIndex], transform.position, Quaternion.identity);
            }
            hasSpawnedRoom = true;
            roomTemplates.numberOfRooms++;
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // If spawnpoint overlaps another spawnpoint
        if (otherCollider.CompareTag("RoomSpawnPoint"))
        {
            if (otherCollider.GetComponent<RoomSpawner>().hasSpawnedRoom == false && this.hasSpawnedRoom == false)
            {
                Instantiate(roomTemplates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            hasSpawnedRoom = true;   
        }
    }

    public void preventSpawn()
    {
        this.hasSpawnedRoom = true;
    }

}
