using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallShooter : MonoBehaviour {

	// Defaults
	private const float defaultRpm = 600;

	// Public properoties for editor
	public GameObject ballPrefab;				// What object for the ammo?
	public float shootSpeed = 20;				// How fast does the ammo fire
	public bool singleShot = true;				// Are we restricted to one shot at a time
	public bool continuous = false;				// Can we fire continuously
	public GameObject m_spawnPoint;				// Where should the amo spawn?
	public float repeatRateRpm = defaultRpm;	// Weapon fire rate in rounds per minute
	public GameObject weapon;					// The weapon used so we can update it


	// Private state variables
	private GameObject ball;					// Track the current ammo
	private float fireCountdown = 0.0f;			// How long until we can fire next?
	private float shotTime;						// How long between shots (based on fire rate public variable)
	private Ammo ammoProps;						// Current ammuntion properties

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

		// If we're good to fire and there is no ammo limitatation
		return fireButton && (ball == null || !singleShot);
	}

	// Update is called once per frame
	void Update () {
		// Every time the player stops firing, update the ammo type
		if (Input.GetButtonUp ("Fire1")) {
			ammoProps = new Ammo ();
			UpdateWeapon ();
		}

		// Check whether we need to fire a ball
		if(isFiring())
		{
			ball = Instantiate (ballPrefab);
			ball.transform.position = m_spawnPoint.transform.position;
			Rigidbody ballRb = ball.GetComponentInChildren<Rigidbody> ();
			ballRb.velocity = m_spawnPoint.transform.rotation * Vector3.forward * shootSpeed;
			BallBehaviour bb =  ball.GetComponent<BallBehaviour>();
			if (bb != null) {
				bb.ammo = ammoProps;
			}
		}
	}

	void UpdateWeapon()
	{
		// Notify the weapong that the ammo has changed
		if (weapon != null)
		{
			AmmoUpdate au = weapon.GetComponent<AmmoUpdate> ();
			if (au != null) {
				au.AmmoUpdated (ammoProps);
			}
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
		ammoProps = new Ammo ();
		UpdateWeapon ();
	}

}
