using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallShooter : MonoBehaviour {

	// Defaults
	private const float defaultRpm = 600;


	public GameObject ballPrefab;
	public float shootSpeed = 20;
	public bool singleShot = true;
	public bool continuous = false;
	public GameObject m_spawnPoint;
	public float repeatRateRpm = defaultRpm;


	private GameObject ball;
	private float fireCountdown = 0.0f;
	private float shotTime;
	// Use this for initialization
	void Start () {
		ReInitVars ();
	}

	private bool isFiring(){
		bool fireButton = false;

		// Are we continueous firing?
		if (continuous) {

			// Check the repeat rate
			if (Input.GetButton ("Fire1")){
				print (fireCountdown);
				// Subtract time whilst we are holding fire
				fireCountdown -= Time.deltaTime;
				if (fireCountdown <= 0.0f) {
					// Fire!
					fireButton = true;

					// Restart the countdown if we just fired
					fireCountdown += shotTime;
				}
			}
		} else { 
			if (Input.GetButtonDown ("Fire1")) {
				// Fire!
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
			ball.transform.position = m_spawnPoint.transform.position;
			Rigidbody ballRb = ball.GetComponentInChildren<Rigidbody> ();
			ballRb.velocity = m_spawnPoint.transform.rotation * Vector3.forward * shootSpeed;
		}
	}

	void OnValidate()
	{
		if (repeatRateRpm == 0.0f)
			repeatRateRpm = defaultRpm;
		if (repeatRateRpm < 0.0f)
			repeatRateRpm = Mathf.Abs(repeatRateRpm);
		ReInitVars ();
	}

	private void ReInitVars()
	{
		shotTime = 60.0f / repeatRateRpm;
	}

}
