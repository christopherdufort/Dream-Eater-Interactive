using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksProjectile : SeekerProjectile
{
	[SerializeField] GameObject explosionObj;

	private void OnDestroy()
	{
		Instantiate(explosionObj, transform.position, Quaternion.identity);
	}
}
