using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Shadow Warrior's gun; will always be pointed at the player character
public class ShadowGunRotate : MonoBehaviour
{
	[SerializeField] Transform parent;
	[SerializeField] GameObject target;

	private void Start()
	{
		target = parent.gameObject.GetComponent<Shadow>().GetTarget();
	}

	// Update is called once per frame
	void Update()
	{
		FaceTarget();
	}

	private void FaceTarget()
	{
		Vector2 normalizedAimDir = ((Vector2)(target.transform.position - parent.position)).normalized;
		transform.position = normalizedAimDir + (Vector2)parent.position;

		float angle = Mathf.Atan2(normalizedAimDir.y, normalizedAimDir.x) * Mathf.Rad2Deg;

		// rotate sprite to face cursor
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

	}
}