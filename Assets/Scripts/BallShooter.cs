using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour {

	public GameObject ballPrefab;
	public float shootSpeed = 20;
	public float shootOffset = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Fire1"))
		{
			GameObject ball = Instantiate (ballPrefab);
			Camera camera = GetComponentInChildren<Camera> ();
			Vector3 offset = new Vector3(0.0f, shootOffset, 0.0f);
			ball.transform.position = camera.transform.position - offset;
			Rigidbody ballRb = ball.GetComponentInChildren<Rigidbody> ();
			ballRb.velocity = camera.transform.rotation * Vector3.forward * shootSpeed;
		}
	}
}
