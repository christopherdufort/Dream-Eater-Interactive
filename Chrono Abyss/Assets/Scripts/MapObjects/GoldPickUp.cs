using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour
{
    GameObject gameController;
    void Start()
    {
        gameController = GameObject.Find("GameController");
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.CompareTag("Player") && gameController != null)
        { 
            ++gameController.GetComponent<GameController>().goldCollected;
            Debug.Log("Total gold collected " + gameController.GetComponent<GameController>().goldCollected);
            Destroy(this.gameObject);
        }
	}
}
