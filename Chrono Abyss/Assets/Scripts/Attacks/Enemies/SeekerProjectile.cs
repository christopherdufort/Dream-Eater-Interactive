using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An enemy projectile that follows the player around until it gets destroyed or exceeds its max distance travelled allowed
public class SeekerProjectile : EnemyProjectile
{
    void Update()
    {
		CheckDestroyed();
		ComputeDirection();

		bool playerFound = target != null;
		if (playerFound)
		{
			MoveTowardsTarget();
		}
    }
}
