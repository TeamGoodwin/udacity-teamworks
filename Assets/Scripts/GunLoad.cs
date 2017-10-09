using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int gunPicked = SceneControl.gun;
		GameObject uzi = GameObject.Find ("Uzi");
		GameObject M200 = GameObject.Find ("M200");
		GameObject SPAS = GameObject.Find ("SPAS");

		switch (gunPicked) {
		case 0:
			Destroy (M200);
			Destroy (SPAS);
			break;
		case 1:
			Destroy (uzi);
			Destroy (SPAS);
			break;
		case 2:
			Destroy (uzi);
			Destroy (M200);
			break;
		}
	}
}
