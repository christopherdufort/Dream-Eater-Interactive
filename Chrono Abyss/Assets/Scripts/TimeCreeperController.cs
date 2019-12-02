using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for handling the Time Creeper's appearances and setting whether or not it can move at any particular time
// gameObject should be attached to the player character's prefab
public class TimeCreeperController : MonoBehaviour
{
	[SerializeField] float timeCreeperSummonTimer = 30f;	// how many secs until creeper is summoned
	[SerializeField] float timeSinceLastMeaningfulAction = 0f;
	[SerializeField] bool playerInBossRoom = false;

	[SerializeField] GameObject timeCreeperObj;
	TimeCreeper timeCreeper;
	GameController gameController;

    // Start is called before the first frame update
    void Awake()
    {
		//DontDestroyOnLoad(this.gameObject);
    }

	private void Start()
	{
		gameController = FindObjectOfType<GameController>();
		 
	}

	// Update is called once per frame
	void Update()
    {
		if (!playerInBossRoom)
		{
			if (timeSinceLastMeaningfulAction < timeCreeperSummonTimer)
			{
				if (!gameController.paused)
				{
					timeSinceLastMeaningfulAction += Time.unscaledDeltaTime;
				}
			}
			else
			{
				if (timeCreeper == null)
				{
					SpawnTimeCreeper();
				}
				else
				{
					RelocateTimeCreeper();
					LetTimeCreeperMove(true);
				}
			}
		}
    }

	public void NotifyMeaningfulEvent()
	{
		//Debug.Log("Player did something meaningful for once in their life");
		timeSinceLastMeaningfulAction = 0f;
		if (timeCreeper != null)
		{
			timeCreeper.SetCanMove(false);
		}
	}

	public void LetTimeCreeperMove(bool val)
	{
		if (timeCreeper != null)
		{
			timeCreeper.SetCanMove(true);
		}
	}

	// spawns the Time Creeper at a fair distance away from the player
	private void SpawnTimeCreeper()
	{
		timeCreeper = Instantiate(timeCreeperObj, transform.position + GenerateLocationOffset(), Quaternion.identity).GetComponent<TimeCreeper>();
		//Debug.Log("Creeper instantiated at " + timeCreeper.transform.position.ToString() + " after player didn't do squat for " + timeSinceLastMeaningfulAction + " seconds!");
		timeCreeper.SetCanMove(true);
	}

	// in case the Time Creeper is too far away, relocates it near the player
	private void RelocateTimeCreeper()
	{
		if (Vector2.Distance(transform.position, timeCreeper.transform.position) > 15f)
		{
			timeCreeper.transform.position = transform.position + GenerateLocationOffset();
		}
	}

	private Vector3 GenerateLocationOffset()
	{
		Vector3 offset = Vector3.zero;
		int rand = Random.Range(1, 5);
		switch (rand)
		{
			case 1:
				offset.x = -5f;
				offset.y = Random.Range(-5f, 5f);
				break;
			case 2:
				offset.x = 5f;
				offset.y = Random.Range(-5f, 5f);
				break;
			case 3:
				offset.y = -5f;
				offset.x = Random.Range(-5f, 5f);
				break;
			case 4:
				offset.y = 5f;
				offset.x = Random.Range(-5f, 5f);
				break;
		}
		return offset;
	}

	public void NotifyPlayerInBossRoom(bool val)
	{
		if (val == true)
		{
			timeSinceLastMeaningfulAction = 0f;
		}
		playerInBossRoom = val;
	}
}
