using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;

    // These variables should be handled by chest spawner and boss spawner manager/controllers.
    public GameObject boss;
    public GameObject chest;

    public int numberOfRooms = 1; // Starts with 1 entry room
    public int maxNumberOfOpenRooms = 12; // Open rooms defined as room which have doors to enter

    public List<GameObject> enterableRooms;

    public float waitTime = 10f;
    bool haveToSpawnBoss = true;
    bool haveToSpawnChest = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This logic should be handled by chest spawner and boss spawner manager/controllers.
        if (haveToSpawnBoss && haveToSpawnChest && Time.time >= waitTime)
        {
            Instantiate(boss, enterableRooms[enterableRooms.Count - 1].transform.position, Quaternion.identity);
            Instantiate(chest, enterableRooms[enterableRooms.Count - 2].transform.position, Quaternion.identity);
            haveToSpawnBoss = false;
            Debug.Log("Boss has spawned!");
            haveToSpawnChest = false;
            Debug.Log("Chest has spawned!");

        }
    }
}
