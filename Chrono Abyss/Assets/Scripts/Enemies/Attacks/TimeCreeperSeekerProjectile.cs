using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCreeperSeekerProjectile : SeekerProjectile
{
	protected new void Update()
	{
		if (!CheckDead())
		{
			SetDirection(((Vector2)(target.transform.position - transform.position)).normalized);     // re-compute direction so projectile can follow player

			float travelIncrement = 0f;
			if (distTravelled < maxDistance)
			{
				Vector2 oldPos = (Vector2)transform.position;
				transform.Translate(new Vector3(direction.x * moveSpeed * Time.unscaledDeltaTime, direction.y * moveSpeed * Time.unscaledDeltaTime, transform.position.z), Space.Self);

				travelIncrement = ((Vector2)transform.position - oldPos).magnitude;
				distTravelled += travelIncrement;
			}
		}
	}
}
