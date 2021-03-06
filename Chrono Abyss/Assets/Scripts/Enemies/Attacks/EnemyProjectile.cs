﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enemy projectile that goes straight to a static target and keeps traveling until it exceeds its max allowed travel range
public class EnemyProjectile : Enemy
{
	[Header("Projectile Stats")]
	[SerializeField] protected float maxDistance;
	[SerializeField] protected float distTravelled;
	[SerializeField] protected bool isIndestructible;

	// Start is called before the first frame update
	protected void Awake()
	{
		InitProjectile();
	}

	// Update is called once per frame
	protected void Update()
	{
		if (!CheckDead())
		{
			MoveTowardsCurrentDirection();
		}
	}
	
	protected new void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerBullet bullet = collision.transform.GetComponent<PlayerBullet>();
		if (bullet != null)
		{
			if (!isIndestructible)
			{
				this.curHitPoints -= bullet.attackValue;
				Destroy(collision.gameObject);
			}
		} else
		{
			PlayerSlash sword = collision.transform.GetComponent<PlayerSlash>();
			if (sword != null)
			{
				if (!isIndestructible)// && (sword.isSlashing))
				{
					//Debug.Log("PROJECTILE DESTROYED BY SWORD!");
					Destroy(this.gameObject);
				}
			} else
			{
				PlayerController player = collision.transform.GetComponent<PlayerController>();
				if (player != null)
				{
					// TODO: Damage player?
					Destroy(this.gameObject);
				}
				else
				{
					if (collision.transform.tag == "Wall")
					{
						//Debug.Log("Enemy projectile came in contact with wall");
						Destroy(this.gameObject);
					} else if (collision.CompareTag("Door"))
					{
						Destroy(this.gameObject);
					}
					else
					{
						// Debug.Log("Enemy projectile hasn't collided with any object of note.");
					}
				}
			}
		}
	}

	protected new float MoveTowardsCurrentDirection()
	{
		float travelIncrement = 0f;
		if (distTravelled < maxDistance)
		{
			travelIncrement = base.MoveTowardsCurrentDirection(); ;
			distTravelled += travelIncrement;
		}
		return travelIncrement;
	}

	protected override bool CheckDead()
	{
		if ((curHitPoints <= Mathf.Epsilon) || (distTravelled >= maxDistance))
		{
			Destroy(this.gameObject);
			return true;
		}
		else return false;
	}

	protected void InitProjectile()
	{
		ScaleLevel();
		distTravelled = 0f;
		target = GameObject.FindWithTag("Player");
		Vector2 dir = ((Vector2)(target.transform.position - transform.position)).normalized;
		SetDirection(dir);
	}

	void SetMaxDistance(float val)
	{
		maxDistance = val;
	}

	void SetMoveSpeed(float val)
	{
		moveSpeed = val;
	}
}
