using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPrefabController : MonoBehaviour
{
    private RoomTemplates roomTemplates;

    // Start is called before the first frame update
    void Start()
    {
        roomTemplates = GameObject.FindGameObjectWithTag("RoomTemplates").GetComponent<RoomTemplates>();
        roomTemplates.enterableRooms.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
