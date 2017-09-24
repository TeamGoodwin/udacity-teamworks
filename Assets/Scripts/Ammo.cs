using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo  {

	public Color color;

	public Ammo() {
		color = new Color(Random.value, Random.value, Random.value);
	}
}
