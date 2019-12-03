using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing : Shooter
{
	[SerializeField] AttackMode attackMode;
	[SerializeField] float circleAttackDist, oscilatingAttackDist, seekerAttackDist;
	[SerializeField] float intermediaryAttackRange;
	private enum AttackMode
	{
		Circle,
		Spray,
		Oscillating,
		Seeker
	}

	protected new void Update()
	{
		NotifyIfBossDead();
		EnemyUpdateLoopStart();
		DetermineAttackMode();
		ShooterAction();
		animator.SetBool("isMoving", isMoving);
		animator.SetBool("isAttacking", isAttacking);
	}

	void NotifyIfBossDead()
	{
		if (curHitPoints <= Mathf.Epsilon)
		{
			FindObjectOfType<BossController>().BossDied();
		}
	}

	protected override void AttackPlayer()
	{
		if (attackCooldownCountdown <= float.Epsilon)
		{
			switch(attackMode)
			{
				case AttackMode.Circle:
					Instantiate(projectiles[(int)AttackMode.Circle], transform.position, Quaternion.identity);
					attackCooldownCountdown = attackCooldown;
					break;
				case AttackMode.Oscillating:
					Instantiate(projectiles[(int)AttackMode.Oscillating], transform.position, Quaternion.identity);
					attackCooldownCountdown = attackCooldown * 0.2f;
					break;
				case AttackMode.Seeker:
					Instantiate(projectiles[(int)AttackMode.Seeker], transform.position, Quaternion.identity);
					attackCooldownCountdown = attackCooldown * 0.2f;
					break;
				case AttackMode.Spray:
					Instantiate(projectiles[(int)AttackMode.Spray], transform.position, Quaternion.identity);
					attackCooldownCountdown = attackCooldown * 0.5f;
					break;
				default:
					break;
			}
		}
		FacePlayerWhenAttacking();
	}

	protected void DetermineAttackMode()
	{
		float distToPlayer = ((Vector2)(target.transform.position - transform.position)).magnitude;
		if (distToPlayer > minComfortDistance)
		{
			float rand = Random.Range(0f, 1f);
			if (rand > 0.6f)
			{
				attackMode = AttackMode.Circle;
			} else if (rand > 0.3f)
			{
				attackMode = AttackMode.Oscillating;
			} else
			{
				attackMode = AttackMode.Seeker;
			}
		} else
		{
			attackMode = AttackMode.Spray;
		}
	}
}