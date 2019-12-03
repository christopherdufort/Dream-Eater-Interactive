using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerController player = collision.transform.GetComponent<PlayerController>();
		if (player != null)
		{
			++player.goldCollected;
			Destroy(this.gameObject);
		}
	}
}
