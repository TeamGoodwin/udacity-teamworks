using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour {

	public GameObject ballPrefab;
	public float shootSpeed = 20;
	public float shootOffset = 1.0f;
	public bool singleShot = true;
	public bool continuous = false;

	private GameObject ball;

	// Use this for initialization
	void Start () {
		
	}

	private bool isFiring(){
		bool fireButton = false;
		if (continuous) {
			if (Input.GetButton ("Fire1")) {
				fireButton = true;
			}
		} else { 
			if (Input.GetButtonDown ("Fire1")) {
				fireButton = true;
			}
		}

		return fireButton && (ball == null || !singleShot);
	}

	// Update is called once per frame
	void Update () {
		if(isFiring())
		{
			ball = Instantiate (ballPrefab);
			Camera camera = GetComponentInChildren<Camera> ();
			Vector3 offset = new Vector3(0.0f, shootOffset, 0.0f);
			ball.transform.position = camera.transform.position - offset;
			Rigidbody ballRb = ball.GetComponentInChildren<Rigidbody> ();
			ballRb.velocity = camera.transform.rotation * Vector3.forward * shootSpeed;
		}
	}


}
