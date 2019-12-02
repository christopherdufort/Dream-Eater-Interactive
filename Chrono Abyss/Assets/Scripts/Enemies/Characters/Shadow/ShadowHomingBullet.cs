using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// seeker bullet for the mirror boss
public class ShadowHomingBullet : ShadowBullet
{
    // Update is called once per frame
    new void Update()
	{
		if (!CheckDead())
		{
			SetDirection(((Vector2)(target.transform.position - transform.position)).normalized);     // re-compute direction so projectile can follow player
			MoveTowardsCurrentDirection();
		}
	}
}
