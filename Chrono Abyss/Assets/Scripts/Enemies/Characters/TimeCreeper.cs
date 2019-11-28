using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCreeper : Shooter
{
	[SerializeField] float fullCombatPhaseHealthThreshold;	// % of health left before entering full combat mode
	[SerializeField] OperationMode operationMode;
	private enum OperationMode { Chase, FullCombat }
	private enum ProjectileType { Basic, Seeker, Spray }

	new void Awake()
	{
		attackCooldownCountdown = 0f;
		curHitPoints = maxHitPoints;
		target = GameObject.FindWithTag("Player");
	}

    // Update is called once per frame
    new void Update()
    {
		if (!CheckDead())
		{
			EnemyUpdateLoopStart();
			
			// TODO: check if player hasn't moved for X time
			// return to work on this once GameController has been worked on more

			if (operationMode == OperationMode.Chase)
			{
				MoveTowardsPlayer();
			} else if (operationMode == OperationMode.FullCombat)
			{
				AttackPlayer();
			}
		}
		animator.SetBool("isMoving", isMoving);
	}

	protected new void MoveTowardsPlayer()
	{
		Vector2 transl = ((Vector2)(target.transform.position - transform.position)).normalized;
		transform.Translate(new Vector3(transl.x * moveSpeed * Time.unscaledDeltaTime, transl.y * moveSpeed * Time.unscaledDeltaTime, transform.position.z), Space.Self);
		isMoving = true;
	}

	protected new void CooldownLapse()
	{
		if (attackCooldownCountdown > 0f)
		{
			attackCooldownCountdown -= Time.unscaledDeltaTime;
		}
	}

	protected override void AttackPlayer()
	{
		float distanceToPlayer = Vector2.Distance(target.transform.position, transform.position);
		float rand = Random.Range(0f, 1f);
		if (attackCooldownCountdown < Mathf.Epsilon)
		{
			if (distanceToPlayer > attackRange)
			{
				if (rand > 0.3f)
				{
					Instantiate(projectiles[(int)ProjectileType.Spray], transform.position, Quaternion.identity);
					attackCooldownCountdown = attackCooldown;
				}
				else
				{
					Instantiate(projectiles[(int)ProjectileType.Seeker], transform.position, Quaternion.identity);
					attackCooldownCountdown = attackCooldown * 0.5f;
				}
			}
			else if (distanceToPlayer > minComfortDistance)
			{
				if (rand > 0.4f)
				{
					Instantiate(projectiles[(int)ProjectileType.Spray], transform.position, Quaternion.identity);
					attackCooldownCountdown = attackCooldown;
				}
				else
				{
					Instantiate(projectiles[(int)ProjectileType.Seeker], transform.position, Quaternion.identity);
					attackCooldownCountdown = attackCooldown * 0.5f;
				}
			}
			else
			{
				if (rand > 0.5f)
				{
					Instantiate(projectiles[(int)ProjectileType.Spray], transform.position, Quaternion.identity);
					attackCooldownCountdown = attackCooldown;
				}
				else
				{
					Instantiate(projectiles[(int)ProjectileType.Seeker], transform.position, Quaternion.identity);
					attackCooldownCountdown = attackCooldown * 0.5f;
				}
			}
		}
		MoveTowardsPlayer();
	}

	// attack phase dependant on remaining health
	protected void DetermineOperationMode()
	{
		if (curHitPoints < maxHitPoints * 0.9f * fullCombatPhaseHealthThreshold)
		{
			operationMode = OperationMode.FullCombat;
		}
		else operationMode = OperationMode.Chase;
	}

	protected new void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			// TODO: Hurt player
		}
		else if (collision.CompareTag("PlayerSword"))
		{
			// TODO: Hurt Time Creeper
		}
		else if (collision.CompareTag("PlayerBullet"))
		{
			// TODO: Hurt Time Creeper
			curHitPoints--;
			Destroy(collision.gameObject);
		}
	}

	// TODO: On defeat: Add function to instantiate a portal to endgame
}
