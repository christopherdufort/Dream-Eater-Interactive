using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeEnemy : Enemy
{
	protected override void AttackPlayer()
	{
		bool cooldownActive = attackCooldownCountdown > 0f;
		if (!cooldownActive)
		{
			// Instantiate Melee attack animation here, if any
			attackCooldownCountdown = attackCooldown;
		}
		SetAttackFacingPlayer();
		isAttacking = true;
	}
}
