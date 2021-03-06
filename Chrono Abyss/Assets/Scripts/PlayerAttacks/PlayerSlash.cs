﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
	public float attackValue;
	public Animator anim;
	public bool isSlashing;
	// what remains of the slash duration; enemies getting slashed will need this information to know when they can get slashed again
	public float slashDurationRemaining;

	private Vector2 dir;
    public GameObject gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController");
        attackValue += gameController.GetComponent<GameController>().playerData.Dexterity;

        anim = GetComponent<Animator>();
		isSlashing = false;
	}

	private void Update()
	{
		if (slashDurationRemaining > 0f)
		{
			slashDurationRemaining -= Time.deltaTime;
		}
		
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
