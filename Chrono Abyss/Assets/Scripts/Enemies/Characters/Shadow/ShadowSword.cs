using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// sword for Mirror Boss... it'll probably just be there for looks
public class ShadowSword : MonoBehaviour
{
	[SerializeField] public float attackValue { get; }
	[SerializeField] BoxCollider2D swordCollider;
	[SerializeField] Animator anim;
	[SerializeField] float slashDuration;
	[SerializeField] Shadow shadowWarrior;
	private void Update()
	{
		// change pos to dir of parent
		transform.localPosition = new Vector3(shadowWarrior.direction.x * 0.8f, shadowWarrior.direction.y * 0.8f, 1);

		// flip sword if left or down
		float scaleX = shadowWarrior.direction.x > 0.5f ? -1.0f : 1.0f;
		transform.localScale = new Vector3(scaleX * 0.7f, 0.7f, 1);
	}

	// unused function, probably will stay that way
	public void Swipe()
	{
		StartCoroutine("SlashCoroutine");
	}

	IEnumerable SlashCoroutine()
	{
		anim.SetTrigger("Slash");
		swordCollider.enabled = true;
		yield return new WaitForSeconds(slashDuration);
		swordCollider.enabled = false;
	}
}
