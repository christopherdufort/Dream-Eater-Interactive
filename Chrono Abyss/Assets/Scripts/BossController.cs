using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject[] bossPrefabs;
    private int randomBossPosition;

    // Start is called before the first frame update
    void Start()
    {
        randomBossPosition = Random.Range(0, bossPrefabs.Length);
        // Sawn a random boss in the middle of the room
        Instantiate(bossPrefabs[randomBossPosition],transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
