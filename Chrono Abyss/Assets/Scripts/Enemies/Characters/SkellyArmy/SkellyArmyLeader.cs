using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a stationary enemy that appears at the end of the skelly army boss
public class SkellyArmyLeader : Shooter
{
	[SerializeField] ProjectileMode projectileMode;
	private enum ProjectileMode { Seeker, Spray }
	[SerializeField] SkellyArmyController controller;

    new void Update()
    {
		if (CheckDead())
		{
			DetermineAttackMode();
			AttackPlayer();
		}
    }

	void DetermineAttackMode()
	{
		float rand = Random.Range(0f, 1f);
		float distToPlayer = Vector2.Distance(target.transform.position, transform.position);

		if (distToPlayer > attackRange)
		{
			if (rand > 0.7f)
			{
				projectileMode = ProjectileMode.Seeker;
				attackCooldownCountdown = attackCooldown * 0.5f;
			}
			else;
			{
				projectileMode = ProjectileMode.Spray;
				attackCooldownCountdown = attackCooldown;
			}
		}
		else if (distToPlayer > minComfortDistance)
		{
			if (rand > 0.5f)
			{
				projectileMode = ProjectileMode.Seeker;
				attackCooldownCountdown = attackCooldown * 0.5f;
			}
			else;
			{
				projectileMode = ProjectileMode.Spray;
				attackCooldownCountdown = attackCooldown;
			}
		}
		else
		{
			if (rand > 0.3f)
			{
				projectileMode = ProjectileMode.Seeker;
				attackCooldownCountdown = attackCooldown * 0.5f;
			}
			else;
			{
				projectileMode = ProjectileMode.Spray;
				attackCooldownCountdown = attackCooldown;
			}
		}
	}

	new void AttackPlayer()
	{
		Instantiate(projectiles[(int)projectileMode], transform.position, Quaternion.identity);
	}

	void Retreat()
	{
		controller.NotifySkellyArmyLeaderRetreated(curHitPoints);
		gameObject.SetActive(false);
		// notify SkellyArmyController
		// need health registered
	}

	// R1, appears when < 60 remain: if heath @ 75%, bail out and notify controller
	// R2, appears when < 30 remain: if health @ 50%, bail out and notify controller
	// R3: until death

	// fade in / fade out
	// can't attack or be attacked during these phases
}
