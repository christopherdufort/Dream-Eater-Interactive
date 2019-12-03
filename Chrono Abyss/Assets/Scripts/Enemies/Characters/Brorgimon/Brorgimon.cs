using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brorgimon : EnemyCharacer
{
	[SerializeField] Transform leftHand, rightHand;
	[SerializeField] GameObject[] projectiles;
	[SerializeField] float nearDistance;
	private enum ProjectileMode { Fireworks, Spray }
	private ProjectileMode projectileMode;
	[SerializeField] float fireRate, cooldown;

	private new void Awake()
	{
		curHitPoints = maxHitPoints;
		cooldown = 0f;
	}

	// Start is called before the first frame update
	void Start()
    {
		EnemyInitialize();
		projectileMode = ProjectileMode.Fireworks;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckDead())
		{
			if (cooldown >= 0f)
			{
				cooldown -= Time.deltaTime;
			}
			DetermineProjectile();
			AttackPlayer();
		}
    }

	private void DetermineProjectile()
	{
		float distance = Vector2.Distance(target.transform.position, transform.position);
		if (distance > nearDistance)
		{
			projectileMode = ProjectileMode.Fireworks;
		} else
		{
			projectileMode = ProjectileMode.Spray;
		}
	}

	protected override void AttackPlayer()
	{
		if (cooldown <= Mathf.Epsilon)
		{
			Instantiate(projectiles[(int)projectileMode], leftHand.position, Quaternion.identity);
			Instantiate(projectiles[(int)projectileMode], rightHand.position, Quaternion.identity);
			cooldown = fireRate;
		}
	}

	private void OnDestroy()
	{
		
	}
}
