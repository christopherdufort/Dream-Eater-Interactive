using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brute : MeleeEnemy
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
		AttemptAttackPlayer();
		animator.SetBool("isMoving", isMoving);
		animator.SetBool("isAttacking", isAttacking);
	}
}
