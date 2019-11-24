using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
	public float attackValue;
	public Animator anim;

	private Vector2 dir;
	
	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		// change pos to dir of player
		transform.localPosition = new Vector3(dir.x * 0.8f, dir.y * 0.8f, 1);

		// flip sword if left or down
		float scaleX = dir.x > 0.5f ? -1.0f : 1.0f;
		transform.localScale = new Vector3(scaleX * 0.7f, 0.7f, 1);
	}

	public void setDir(Vector2 newDir)
	{
		dir = newDir;
	}
}
