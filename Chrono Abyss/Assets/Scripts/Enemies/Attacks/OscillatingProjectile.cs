using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatingProjectile : EnemyProjectile
{
	[SerializeField] float amplitude, frequency;
	float sinTimerCur = 0f;

	protected new void Awake()
	{
		InitProjectile();
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

    // Update is called once per frame
    protected new void Update()
	{
		if (!CheckDead())
		{
			distTravelled += MoveTowardsCurrentDirection();
		}
	}

	// returns distance travelled
	protected new float MoveTowardsCurrentDirection()
	{
		if (sinTimerCur > 2 * Mathf.PI)
		{
			sinTimerCur = 0f;
		}

		float oscillation = Mathf.Cos(frequency * sinTimerCur) * moveSpeed * Time.deltaTime;
		sinTimerCur += Time.deltaTime;

		Vector2 origPosition = transform.position;
		transform.Translate(new Vector3(moveSpeed * Time.deltaTime, amplitude * oscillation, 0f));
		return ((Vector2)transform.position - origPosition).normalized;
	}
}
