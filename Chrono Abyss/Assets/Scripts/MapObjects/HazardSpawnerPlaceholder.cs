using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TEMPORARY FILE
// This code is jus meant only for testing purposes.
// Delete this when the project has a proper hazard spawner.
public class HazardSpawnerPlaceholder : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector2 spawnValues;
    public float spawnWaitMax=0.1f;
    public float spawnWaitMin=0.0f;
    private float spawnTimer;

    public int startWait;
    public int spawnMax = 4; // Initial limit per room.

    public bool canSpawnHazards = true;

    [SerializeField] public int hazardCount = 0;

    private int randHazard;              // index number for which enemy is going to be spawned

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(spawnWaitMin, spawnWaitMax);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (canSpawnHazards && (hazardCount < spawnMax) && (spawnTimer <= 0f))
        {
            spawnHazard();
            spawnTimer = Random.Range(spawnWaitMin, spawnWaitMax);
        }
    }

    void spawnHazard()
    {
        randHazard = Random.Range(0, hazards.Length);

        float xPosEnemy = Random.Range(-spawnValues.x, spawnValues.x);
        float yPosEnemy = Random.Range(-spawnValues.y, spawnValues.y);
        Vector3 spawnPosition = new Vector3(xPosEnemy, yPosEnemy, 1);

        GameObject enemyCreated = Instantiate(hazards[randHazard], spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
        hazardCount++;

        if (hazardCount>= spawnMax)
        {
            canSpawnHazards = false;
            //delete this script to prevent future spawns and clean up game?
        }
    }
}
