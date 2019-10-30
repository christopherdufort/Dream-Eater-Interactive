using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TEMPORARY FILE
// This code is just from 376 Assignment #2 and meant only for testing purposes.
// Delete this when the project has a proper enemy spawner.
public class EnemySpawnerPlaceholder : MonoBehaviour
{
	public GameObject[] enemies;
	public Vector2 spawnValues;
	public float spawnWaitMax;
	public float spawnWaitMin;
	private float spawnTimer;

	public int startWait;
	public int spawnMax;

	private int randEnemy;              // index number for which enemy is going to be spawned

	[SerializeField] public int curEnemiesAmt;

	[SerializeField] public static int enemiesAmt = 0;
	
	void Start()
    {
		spawnTimer = Random.Range(spawnWaitMin, spawnWaitMax);
	}
	
    void Update()
    {
		spawnTimer -= Time.deltaTime;

		if ((curEnemiesAmt < spawnMax) && (spawnTimer <= 0f))
		{
			spawnEnemy();
			spawnTimer = Random.Range(spawnWaitMin, spawnWaitMax);
		}

		curEnemiesAmt = enemiesAmt;
	}
	
	void spawnEnemy()
	{
		randEnemy = Random.Range(0, enemies.Length);

		float xPosEnemy = Random.Range(-spawnValues.x, spawnValues.x);
		float yPosEnemy = Random.Range(-spawnValues.y, spawnValues.y);
		Vector3 spawnPosition = new Vector3(xPosEnemy, yPosEnemy, 1);

		GameObject enemyCreated = Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
		//print("spawned enemy #" + randEnemy + " at (" + xPosEnemy + "," + yPosEnemy + "," + spawnPosition.z + ")");
		enemiesAmt++;
	}
}
