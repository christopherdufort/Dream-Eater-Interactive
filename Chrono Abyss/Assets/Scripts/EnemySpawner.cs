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
		
		if (!OverlapsACollider(spawnPosition, enemies[randEnemy]))
		{
			GameObject enemyCreated = Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
			curEnemiesAmt++;
		}
	}

	bool OverlapsACollider(Vector3 spawnPosition, GameObject enemyToSpawn)
	{
		BoxCollider2D enemyToSpawnCollider = enemyToSpawn.GetComponent<BoxCollider2D>();

		Collider2D[] curColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(2*spawnValues.x, 2*spawnValues.y), 0f);
		foreach (Collider2D collider in curColliders)
		{
			Vector3 colliderCenter = collider.bounds.center;
			float horizontalExtent = collider.bounds.extents.x;
			float verticalExtent = collider.bounds.extents.y;

			Vector2 upper = new Vector2(colliderCenter.x + horizontalExtent, colliderCenter.y + verticalExtent);
			Vector2 lower = new Vector2(colliderCenter.x - horizontalExtent, colliderCenter.y - verticalExtent);

			if ((spawnPosition.x <= upper.x) && (spawnPosition.x >= lower.x) && (spawnPosition.y <= upper.y) && (spawnPosition.y >= lower.y))
			{
				return true;
			}
		}
		return false;
	}
}
