using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyArmyLeader : Shooter
{
	[SerializeField] ProjectileMode projectileMode;
	private enum ProjectileMode { Seeker, Spray }
	[SerializeField] SkellyArmyController controller;
	[SerializeField] float roundOneEndHealthThreshold = 0.75f;
	[SerializeField] float roundTwoEndHealthThreshold = 0.5f;
	bool canAttack = true;

	private void OnEnable()
	{
		EnemyInitialize();
		FadeIn();
		curHitPoints = controller.getSkellyLeaderRemainingHealth();
		canAttack = true;
	}

	new void Update()
    {
		if (!CheckDead())
		{
			Retreat();
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
			else
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
			else
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
			else
			{
				projectileMode = ProjectileMode.Spray;
				attackCooldownCountdown = attackCooldown;
			}
		}
	}

	protected override void AttackPlayer()
	{
		if (canAttack)
		{
			Instantiate(projectiles[(int)projectileMode], transform.position, Quaternion.identity);
			StartCoroutine("AttackCooldown");
		}
	}

	IEnumerator AttackCooldown()
	{
		canAttack = false;
		yield return new WaitForSeconds(attackCooldownCountdown);
		canAttack = true;
	}

	// R1, appears when < 80 skellies remain: if heath @ 75%, bail out and notify controller
	// R2, appears when < 50 skellies remain: if health @ 50%, bail out and notify controller
	// R3: appears when < 20 skellies remain: until death
	void Retreat()
	{
		if (((curHitPoints < roundOneEndHealthThreshold * maxHitPoints) && (controller.getTimesLeaderAppeared() == 1))
			|| ((curHitPoints < roundTwoEndHealthThreshold * maxHitPoints) && (controller.getTimesLeaderAppeared() == 2)))
		{
			controller.NotifySkellyArmyLeaderRetreated(curHitPoints);
			FadeOut();
			gameObject.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		controller.NotifySkellyArmyLeaderDead();
	}

	// can't attack or be attacked during this phase
	void FadeIn()
	{

	}

	// can't attack or be attacked during this phase
	void FadeOut()
	{

	}
}
