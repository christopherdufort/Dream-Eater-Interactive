using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An enemy projectile that follows the player around until it gets destroyed or exceeds its max distance travelled allowed
public class SeekerProjectile : EnemyProjectile
{
    void Update()
    {
		CheckDestroyed();
		ComputeDirection();		// re-compute direction so projectile can follow player
		
		if (target != null)
		{
			MoveTowardsTarget();
		}
    }
}
