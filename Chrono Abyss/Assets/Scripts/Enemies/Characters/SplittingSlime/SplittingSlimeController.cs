using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// holds the real health of the splitting slime boss as a collective
public class SplittingSlimeController : MonoBehaviour
{
	[SerializeField] List<SplittingSlime> slimeList;
	[SerializeField] float maxHitPoints, curHitPoints;

	private void Awake()
	{
		curHitPoints = maxHitPoints;
	}

	private void Update()
	{
		CheckDeath();
	}

	private void CheckDeath()
	{
		// once the collective loses all its health, all slimes should die
		if (curHitPoints < Mathf.Epsilon)
		{
			KillAll();
			// TODO: drop permanent upgrade
			// TODO: instantiate a portal to next world
			// TODO: Notify Game Controller when dead
		}
	}

	// function for the split slimes to call when they die, so the appropriate amt of health can be taken off the collective
	public void NotifyHealthLost(float healthLost)
	{
		if (curHitPoints > 0f)
		{
			curHitPoints -= healthLost;
			curHitPoints = Mathf.Max(curHitPoints, 0f);
		}
	}

	// for when we'll have to kill all slimes
	public void AddSlime(SplittingSlime slime)
	{
		slimeList.Add(slime);
	}

	public void KillAll()
	{
		for (int i = 0; i < slimeList.Count; i++)
		{
			if (slimeList[i] != null)
			{
				slimeList[i].Die();
			}
		}
	}
}
