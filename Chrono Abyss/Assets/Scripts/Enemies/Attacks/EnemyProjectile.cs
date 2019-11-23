using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enemy projectile that goes straight to a static target and keeps traveling until it exceeds its max allowed travel range
public class EnemyProjectile : Enemy
{
	[SerializeField] protected float maxDistance;
	[SerializeField] protected float distTravelled;
	[SerializeField] protected bool isIndestructible;

	// Start is called before the first frame update
	void Start()
	{
		InitProjectile();
	}

	// Update is called once per frame
	void Update()
	{
		if (!CheckDead())
		{
			MoveTowardsCurrentDirection();
		}
	}
	
	private new void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerSlash slash = collision.transform.GetComponent<PlayerSlash>();
		if (slash != null)
		{
			if (!isIndestructible)
			{
				this.curHitPoints -= slash.attackValue;
			}
		} else
		{
			PlayerBullet bullet = collision.transform.GetComponent<PlayerBullet>();
			if (bullet != null)
			{
				if (!isIndestructible)
				{
					this.curHitPoints -= bullet.attackValue;
				}
			} else
			{
				PlayerController player = collision.transform.GetComponent<PlayerController>();
				if (player != null)
				{
					// TODO: Damage player
					Destroy(this.gameObject);
				} else
				{
					// TODO: What if it hits a wall?
					// Debug.Log("Enemy projectile hasn't collided with any object of note.");
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
		SetDirection(target.transform.position);
	}
}
