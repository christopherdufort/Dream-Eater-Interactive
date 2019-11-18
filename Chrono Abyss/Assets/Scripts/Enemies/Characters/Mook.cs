using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: If a Brute is nearby, Mooks will make way for the Brute
// Maybe use a larger trigger collider to detect if a Brute is within the vicinity

public class Mook : MeleeEnemy
{
	protected void OnTriggerStay(Collider other)
	{
		Brute brute = other.gameObject.GetComponent<Brute>();
		if (brute != null)
		{
			MoveAside();
		}
	}

	protected void MoveAside()
	{

	}
}