using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeEnemy : EnemyCharacer
{
	protected void Start()
	{
		EnemyInitialize();
	}
	
	protected void Update()
	{
		EnemyUpdateLoopStart();
	}
	protected override void AttackPlayer()
	{
		if (attackCooldownCountdown < float.Epsilon)
		{
			// TODO: function for attacking player
			isAttacking = true;
			FacePlayerWhenAttacking();
			attackCooldownCountdown = attackCooldown;
		}
		else isAttacking = false;
	}

	protected new void EnemyUpdateLoopStart()
	{
		base.EnemyUpdateLoopStart();
		ChargeAtPlayer();
		animator.SetBool("isMoving", isMoving);
		animator.SetBool("isAttacking", isAttacking);
	}
}
