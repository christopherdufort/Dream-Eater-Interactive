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
    public GameObject boss;

    public int numberOfRooms = 1;
    public int maxNumberOfOpenRooms = 12;

    public List<GameObject> enterableRooms;

    public float waitTime = 5f;
    bool haveToSpawnBoss = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (haveToSpawnBoss && Time.time >= waitTime)
        {
            Instantiate(boss, enterableRooms[enterableRooms.Count - 1].transform.position, Quaternion.identity);
            haveToSpawnBoss = false;
            Debug.Log("Boss has spawned!");
        }
    }
}
