using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enemy projectile that goes straight to a static target and keeps traveling until it exceeds its max allowed travel range
public class EnemyProjectile : MonoBehaviour
{
	[SerializeField] protected float maxHealth;
	[SerializeField] protected float curHealth;
	[SerializeField] protected float damageValue;
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float maxDistance;
	[SerializeField] protected float distTravelled;
	[SerializeField] protected bool isIndestructible;
	[SerializeField] protected Vector2 direction;
	[SerializeField] protected GameObject target;

    // Start is called before the first frame update
    void Start()
    {
		InitProjectile();
    }

    // Update is called once per frame
    void Update()
    {
		CheckDestroyed();
		MoveTowardsTarget();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!isIndestructible)
		{
			PlayerSlash slash = collision.transform.GetComponent<PlayerSlash>();
			PlayerBullet bullet = collision.transform.GetComponent<PlayerBullet>();
			PlayerController player = collision.transform.GetComponent<PlayerController>();

			if (slash != null)
			{
				this.curHealth -= slash.attackValue;
			} else if (bullet != null)
			{
				this.curHealth -= bullet.attackValue;
				Destroy(collision.gameObject);      //temp
			} else if (player != null)
			{
				// TODO: Damage player
				Destroy(this.gameObject);
			}
		}
	}

	protected void CheckDestroyed()
	{
		if ((curHealth <= Mathf.Epsilon) || (distTravelled >= maxDistance))
		{
			Destroy(this.gameObject);
		}
	}

	// basic projectile movement: moves towards a specific set point in world
	protected void MoveTowardsTarget()
	{
		if (distTravelled < maxDistance)
		{
			Vector2 oldPos = (Vector2)transform.position;

			float xDisp = transform.position.x + direction.x * moveSpeed * Time.deltaTime;
			float yDisp = transform.position.y + direction.y * moveSpeed * Time.deltaTime;

			transform.position = new Vector3(xDisp, yDisp, transform.position.z);

			// update distance travelled
			distTravelled += ((Vector2)transform.position - oldPos).magnitude;
		}
	}

	protected void InitProjectile()
	{
		distTravelled = 0f;
		ComputeDirection();
	}

	// Find direction from current point to the target (the player)
	protected void ComputeDirection()
	{
		if (target == null)
		{
			target = GameObject.FindWithTag("Player");
		}

		Vector2 targetPos = (Vector2)target.transform.position;
		float xDist = targetPos.x - transform.position.x;
		float yDist = targetPos.y - transform.position.y;
		direction = new Vector2(xDist, yDist).normalized;
	}
}
