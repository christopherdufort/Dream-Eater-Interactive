using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksProjectile : SeekerProjectile
{
	[SerializeField] GameObject explosionObj;

	new void Update()
	{
		if (!CheckDead())
		{
			SetDirection(((Vector2)(target.transform.position - transform.position)).normalized);     // re-compute direction so projectile can follow player
			MoveTowardsCurrentDirection();
		}
	}

	protected new bool CheckDead()
	{
		if ((curHitPoints <= Mathf.Epsilon) || (distTravelled >= maxDistance))
		{
			Instantiate(explosionObj, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
			return true;
		}
		else return false;
	}
}
