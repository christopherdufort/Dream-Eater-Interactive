using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
	[Space]
	[Header ("Projectiles")]
	[SerializeField] protected EnemyProjectile[] projectiles;

	[Space]
	[SerializeField] protected float minComfortDistance;

	bool isBackingUp;

	// Start is called before the first frame update
	void Start()
	{
		EnemyInitialize();
		isBackingUp = false;
	}

    // Update is called once per frame
    void Update()
	{
		EnemyUpdateLoopStart();

		if (isBackingUp)
		{
			FlipSprite();
		} else
		{
			FlipSpriteWhileBackingUp();
		}

		Vector2 movementDirVec = (Vector2)transform.position - playerPos;
		float playerToEnemyDist = movementDirVec.magnitude;
		if (playerToEnemyDist < minComfortDistance)
		{
			MaintainDistanceFromPlayer(movementDirVec);
			isBackingUp = true;

		} else
		{
			AttemptAttackPlayer();
			isBackingUp = false;
		}

		animator.SetBool("isMoving", isMoving);
		animator.SetBool("isAttacking", isAttacking);
	}
	
	protected void MaintainDistanceFromPlayer(Vector2 movementDirVec)
	{
		movementDirVecNorm = movementDirVec.normalized;

		// vectors values for animator inverted so enemy faces player while moving away
		animator.SetFloat("moveX", -movementDirVecNorm.x);
		animator.SetFloat("moveY", -movementDirVecNorm.y);

		float xMove = transform.position.x + movementDirVec.x * moveSpeed * Time.deltaTime;
		float yMove = transform.position.y + movementDirVec.y * moveSpeed * Time.deltaTime;
		rigidBody.MovePosition(new Vector2(xMove, yMove));

		AttackPlayer();
		isMoving = false;
	}

	protected override void AttackPlayer()
	{
		bool cooldownActive = attackCooldownTimer > 0f;
		if (!cooldownActive)
		{
			Instantiate(projectiles[0], transform.position, Quaternion.identity);
			attackCooldownTimer = attackCooldown;
		}
		isAttacking = true;
	}

	protected void Strafe()
	{
		// TODO
	}

	// TODO: This was made to go with the placeholder enemy sprites which have "west" as a default direction.
	protected void FlipSpriteWhileBackingUp()
	{
		if (movementDirVecNorm.x < float.Epsilon)
		{
			Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
		else
		{
			Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
	}
}
