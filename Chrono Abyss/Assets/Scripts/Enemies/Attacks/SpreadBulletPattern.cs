using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a bullet pattern, can spray from 0 to 360 degrees all around the shooter
public class SpreadBulletPattern : MonoBehaviour
{
	[SerializeField][Tooltip("Should be an odd, positive number")] int numOfBullets;
	[SerializeField][Range(0f, 360f)] float sprayAngle;
	[SerializeField] GameObject projectileObj;
	GameObject firstBulletObj;
	float angleStep;

	protected void Awake()
	{
		angleStep = 0f;
		ValidateBulletPattern();
		GenerateAngleStep();
		GenerateBullets();
	}

	// ensures that amount of bullets is positive and an uneven number
	protected void ValidateBulletPattern()
	{
		if (numOfBullets < 1)
		{
			numOfBullets = 1;
		}
		if (numOfBullets%2 == 0)
		{
			++numOfBullets;
		}
		Mathf.Clamp(sprayAngle, 0f, 360f);
	}

	protected void GenerateAngleStep()
	{
		if (numOfBullets > 1)
		{
			angleStep = sprayAngle / (numOfBullets);
		}
	}

	protected void GenerateBullets()
	{
		// first bullet
		firstBulletObj = Instantiate(projectileObj, transform.position, Quaternion.identity);

		// subsequent bullets
		for (int angleStepAmt = 1; angleStepAmt < numOfBullets * 0.5; ++angleStepAmt)
		{
			GameObject projectileLeftObj = Instantiate(projectileObj, transform.position, Quaternion.identity);
			EnemyProjectile projectileLeft = projectileLeftObj.GetComponent<EnemyProjectile>();
			projectileLeft.SetDirection((GetRotatedVector(firstBulletObj.GetComponent<EnemyProjectile>().direction, angleStepAmt * angleStep)).normalized);

			GameObject projectileRightObj = Instantiate(projectileObj, transform.position, Quaternion.identity);
			EnemyProjectile projectileRight = projectileRightObj.GetComponent<EnemyProjectile>();
			projectileRight.SetDirection((GetRotatedVector(firstBulletObj.GetComponent<EnemyProjectile>().direction, - angleStepAmt * angleStep)).normalized);
		}

		Destroy(gameObject);
	}

	// rotates returns a vector X angle away from the input vector
	protected Vector2 GetRotatedVector(Vector2 vecToRotate, float angle)
	{
		float radians = angle * Mathf.Deg2Rad;
		float sinTheta = Mathf.Sin(radians);
		float cosTheta = Mathf.Cos(radians);

		float x = vecToRotate.x;
		float y = vecToRotate.y;

		return new Vector2(x*cosTheta - y*sinTheta, x*sinTheta + y*cosTheta);
	}
}
