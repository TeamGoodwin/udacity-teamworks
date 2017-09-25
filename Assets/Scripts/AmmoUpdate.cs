using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUpdate : MonoBehaviour {
	public BallShooter ballShooter;

	void Start()
	{
		ballShooter.weaponChanged += AmmoUpdated;
	}

	public void AmmoUpdated(Ammo ammo)
	{
		Renderer objRend = GetComponent<Renderer>();
		if (objRend != null) {
			objRend.material.color = ammo.color;
		}
	}
}
