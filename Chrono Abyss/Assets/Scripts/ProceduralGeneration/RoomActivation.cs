using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivation : MonoBehaviour
{
    public GameObject roomFog;
    private GameObject roomFogObject;
    public EnemySpawner enemySpawnerInRoom;

    // Start is called before the first frame update
    void Start()
    {
        roomFogObject = Instantiate(roomFog, transform.position, Quaternion.identity); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            roomFogObject.SetActive(false);
            enemySpawnerInRoom.InitiateEnemySpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
