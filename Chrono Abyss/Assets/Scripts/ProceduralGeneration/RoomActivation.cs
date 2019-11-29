using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivation : MonoBehaviour
{
    public GameObject roomFogPrefab;
    private GameObject roomFogInstance;
    public EnemySpawner enemySpawnerInRoom;

    // Start is called before the first frame update
    void Start()
    {
        roomFogInstance = Instantiate(roomFogPrefab, transform.position, Quaternion.identity); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            roomFogInstance.SetActive(false);
            enemySpawnerInRoom.InitiateEnemySpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
