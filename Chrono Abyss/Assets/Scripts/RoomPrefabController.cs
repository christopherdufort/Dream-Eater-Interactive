using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPrefabController : MonoBehaviour
{
    public bool showDoors = false;
    public GameObject[] doors;
    private RoomTemplates roomTemplates;

    // Start is called before the first frame update
    void Start()
    {
        if (showDoors == false)
        {
            foreach(GameObject door in doors)
            {
                door.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        roomTemplates.enterableRooms.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
