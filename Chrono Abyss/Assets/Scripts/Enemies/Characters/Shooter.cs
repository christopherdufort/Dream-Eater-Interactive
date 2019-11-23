using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyCharacer
{
	[Space]
	[Header ("Projectiles")]
	[SerializeField] protected EnemyProjectile[] projectiles;

	[Space]
	[SerializeField] protected float minComfortDistance;

	// Start is called before the first frame update
	void Start()
	{
		EnemyInitialize();
	}

    // Update is called once per frame
    void Update()
	{
		EnemyUpdateLoopStart();

		Vector2 playerToEnemy = (Vector2)(transform.position - target.transform.position);
		if (playerToEnemy.magnitude < minComfortDistance)
		{
			MaintainDistanceFromPlayer(playerToEnemy);

		} else
		{
			ChargeAtPlayer();
		}

		animator.SetBool("isMoving", isMoving);
		animator.SetBool("isAttacking", isAttacking);
	}
	
	protected void MaintainDistanceFromPlayer(Vector2 playerToEnemy)
	{
		SetDirection((Vector2)transform.position + playerToEnemy);
		MoveTowardsCurrentDirection();
		AttackPlayer();
		isMoving = false;		// for animator; don't want movement animation
	}

	protected override void AttackPlayer()
	{
		if (attackCooldownCountdown <= float.Epsilon)
		{
			Instantiate(projectiles[0], transform.position, Quaternion.identity);
			attackCooldownCountdown = attackCooldown;
		}
		FacePlayerWhenAttacking();
	}

	protected void Strafe()
	{
		// TODO
	}
}
