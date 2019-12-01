using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyArmySoldier : Shooter
{
	[SerializeField] SkellyArmyController controller;
    new void Awake()
	{
		base.Awake();
		controller = GameObject.FindGameObjectWithTag("SkellyArmyController").GetComponent<SkellyArmyController>();
    }

	void OnDestroy()
	{
		controller.NotifySkellySoldierDead();
	}
}
