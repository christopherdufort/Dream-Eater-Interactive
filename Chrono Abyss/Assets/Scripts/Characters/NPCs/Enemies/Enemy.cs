using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : NPC
{
	[Space]
	[Header("Combat Stats")]
	[SerializeField] protected float basicAttack;
	[SerializeField] protected float attackRange;
	[SerializeField] protected float attackCooldown;
	[SerializeField] protected float attackCooldownCountdown;

	[Header("References")]
	[SerializeField] protected GameObject playerObj;
	[SerializeField] protected Vector2 playerPos;

	protected Vector2 movementDirVecNorm = Vector2.zero;

	// for animator
	protected bool isAttacking;
	
	protected void ChargeAtPlayer()
	{
		if (playerObj != null)
		{
			float enemyPlayerDist = (playerPos - (Vector2)transform.position).magnitude;
			if (enemyPlayerDist <= attackRange)
			{
				StopMoving();
				AttackPlayer();
			} else
			{
				MoveTowardsPlayer();
			}
		}
	}

	protected abstract void AttackPlayer();

	protected void MoveTowardsPlayer()
	{
		Vector2 movementDirVec = playerPos - (Vector2)this.transform.position;
		movementDirVecNorm = movementDirVec.normalized;
		animator.SetFloat("moveX", movementDirVecNorm.x);
		animator.SetFloat("moveY", movementDirVecNorm.y);
		moveTowards(movementDirVecNorm);
		if (movementDirVec.magnitude > Mathf.Epsilon)
		{
			isMoving = true;
		}
	}

	protected void moveTowards(Vector2 movementDirVec)
	{
		float xMove = transform.position.x + movementDirVec.x * moveSpeed * Time.deltaTime;
		float yMove = transform.position.y + movementDirVec.y * moveSpeed * Time.deltaTime;
		rigidBody.MovePosition(new Vector2(xMove, yMove));

	}

	// call this thing at the start for every enemy
	protected void EnemyInitialize()
	{
		attackCooldownCountdown = attackCooldown;
		rigidBody.freezeRotation = true;
		curHitPoints = maxHitPoints;
		playerObj = GameObject.FindWithTag("Player");
	}

	// common things for every frame update for enemies
	protected void EnemyUpdateLoopStart()
	{
		CheckDead();
		CooldownLapse();
		isMoving = false;       // by default
		isAttacking = false;	// by default
		playerPos = playerObj.transform.position;
	}

	protected void CooldownLapse()
	{
		if (attackCooldownCountdown > 0f)
		{
			attackCooldownCountdown -= Time.deltaTime;
		}
	}

	// TODO: This was made to go with the placeholder enemy sprites which have "west" as a default direction.
	protected void FlipSprite()
	{
		if (movementDirVecNorm.x > float.Epsilon)
		{
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		} else
		{
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
	}
	
	// ensures sprite is facing player while attacking
	protected void SetAttackFacingPlayer()
	{
		Vector2 attackDirNorm = (playerPos - (Vector2)transform.position).normalized;
		animator.SetFloat("dirX", attackDirNorm.x);
		animator.SetFloat("dirY", attackDirNorm.y);
		isAttacking = true;

		// flip sprite towards player while attacking
		int flipSpriteX = -1;
		if (attackDirNorm.x < float.Epsilon)
		{
			flipSpriteX = 1;
		}
		transform.localScale = new Vector3(flipSpriteX * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
	}
}
