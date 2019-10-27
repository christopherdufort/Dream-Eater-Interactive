using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] enemies;
	public Vector2 spawnValues;
	public float spawnWait;
	public float spawnWaitMin;
	public float spawnWaitMax;

	public int startWait;
	public bool stop;
	public int spawnMax;

	private int randEnemy;              // index number for which enemy is going to be spawned
	private float spawnTimer;

	[SerializeField]
	public int curEnemiesAmt;

	public static int EnemiesAmt = 0;
	
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

		curEnemiesAmt = EnemiesAmt;
	}
	
	void spawnEnemy()
	{
		randEnemy = Random.Range(0, enemies.Length);

		float xPosEnemy = Random.Range(-spawnValues.x, spawnValues.x);
		float yPosEnemy = Random.Range(-spawnValues.y, spawnValues.y);
		Vector3 spawnPosition = new Vector3(xPosEnemy, yPosEnemy, 1);

		GameObject enemyCreated = Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
		//print("spawned enemy #" + randEnemy + " at (" + xPosEnemy + "," + yPosEnemy + "," + spawnPosition.z + ")");
		EnemiesAmt++;
	}
}
