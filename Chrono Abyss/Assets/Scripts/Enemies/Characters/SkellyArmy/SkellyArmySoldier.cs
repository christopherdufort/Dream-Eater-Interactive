using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyArmySoldier : Mook
{
	[SerializeField] SkellyArmyController controller;
    // Start is called before the first frame update
    void Awake()
    {
		controller = GameObject.Find("SkellyArmyController").GetComponent<SkellyArmyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnDeath()
	{
		controller.NotifySkellySoldierDead();
	}
}
