using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: If a Brute is nearby, Mooks will make way for the Brute
// Maybe use a larger trigger collider to detect if a Brute is within the vicinity

public class Mook : MeleeEnemy
{
    // Start is called before the first frame update
    void Start()
	{
		EnemyInitialize();
	}

    // Update is called once per frame
    void Update()
	{
		EnemyUpdateLoopStart();
		FlipSprite();
		ChargeAtPlayer();
		animator.SetBool("isMoving", isMoving);
		animator.SetBool("isAttacking", isAttacking);
	}
}
