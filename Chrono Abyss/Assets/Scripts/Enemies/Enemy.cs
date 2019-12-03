using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public abstract class Enemy : MonoBehaviour
{
	[Space]
	[Header("General Stats")]
	[SerializeField] protected int level;
	[SerializeField] protected float maxHitPoints;
	[SerializeField] protected float curHitPoints;
	[SerializeField] protected float attackValue;
	[SerializeField] protected bool canGetSlashed = true;

	[Space]
	[Header("Movement")]
	[SerializeField] protected float moveSpeed;
	[SerializeField] public Vector2 direction;

	[Space]
	[Header("References")]
	[SerializeField] protected Rigidbody2D rigidBody;
	[SerializeField] protected Animator animator;
	[SerializeField] protected GameObject deathAnimation;
	[SerializeField] protected GameObject target;

	[Space]
	[Header("Stats Scale Factor")]
	[SerializeField] protected float hitPointUpMin;
	[SerializeField] protected float hitPointUpMax;
	[SerializeField] protected float attackUpMin;
	[SerializeField] protected float attackUpMax;
	[SerializeField] protected float moveSpeedUpMin;
	[SerializeField] protected float moveSpeedUpMax;

	// for animator
	protected bool isDead;
	protected bool isMoving;

    // Update is called once per frame
    void Update()
	{
		CheckDead();
	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerBullet bullet = collision.transform.GetComponent<PlayerBullet>();

		if (bullet != null)
		{
			this.curHitPoints -= bullet.attackValue;
			Destroy(collision.gameObject);
		}
	}

	protected abstract bool CheckDead();

	// basic moves towards set direction, returns distance travelled
	protected float MoveTowardsCurrentDirection()
	{
		Vector2 oldPos = (Vector2)transform.position;

		float xDisp = transform.position.x + direction.x * moveSpeed * Time.deltaTime;
		float yDisp = transform.position.y + direction.y * moveSpeed * Time.deltaTime;

		transform.position = new Vector3(xDisp, yDisp, transform.position.z);

		// return distance travelled
		return ((Vector2)transform.position - oldPos).magnitude;
	}

	protected void StopMoving()
	{
		rigidBody.velocity = Vector3.zero;
		isMoving = false;
	}
	public void SetDirection(Vector2 dir)
	{
		direction = dir;
	}

	protected void PlayDeathAnimation()
	{
		Instantiate(deathAnimation, transform.position, Quaternion.identity);
	}

	// scales enemy stats, dependent on floor level
	protected void ScaleLevel()
	{
        for (int i = 1; i < level; i++)
		{
			maxHitPoints += Random.Range(hitPointUpMin, hitPointUpMax);
			attackValue += Random.Range(attackUpMin, attackUpMax);
			moveSpeed += Random.Range(moveSpeedUpMin, moveSpeedUpMax);
		}
	}

	public float GetMaxHealth()
	{
		return maxHitPoints;
	}

	public float GetCurrentHealth()
	{
		return curHitPoints;
	}

	protected void OnTriggerStay2D(Collider2D collision)
	{
		PlayerSlash slash = collision.transform.GetComponent<PlayerSlash>();
		if (slash != null)
		{
			if ((slash.isSlashing) && canGetSlashed)
			{
				curHitPoints -= slash.attackValue;
				canGetSlashed = false;
				StartCoroutine("NextSlashDamageDelay", slash);
			}
		}
	}

	protected IEnumerator NextSlashDamageDelay(PlayerSlash slash)
	{
		yield return new WaitForSeconds(slash.slashDurationRemaining);
		canGetSlashed = true;
	}
}