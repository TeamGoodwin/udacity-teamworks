using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUpdate : MonoBehaviour {
	public BallShooter ballShooter;

	private Renderer objRend;

	void Start()
	{
		ballShooter.weaponChanged += AmmoUpdated;
		objRend = GetComponent<Renderer>();
	}

	public void AmmoUpdated(Ammo ammo)
	{
		if (objRend != null) {
			objRend.material.color = ammo.color;
		}
	}
}
