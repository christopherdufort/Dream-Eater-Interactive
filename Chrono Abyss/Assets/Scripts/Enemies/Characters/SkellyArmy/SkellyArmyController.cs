using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyArmyController : MonoBehaviour
{
	[SerializeField] float maxHealth, curHealth;
	[SerializeField] int skelliesOnBench = 100, skelliesPresent;
	[SerializeField] GameObject[] spawnPoint;
	private float healthValueOfSoldier, healthValueOfLeader;
	[SerializeField] private int skellyLeaderAmt = 1;
	[SerializeField] private int timesLeaderAppeared;
	private float skellyLeaderRemainingHealth;
	[SerializeField] GameObject skellyLeader, skellySoldier;

	// Start is called before the first frame update
	void Awake()
	{
		healthValueOfLeader = 0.1f * maxHealth;
		healthValueOfSoldier = (0.9f * maxHealth) / skelliesOnBench;
		curHealth = maxHealth;
		skellyLeaderRemainingHealth = skellyLeader.GetComponent<SkellyArmyLeader>().GetCurrentHealth();
	}

	private void Start()
	{
		SpawnSkellies(20);
	}

	// Update is called once per frame
	void Update()
	{
		if (!CheckDead())
		{
			SkellyLeaderPhases();
			RespawnSkellies();
		} else
		{
			Debug.Log("Skelly boss killed");
			// TODO: all the stuff that pertains to boss fight victory
		}
	}

	// determines when the skelly leader phases occur
	private void SkellyLeaderPhases()
	{
		// Phase One
		if ((skelliesOnBench < 70) && (skellyLeaderAmt == 1) && (timesLeaderAppeared == 0))
		{
			SpawnSkellyLeader();
		}
		// Phase Two
		if ((skelliesOnBench < 50) && (skellyLeaderAmt == 1) && (timesLeaderAppeared == 1))
		{
			SpawnSkellyLeader();
		}
		// Phase Three (Final)
		if ((skelliesOnBench < 20) && (skellyLeaderAmt == 1) && (timesLeaderAppeared == 2))
		{
			SpawnSkellies(skelliesOnBench);
			SpawnSkellyLeader();
		}
	}

	private void RespawnSkellies()
	{
		if ((skelliesPresent < 10) && (skelliesOnBench > 10))
		{
			SpawnSkellies(10);
		}
	}

	private void SpawnSkellies(int amt)
	{
		for (int i = 0; i < amt; i++)
		{
			SpawnSkelly();
		}
	}

	private void SpawnSkelly()
	{
		int i = Random.Range(0, 3);
		Vector3 offset = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0f);
		Instantiate(skellySoldier, spawnPoint[i].transform.position + offset, Quaternion.identity);
		++skelliesPresent;
		--skelliesOnBench;
	}

	private void SpawnSkellyLeader()
	{
		// reposition skeleton leader
		skellyLeader.transform.position = new Vector2(Random.Range(spawnPoint[2].transform.position.x, spawnPoint[0].transform.position.x),
								Random.Range(spawnPoint[2].transform.position.y, spawnPoint[0].transform.position.y));

		skellyLeader.SetActive(true);
		++timesLeaderAppeared;
		--skellyLeaderAmt;
	}

	public void NotifySkellyLeaderSpawned()
	{
		++timesLeaderAppeared;
	}

	public void NotifySkellySoldierSpawned()
	{
		++skelliesPresent;
	}

	public void NotifySkellySoldierDead()
	{
		curHealth -= healthValueOfSoldier;
		--skelliesPresent;
	}

	public void NotifySkellyArmyLeaderRetreated(float hitPointsLeft)
	{
		skellyLeaderRemainingHealth = hitPointsLeft;
		++skellyLeaderAmt;
	}

	public void NotifySkellyArmyLeaderDead()
	{
		curHealth -= healthValueOfLeader;
	}

	bool CheckDead()
	{
		return (curHealth < healthValueOfSoldier * 0.5);  // just a low arbitrary value, trying to account for floating-point inaccuracy
	}

	public int getTimesLeaderAppeared()
	{
		return timesLeaderAppeared;
	}

	public float getSkellyLeaderRemainingHealth()
	{
		return skellyLeaderRemainingHealth;
	}
}