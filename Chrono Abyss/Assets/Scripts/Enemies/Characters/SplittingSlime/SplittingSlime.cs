using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script pertaining to the individual splitting slime
public class SplittingSlime : Shooter
{
	[SerializeField] SplittingSlimeController controller;
	[SerializeField] SplittingSlimeSpawner spawner;
	[SerializeField] int roundNumber; // how many splits have occured?
	[SerializeField] float splitThreshold; // percent of health remaining before attempting split
	[SerializeField] float invincibilityPeriod = 0.75f; // invincible for a couple seconds from spawn time

	private new void Awake()
    {
		controller = GameObject.FindGameObjectWithTag("SplittingSlimeController").GetComponent<SplittingSlimeController>();
		base.Awake();
		curHitPoints = maxHitPoints;
		controller.AddSlime(this);
		//canGetSlashed = false;
	}

    // Update is called once per frame
    private new void Update()
    {
		if (invincibilityPeriod > 0f)
		{
			invincibilityPeriod -= Time.deltaTime;
			// has the split slime's grace period ended?
			if (invincibilityPeriod < Mathf.Epsilon)
			{
				//canGetSlashed = true;
			}
		}

		if (!CheckDead())
		{
			Split();
			if (invincibilityPeriod < Mathf.Epsilon)
			{
				base.Update();
			}
		}
    }

	private void Split()
	{
		if (curHitPoints < splitThreshold * maxHitPoints)
		{
			if (roundNumber < 8)
			{
				controller.NotifyHealthLost(maxHitPoints - curHitPoints);
				spawner.Spawn(roundNumber, transform.position);
				Destroy(gameObject);
			}
		}
	}

	private new bool CheckDead()
	{
		if (curHitPoints < float.Epsilon)
		{
			PlayDeathAnimation();
			DropLoot();
			controller.NotifyHealthLost(maxHitPoints);
			Destroy(this.gameObject);
			return true;
		}
		else return false;
	}

	public void Die()
	{
		curHitPoints = 0f;
	}
	protected override void AttackPlayer()
	{
		if (attackCooldownCountdown <= float.Epsilon)
		{
			Instantiate(projectiles[roundNumber], transform.position, Quaternion.identity);
			attackCooldownCountdown = attackCooldown;
		}
		FacePlayerWhenAttacking();
	}
}