using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An enemy projectile that follows the player around until it gets destroyed or exceeds its max distance travelled allowed
public class SeekerProjectile : EnemyProjectile
{
	// Start is called before the first frame update
	void Start()
	{
		InitProjectile();
	}

	void Update()
    {
		if (!CheckDead())
		{
			SetDirection(target.transform.position);     // re-compute direction so projectile can follow player
			MoveTowardsCurrentDirection();
		}
    }
}
