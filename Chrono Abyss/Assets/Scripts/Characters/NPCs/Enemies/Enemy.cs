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
	[SerializeField] protected float attackCooldownTimer;

	[Header("References")]
	[SerializeField] protected GameObject playerObj;
	[SerializeField] protected Vector2 playerPos;

	protected Vector2 movementDirVecNorm = Vector2.zero;

	// for animator
	protected bool isAttacking;
	
	protected void AttemptAttackPlayer()
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

	protected void GetPlayerCoordinates()
	{
		if (playerObj == null)
		{
			playerObj = GameObject.FindWithTag("Player");
			if (playerObj != null)
			{
				playerPos = playerObj.transform.position;
			}
		}
		else
		{
			playerPos = playerObj.transform.position;
		}
	}

	protected void MoveTowardsPlayer()
	{
		Vector2 movementDirVec = playerPos - (Vector2)this.transform.position;
		movementDirVecNorm = movementDirVec.normalized;
		animator.SetFloat("moveX", movementDirVecNorm.x);
		animator.SetFloat("moveY", movementDirVecNorm.y);

		float xMove = transform.position.x + movementDirVec.x * moveSpeed * Time.deltaTime;
		float yMove = transform.position.y + movementDirVec.y * moveSpeed * Time.deltaTime;
		rigidBody.MovePosition(new Vector2(xMove, yMove));
		if (movementDirVec.magnitude > Mathf.Epsilon)
		{
			isMoving = true;
		}
	}

	// call this thing at the start for every enemy
	protected void EnemyInitialize()
	{
		attackCooldownTimer = attackCooldown;
		rigidBody.freezeRotation = true;
		curHitPoints = maxHitPoints;
		GetPlayerCoordinates();
	}

	// common things for every frame update for enemies
	protected void EnemyUpdateLoopStart()
	{
		isMoving = false;       // by default
		isAttacking = false;	// by default
		CooldownActive();
		CheckDead();
		GetPlayerCoordinates();
	}

	protected void CooldownActive()
	{
		bool cooldownActive = attackCooldownTimer > 0f;
		if (cooldownActive)
		{
			attackCooldownTimer -= Time.deltaTime;
		}
	}

	// TODO: This was made to go with the placeholder enemy sprites which have "west" as a default direction.
	protected void FlipSprite()
	{
		if (movementDirVecNorm.x > float.Epsilon)
		{
			Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		} else
		{
			Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
	}
}
