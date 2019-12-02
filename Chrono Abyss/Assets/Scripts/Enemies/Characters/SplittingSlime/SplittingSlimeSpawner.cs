using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingSlimeSpawner : MonoBehaviour
{
	[SerializeField] GameObject[] splittingSlimePrefabs;

    public void Spawn(int prevRoundNumber, Vector2 position)
	{
		Instantiate(splittingSlimePrefabs[prevRoundNumber], position, Quaternion.identity).GetComponent<SplittingSlime>();
		Instantiate(splittingSlimePrefabs[prevRoundNumber], position, Quaternion.identity).GetComponent<SplittingSlime>();
	}
}
