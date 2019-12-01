using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBullet : EnemyProjectile
{
	new void Awake()
	{
		InitProjectile();
		Rotate();
	}

	public new void SetDirection(Vector2 dir)
	{
		base.SetDirection(dir);
		Rotate();
	}

	public void Rotate()
	{
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}
