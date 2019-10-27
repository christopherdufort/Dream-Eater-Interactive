using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	[Space]
	[Header("General Stats")]
	[SerializeField] protected int level;
	[SerializeField] protected float maxHitPoints;
	[SerializeField] protected float curHitPoints;

	[Space]
	[Header("Movement")]
	[SerializeField] protected float moveSpeed;

	[Space]
	[Header("References")]
	[SerializeField] protected Rigidbody2D rigidBody;
	[SerializeField] protected Animator animator;
	[SerializeField] protected GameObject deathAnimation;

	// for animator
	protected bool isDead;
	protected bool isMoving;

    void Start()
	{
		rigidBody.freezeRotation = true;
	}

    // Update is called once per frame
    void Update()
	{
		CheckDead();
	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		float slashDamage = 5f, bulletDamage = 2f;      // placeholder
		bool isSlash = collision.transform.GetComponent<PlayerSlash>() != null;
		bool isBullet = collision.transform.GetComponent<PlayerBullet>() != null;

		if (isSlash)
		{
			print("You slashed an enemy!");
			this.curHitPoints -= slashDamage;
		} else if (isBullet)
		{
			print("You shot an enemy!");
			this.curHitPoints -= bulletDamage;
			Destroy(collision.gameObject);		//temp
		}
	}

	protected void CheckDead()
	{
		if (curHitPoints < float.Epsilon)
		{
			PlayDeathAnimation();
			EnemySpawner.EnemiesAmt--;
			Destroy(this.gameObject);
		}
	}

	protected void StopMoving()
	{
		rigidBody.velocity = Vector3.zero;
		isMoving = false;
	}

	protected void PlayDeathAnimation()
	{
		Instantiate(deathAnimation, transform.position, Quaternion.identity);
	}
}
