using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : Shooter
{
	[SerializeField] CurrentAction curAction;
	private enum CurrentAction { Strafing, BackingOff, Charging, CircleFire }
	[SerializeField] AttackMode attackMode;
    private enum AttackMode { Slash, CircleProjectile, SprayProjectile, SeekerProjectile, SinusoidalProjectile }
	[SerializeField] StrafeDirection strafeDir;
	private enum StrafeDirection { Left, Right }
	private enum HealthLevel { High, Medium, Low }

	[SerializeField] float baseMoveSpeed, chargingMoveSpeed;
	[SerializeField] float strafeDurationMin, strafeDurationMax, strafeDurationSet;
	[SerializeField] float chargingDistance, chargingDistanceTravelled;

	Collider2D[] surroundingObjects = new Collider2D[30];
	[SerializeField] float playerBulletDetectionRadius;
	[SerializeField] int attackBulletsThreshold;

	[SerializeField] float swipeRange;
	[SerializeField] ShadowSword sword;
	private float actionTimeRemaining;

	[SerializeField] GameObject bulletPrefab;

	private bool startedCharging;

	// timer for projectile
	// timer for slashing
	// timer for next action

	private new void Awake()
	{
		EnemyInitialize();
		baseMoveSpeed = moveSpeed;
		startedCharging = false;
	}

	private new void Start()
	{
		SetDirection(GetDirectionToPlayer());
		StartStrafing();
		InvokeRepeating("Shoot", 0f, 1f);
	}

	private new void Update()
	{
		SetDirection(GetDirectionToPlayer());

		if (actionTimeRemaining > Mathf.Epsilon)
		{
			actionTimeRemaining -= Time.deltaTime;
		} else
		{
			// determine what to do next
		}
		Strafe();
		Animate();
	}

	void Animate()
	{
		if (direction != Vector2.zero)
		{
			animator.SetFloat("X", direction.x);
			animator.SetFloat("Y", direction.y);
		}
		// Hand off moveSpeed value to animator (for blend tree)
		animator.SetFloat("Speed", moveSpeed);
	}

	private new void Strafe()
	{
		if (curAction == CurrentAction.Strafing)
		{
			if (strafeDurationSet < Mathf.Epsilon)
			{
				SwitchStrafeDirection();
			}

			SetDirectionToStrafe();
			MoveTowardsCurrentDirection();
			strafeDurationSet -= Time.deltaTime;
		}
	}

	private void SwitchStrafeDirection()
	{
		strafeDir = (strafeDir == StrafeDirection.Left) ? StrafeDirection.Right : StrafeDirection.Left;
		strafeDurationSet = Random.Range(strafeDurationMin, strafeDurationMax);
	}

	private void SetDirectionToStrafe()
	{
		direction = (strafeDir == StrafeDirection.Left) ? Vector2.Perpendicular(direction) : -1 * Vector2.Perpendicular(direction);
	}

	private void StartStrafing()
	{
		strafeDurationSet = Random.Range(strafeDurationMin, strafeDurationMax);
		float rand = Random.Range(0f, 1f);
		if (rand > 0.5f)
		{
			strafeDir = StrafeDirection.Left;
		}
		else
		{
			strafeDir = StrafeDirection.Right;
		}
	}


	void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, (Vector2)transform.position + GetDirectionToPlayer() * 1.4f, Quaternion.identity);
	}

	public GameObject GetTarget()
	{
		return target;
	}

	private Vector2 GetDirectionToPlayer()
	{
		return ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
	}

	/*
	private new void Update()
	{
		if (!CheckDead())
		{
			actionTimeRemaining -= Time.deltaTime;
			DetermineNextAction();
			DetermineNextProjectile();
			Strafe();
			AttackPlayer();
			MaintainDistanceFromPlayer((Vector2)target.transform.position - (Vector2)transform.position);
		}
	}

	private void DetermineNextAction()
	{
		if (actionTimeRemaining < Mathf.Epsilon)
		{
			float distToPlayer = Vector2.Distance(target.transform.position, transform.position);
			float rand = Random.Range(0f, 1f);
			if (distToPlayer > attackRange)
			{
				curAction = CurrentAction.Strafing;
				attackMode = AttackMode.SprayProjectile;
			}
			else if (distToPlayer > minComfortDistance)
			{
				curAction = CurrentAction.Strafing;
			}
			else
			{
				// 50-50 chance: back away or charge
				if (rand > 0.5f)
				{
					curAction = CurrentAction.BackingOff;
				}
				else
				{
					curAction = CurrentAction.Charging;
				}
			}
		}
	}

	private void DetermineNextProjectile()
	{

	}

	private void ChargeTowardsPlayer()
	{
		moveSpeed = chargingMoveSpeed;
		chargingDistance = Vector2.Distance(target.transform.position, transform.position) * 1.5f;
	}

	private void DetectPlayerProjectiles()
	{
		int playerBulletCount = 0;
		Physics2D.OverlapCircleNonAlloc(transform.position, playerBulletDetectionRadius, surroundingObjects);
		foreach (Collider2D col in surroundingObjects)
		{
			if (col.CompareTag("PlayerBullet"))
			{
				++playerBulletCount;
			}
		}
		if (playerBulletCount > attackBulletsThreshold)
		{
			curAction = CurrentAction.CircleFire;
		}
	}

	private void AttackPlayerProjectiles()
	{
	}

	new void AttackPlayer()
	{
		if (actionTimeRemaining < Mathf.Epsilon)
		{
			float distance = Vector2.Distance(target.transform.position, transform.position);
			if (distance < swipeRange)
			{
				sword.Swipe();
			}
			else if (distance < minComfortDistance)
			{

			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// if hit a wall and was strafing, strafe in other direction
		// if hit player, attack!
	}
	*/
}
