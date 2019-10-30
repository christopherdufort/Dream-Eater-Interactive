using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
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
		FlipSprite();

		Vector2 movementDirVec = (Vector2)transform.position - playerPos;
		if (movementDirVec.magnitude < minComfortDistance)
		{
			MaintainDistanceFromPlayer(movementDirVec.normalized);

		} else
		{
			ChargeAtPlayer();
		}

		animator.SetBool("isMoving", isMoving);
		animator.SetBool("isAttacking", isAttacking);
	}
	
	protected void MaintainDistanceFromPlayer(Vector2 movementDirVecNorm)
	{
		moveTowards(movementDirVecNorm);
		AttackPlayer();
		isMoving = false;
	}

	protected override void AttackPlayer()
	{
		bool cooldownActive = attackCooldownCountdown > 0f;
		if (!cooldownActive)
		{
			Instantiate(projectiles[0], transform.position, Quaternion.identity);
			attackCooldownCountdown = attackCooldown;
		}
		SetAttackFacingPlayer();
	}

	protected void Strafe()
	{
		// TODO
	}
}
