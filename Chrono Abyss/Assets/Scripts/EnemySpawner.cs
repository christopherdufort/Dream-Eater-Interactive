using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// place this script in each room you wish to have enemies spawn in
public class EnemySpawner : MonoBehaviour
{
	public GameObject[] enemies;
	public Vector2 spawnValues;

	private int randEnemy;

	[SerializeField] public int spawnEnemiesAmtSet = 4;
	[SerializeField] public int curEnemiesAmt;
	
	void Start()
    {
		curEnemiesAmt = 0;

		while (curEnemiesAmt < spawnEnemiesAmtSet)
		{
			SpawnEnemy();
		}
	}
	
	void SpawnEnemy()
	{
		randEnemy = Random.Range(0, enemies.Length);

		float xPosEnemy = Random.Range(-spawnValues.x, spawnValues.x);
		float yPosEnemy = Random.Range(-spawnValues.y, spawnValues.y);
		Vector3 spawnPosition = new Vector3(xPosEnemy, yPosEnemy, 1);
		
		//if (!OverlapsACollider(spawnPosition, enemies[randEnemy]))
		//{
			GameObject enemyCreated = Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
			curEnemiesAmt++;
		//}
	}

	// function will be back when desert, ice & lava room prefabs are more complete
	/*
	bool OverlapsACollider(Vector3 spawnPosition, GameObject enemyToSpawn)
	{
		BoxCollider2D enemyToSpawnCollider = enemyToSpawn.GetComponent<BoxCollider2D>();
		Vector2 enemyToSpawnExtents = enemyToSpawnCollider.bounds.extents;
		Vector2 enemyToSpawnUpper = new Vector2(spawnPosition.x + enemyToSpawnExtents.x, spawnPosition.y + enemyToSpawnExtents.y);
		Vector2 enemyToSpawnLower = new Vector2(spawnPosition.x - enemyToSpawnExtents.x, spawnPosition.y - enemyToSpawnExtents.y);

		Collider2D[] curColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(2*spawnValues.x, 2*spawnValues.y), 0f);
		foreach (Collider2D collider in curColliders)
		{
			Vector2 colliderCenter = collider.bounds.center;
			Vector2 colliderExtents = collider.bounds.extents;

			Vector2 colUpper = new Vector2(colliderCenter.x + colliderExtents.x, colliderCenter.y + colliderExtents.y);
			Vector2 colLower = new Vector2(colliderCenter.x - colliderExtents.x, colliderCenter.y - colliderExtents.y);

			if ((spawnPosition.x <= colUpper.x) && (spawnPosition.x >= colLower.x) && (spawnPosition.y <= colUpper.y) && (spawnPosition.y >= colLower.y))
			{
				return true;
			}
			if ((colliderCenter.x <= enemyToSpawnUpper.x) && (colliderCenter.x >= enemyToSpawnLower.x) && (colliderCenter.y <= enemyToSpawnUpper.y) && (colliderCenter.y >= enemyToSpawnLower.y))
			{
				return true;
			}
		}
		return false;
	}
	*/
}
