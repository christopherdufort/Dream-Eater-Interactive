using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerController player = collision.transform.GetComponent<PlayerController>();
		if (player != null)
		{
			player.setCurrentHealth(player.getCurrentHealth() + 1);
			Destroy(this.gameObject);
		}
	}
}
