using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCreeperBasicProjectile : EnemyProjectile
{
	public GameController gameController;

	protected new void Update()
	{
		if (!CheckDead())
		{
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
