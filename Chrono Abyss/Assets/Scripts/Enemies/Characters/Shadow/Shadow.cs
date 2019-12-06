using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : Shooter
{
	private enum ProjectileMode { RegularProjectile, SprayProjectile, SeekerProjectile }
	private enum StrafeSidewaysDirection { Left, Right }
	private enum StrafeFrontwaysDirection { Front, Back, None }

	[Header("States")]
	[SerializeField] private ProjectileMode projectileMode;
	[SerializeField] private StrafeSidewaysDirection sidewaysStrafeDir;
	[SerializeField] private StrafeFrontwaysDirection frontwaysStrafeDir;

	[Header("Mirror Boss-Unique Movement")]
	[SerializeField] float strafeDurationMin;
	[SerializeField] float strafeDurationMax;
	[SerializeField] float strafeDurationSet;

	[SerializeField] float regularProjectileCooldown = 0.75f, sprayProjectileCooldown = 2f, seekerProjectileCooldown = 1.5f;

	[SerializeField] ShadowSword sword;
	private float strafeTimer;
	private bool startedCharging = false;
	private bool canShoot = true;

	private new void Awake()
	{
		EnemyInitialize();
	}

	private new void Start()
	{
		SetDirection(GetDirectionToPlayer());
		StartStrafing();
	}

	private new void Update()
	{
		NotifyIfBossDead();
		if (!CheckDead())
		{
			SetDirection(GetDirectionToPlayer());
			// decisions
			DetermineNextProjectile();
			UpdateFrontwaysStrafeDirection();
			// take aciton
			Strafe();
			Attack();
			Animate();
		}
	}

	void NotifyIfBossDead()
	{
		if (curHitPoints <= Mathf.Epsilon)
		{
			FindObjectOfType<BossController>().BossDied();
            Destroy(gameObject);
        }
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
		if (strafeDurationSet < Mathf.Epsilon)
		{
			SwitchStrafeDirection();
		}

		SetDirectionToStrafe();
		MoveTowardsCurrentDirection();
		strafeDurationSet -= Time.deltaTime;
	}

	// initializes strafing movement
	private void StartStrafing()
	{
		strafeDurationSet = Random.Range(strafeDurationMin, strafeDurationMax);
		float rand = Random.Range(0f, 1f);
		if (rand > 0.5f)
		{
			sidewaysStrafeDir = StrafeSidewaysDirection.Left;
		}
		else
		{
			sidewaysStrafeDir = StrafeSidewaysDirection.Right;
		}
	}

	// change from strafing left or right
	private void SwitchStrafeDirection()
	{
		sidewaysStrafeDir = (sidewaysStrafeDir == StrafeSidewaysDirection.Left) ? StrafeSidewaysDirection.Right : StrafeSidewaysDirection.Left;
		strafeDurationSet = Random.Range(strafeDurationMin, strafeDurationMax);
	}

	private void SetDirectionToStrafe()
	{
		Vector2 forwardMovement = ((Vector2)(target.transform.position - transform.position)).normalized;
		Vector2 sideMovement = Vector2.Perpendicular(direction);

		switch (frontwaysStrafeDir)
		{
			// only strafing sideways
			case StrafeFrontwaysDirection.None:
				direction = (sidewaysStrafeDir == StrafeSidewaysDirection.Left) ? Vector2.Perpendicular(direction) : -1 * Vector2.Perpendicular(direction);
				break;
			// strafing while approaching player (45 degrees, towards player)
			case StrafeFrontwaysDirection.Front:
				direction = (sidewaysStrafeDir == StrafeSidewaysDirection.Left) ?
					((forwardMovement + Vector2.Perpendicular(direction)) * 0.5f).normalized
					: ((forwardMovement + -1 * Vector2.Perpendicular(direction)) * 0.5f).normalized;
				break;
			// strafing while backing away (45 degrees, away from direction to player)
			case StrafeFrontwaysDirection.Back:
				direction = (sidewaysStrafeDir == StrafeSidewaysDirection.Left) ?
					((-forwardMovement + Vector2.Perpendicular(direction)) * 0.5f).normalized
					: ((-forwardMovement + -1 * Vector2.Perpendicular(direction)) * 0.5f).normalized;
				break;
			default:
				Debug.Log("mirror dude strafing... no this shouldn't be happening");
				break;
		}
	}

	// set forward-axis (from enemy to player) direction
	private void UpdateFrontwaysStrafeDirection()
	{
		float dist = Vector2.Distance(transform.position, target.transform.position);
		// approach player while strafing if too far away
		if (dist > attackRange)
		{
			frontwaysStrafeDir = StrafeFrontwaysDirection.Front;
		}
		// back away if too close
		else if (dist < minComfortDistance)
		{
			frontwaysStrafeDir = StrafeFrontwaysDirection.Back;
		}
		// stay in same lane
		else
		{
			frontwaysStrafeDir = StrafeFrontwaysDirection.None;
		}
	}

	private void Attack()
	{
		StartCoroutine("ShootPlayer");
	}

	IEnumerator ShootPlayer()
	{
		if (canShoot)
		{
			GameObject bullet = Instantiate(projectiles[(int)projectileMode], (Vector2)transform.position + GetDirectionToPlayer() * 1.4f, Quaternion.identity);

			canShoot = false;

			switch (projectileMode)
			{
				case ProjectileMode.RegularProjectile:
					yield return new WaitForSeconds(regularProjectileCooldown);
					break;
				case ProjectileMode.SeekerProjectile:
					yield return new WaitForSeconds(seekerProjectileCooldown);
					break;
				case ProjectileMode.SprayProjectile:
					yield return new WaitForSeconds(sprayProjectileCooldown);
					break;
				default:
					Debug.Log("No projectile mode? What sorcery is this?");
					break;
			}

			canShoot = true;
		}
	}

	// for gun & sword-related classes' use
	public GameObject GetTarget()
	{
		return target;
	}

	private Vector2 GetDirectionToPlayer()
	{
		return ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
	}

	// decides which projectile to use next based on randomness & distance from player
	private void DetermineNextProjectile()
	{
		float dist = Vector2.Distance(transform.position, target.transform.position);
		float rand = Random.Range(0f, 1f);
		if (dist > attackRange)
		{
			if (rand > 0.8f)
			{
				projectileMode = ProjectileMode.SeekerProjectile;
			} else if (rand > 0.5f)
			{
				projectileMode = ProjectileMode.SprayProjectile;
			}
			else
			{
				projectileMode = ProjectileMode.RegularProjectile;
			}
		}
		else if (dist > minComfortDistance)
		{
			if (rand > 0.4f)
			{
				projectileMode = ProjectileMode.SprayProjectile;
			}
			else
			{
				projectileMode = ProjectileMode.RegularProjectile;
			}
		}
		else
		{
			projectileMode = ProjectileMode.RegularProjectile;
		}
	}
}