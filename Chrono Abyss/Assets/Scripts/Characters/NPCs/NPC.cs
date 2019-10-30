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
		PlayerSlash slash = collision.transform.GetComponent<PlayerSlash>();
		PlayerBullet bullet = collision.transform.GetComponent<PlayerBullet>();

		if (slash != null)
		{
			this.curHitPoints -= slash.attackValue;
		} else if (bullet != null)
		{
			this.curHitPoints -= bullet.attackValue;
			Destroy(collision.gameObject);
		}
	}

	protected void CheckDead()
	{
		if (curHitPoints < float.Epsilon)
		{
			PlayDeathAnimation();
			EnemySpawnerPlaceholder.enemiesAmt--;
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
