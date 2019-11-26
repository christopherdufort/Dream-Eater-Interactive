using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCharacer : Enemy
{
	[Space]
	[Header("Combat Stats")]
	[SerializeField] protected float attackRange;
	[SerializeField] protected float attackCooldown;
	[SerializeField] protected float attackCooldownCountdown;

	[Space]
	[Header("DroppableLoot")]
	// The two lists below would've been a dictionary, unfortunately, Unity won't serialize dictionaries so that'll have to do
	[SerializeField] protected List<GameObject> droppableLoot;
	[SerializeField][Range(0f, 1f)][Tooltip ("Odds of each drop appearing upon death of enemy respective of list index number")]
	protected List<float> dropOdds;

	protected void Awake()
	{
		ValidateDroppableLootLists();
	}

	// for animator
	protected bool isAttacking;

	protected void ChargeAtPlayer()
	{
		if (target != null)
		{
			float enemyPlayerDist = ((Vector2)(target.transform.position - transform.position)).magnitude;
			if (enemyPlayerDist <= attackRange)
			{
				StopMoving();
				AttackPlayer();
			} else
			{
				MoveTowardsPlayer();
			}
		} else
		{
			Debug.Log("Player could not be found.");
		}
	}

	protected abstract void AttackPlayer();

	protected void MoveTowardsPlayer()
	{
		SetDirection(((Vector2)(target.transform.position - transform.position)).normalized);
		animator.SetFloat("moveX", direction.x);
		animator.SetFloat("moveY", direction.y);
		MoveTowardsCurrentDirection();
		isMoving = (direction.magnitude > Mathf.Epsilon);
	}

	// call this thing at the start for every enemy
	protected void EnemyInitialize()
	{
		attackCooldownCountdown = attackCooldown;
		rigidBody.freezeRotation = true;
		curHitPoints = maxHitPoints;
		target = GameObject.FindWithTag("Player");
	}

	// common things for every frame update for enemies
	protected void EnemyUpdateLoopStart()
	{
		CheckDead();
		CooldownLapse();
		isMoving = false;       // by default
		isAttacking = false;	// by default
	}

	protected void CooldownLapse()
	{
		if (attackCooldownCountdown > 0f)
		{
			attackCooldownCountdown -= Time.deltaTime;
		}
	}
	
	// ensures sprite is facing player while attacking
	protected void FacePlayerWhenAttacking()
	{
		Vector2 attackDirNorm = ((Vector2)(target.transform.position - transform.position)).normalized;
		animator.SetFloat("dirX", attackDirNorm.x);
		animator.SetFloat("dirY", attackDirNorm.y);
		isAttacking = true;

		transform.localScale = new Vector3(((attackDirNorm.x < float.Epsilon)?1:-1) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
	}

	protected override bool CheckDead()
	{
		if (curHitPoints < float.Epsilon)
		{
			PlayDeathAnimation();
			DropLoot();
			Destroy(this.gameObject);
			return true;
		}
		else return false;
	}

	protected void DropLoot()
	{
		for (int i = 0; i < droppableLoot.Count; i++)
		{
			float roll = Random.Range(0f, 1f);
			if (roll < dropOdds[i])
			{
				// TODO:
				// instantiate drop loot here
				// probably will have to ensure either the loot doesn't overlap or something, something to discuss with the group
				Debug.Log("Enemy dropped loot #" + i);
			}
		}
	}
	
	// TODO: call this on awake when we have a loot system implemented
	protected void ValidateDroppableLootLists()
	{
		if (droppableLoot.Count == 0 || dropOdds.Count == 0)
		{
			droppableLoot.Clear();
			dropOdds.Clear();
			return;
		}

		// truncate either list if even
		int difference = droppableLoot.Count - dropOdds.Count;

		if (difference > 0)
		{
			droppableLoot.RemoveRange(dropOdds.Count, droppableLoot.Count - dropOdds.Count);
		}
		else if (difference < 0)
		{
			dropOdds.RemoveRange(droppableLoot.Count, dropOdds.Count - droppableLoot.Count);
		}

		// keep odds in range
		foreach (float odds in dropOdds)
		{
			Mathf.Clamp(odds, 0f, 1f);
		}
	}
}
