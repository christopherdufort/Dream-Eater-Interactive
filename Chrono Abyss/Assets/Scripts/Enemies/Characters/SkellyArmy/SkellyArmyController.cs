using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyArmyController : MonoBehaviour
{
	[SerializeField] float maxHealth, curHealth;
	[SerializeField] int skelliesTotalAmt, skelliesPresent;
	[SerializeField] EnemySpawner skellySpawner;
	[SerializeField] GameObject[] spawnPoint;
	private float soldierHealth;
	private bool skellyLeaderSpawned;
	[SerializeField] int timesLeaderAppeared;

    // Start is called before the first frame update
    void Awake()
    {
		soldierHealth = 0.9f * (1 / skelliesTotalAmt) * maxHealth;
		curHealth = maxHealth;
		skellyLeaderSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (!CheckDead())
		{
			if ((skelliesTotalAmt < 10) && (!skellyLeaderSpawned))
			{
				// TODO: spawn skelly army leader
			}

			if (skelliesPresent < 5)
			{
				if (skelliesTotalAmt > 15)
				{
					// TODO: spawn skelly
					for(int i = 0; i < 10; i++)
					{
						
					}
				}
			}
		}
    }

	public void NotifySkellyLeaderSpawned()
	{
		skellyLeaderSpawned = true;
		++timesLeaderAppeared;
	}

	public void NotifySkellySoldierSpawned()
	{
		++skelliesPresent;
	}

	public void NotifySkellySoldierDead()
	{
		curHealth -= soldierHealth;
		--skelliesTotalAmt;
		--skelliesPresent;
	}

	public void NotifySkellyArmyLeaderRetreated(float hitPointsLeft)
	{

	}

	public void NotifySkellyArmyLeaderDead()
	{
		curHealth -= maxHealth * 0.1f;
	}

	bool CheckDead()
	{
		return (curHealth < soldierHealth * 0.1f);	// 0.1f is just an arbitrary value, trying to account for floating-point inaccuracy
	}

	public int getTimesLeaderAppeared()
	{
		return timesLeaderAppeared;
	}

	// TODO: Notify Game Controller when dead
}