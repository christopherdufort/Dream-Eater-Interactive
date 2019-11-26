using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyCharacer
{
	[Space]
	[Header ("Projectiles")]
	[SerializeField] protected GameObject[] projectiles;

	[Space]
	[SerializeField] protected float minComfortDistance;

	// Start is called before the first frame update
	protected void Start()
	{
		EnemyInitialize();
	}

    // Update is called once per frame
    protected void Update()
	{
		EnemyUpdateLoopStart();
		ShooterAction();
		animator.SetBool("isMoving", isMoving);
		animator.SetBool("isAttacking", isAttacking);
	}
	
	protected void MaintainDistanceFromPlayer(Vector2 playerToEnemy)
	{
		SetDirection(((Vector2)playerToEnemy).normalized);
		MoveTowardsCurrentDirection();
		AttackPlayer();
		isMoving = true;
		isAttacking = false;
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
	
	// determines whether shooter-type enemy should back off or go on the offensive
	protected void ShooterAction()
	{
		Vector2 playerToEnemy = (Vector2)(transform.position - target.transform.position);
		if (playerToEnemy.magnitude < minComfortDistance)
		{
			MaintainDistanceFromPlayer(playerToEnemy);

		}
		else
		{
			ChargeAtPlayer();
		}
	}

	protected void Strafe()
	{
		// TODO
	}
}
