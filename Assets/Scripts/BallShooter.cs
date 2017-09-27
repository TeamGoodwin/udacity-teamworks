using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(LineRenderer))]
public class BallShooter : MonoBehaviour {

	// Const properties
	private const float defaultRpm = 600;

	// Common properties
	public bool useLaser = false;				// Should we use the laser or the paintball
	/// </summary>
	public GameObject m_spawnPoint;				// Where should the amo spawn?
	public GameObject weapon;					// The weapon used so we can update it
	public bool singleShot = true;				// Are we restricted to one shot at a time
	public bool continuous = false;				// Can we fire continuously
	public float repeatRateRpm = defaultRpm;	// Weapon fire rate in rounds per minute

	// Laser-relatd properties
	public LineRenderer m_GunFlare;                               // This is used to display the gun as a laser.
	public float laserLength	 = 70f;                       // How far the line renderer will reach if a target isn't hit.
	public float laserTime = 0.07f;                // How long, in seconds, the line renderer and flare are visible for with each shot.
	public float pulseDelay = 0.1f;

	// Ball properoties
	public float shootSpeed = 20;				// How fast does the ammo fire
	public GameObject ballPrefab;				// What object for the ammo?
	public AudioClip ballFireClip;				// Sound of the firing paintball
		
	// Hidden public variables
	public Action<Ammo> weaponChanged;			//  Notifcations when the weapon has been changed


	// Private variables
	private GameObject ball;					// Track the current ammo
	private float fireCountdown = 0.0f;			// How long until we can fire next?
	private float shotTime;						// How long between shots (based on fire rate public variable)
	private Ammo ammoProps;						// Current ammuntion properties

	bool laserFiring = false;

	// Use this for initialization
	void Start () {
		InitVars ();
	}
		

	private bool IsBallFiring(){
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

		// Fire the ball if appropriate
		if (useLaser) {
			CheckLaserFiring ();
		} else {
			CheckBallFiring ();
		}

	}

	void FixedUpdate() {
		if (laserFiring == true) {
			RaycastHit hit;
			if (Physics.Raycast (m_spawnPoint.transform.position, m_spawnPoint.transform.forward, out hit)) {
				if (hit.collider != null) {
					PaintballHit pbHit = hit.collider.gameObject.GetComponent<PaintballHit> ();
					if (pbHit) {
						pbHit.LaserHit (ammoProps.color);
					}
				}
			}
		}
	}

	void CheckBallFiring()
	{
		// Check whether we need to fire a ball
		if(IsBallFiring())
		{
			FirePaintball ();
		}
	}

	void UpdateWeapon()
	{
		if (weaponChanged != null) {
			weaponChanged (ammoProps);
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

	private void InitVars()
	{
		ammoProps = new Ammo ();
		UpdateWeapon ();	
		ReInitVars();
	}

	private void ReInitVars()
	{
		shotTime = 60.0f / repeatRateRpm;
	}

	void CheckLaserFiring()
	{
		if (Input.GetButtonDown ("Fire1")) {
			// We started firing
			StartCoroutine(FireLaser ());
		} 
		else if (Input.GetButtonUp ("Fire1") && continuous) {
			// We stopped firing in a continuous fire situation
			StopLaser();
		}
	}


	private IEnumerator FireLaser()
	{
		Transform target = null;
		laserFiring = true;
		// Set the length of the line renderer to the default.
		float lineLength = laserLength;

		// If there is a target, the line renderer's length is instead the distance from the gun to the target.
		if (target)
			lineLength = Vector3.Distance (m_spawnPoint.transform.position, target.position);

		// Set the shader color
		m_GunFlare.material = new Material(Shader.Find("Particles/Additive"));
		m_GunFlare.startColor = ammoProps.color;
		m_GunFlare.endColor = ammoProps.color;

		// Whilst the line renderer is on move it with the gun.
		yield return StartCoroutine (MoveLineRenderer (lineLength));
	}

	private void StopLaser()
	{
		laserFiring = false;
	}

	private void FirePaintball()
	{
		ball = Instantiate (ballPrefab);
		ball.transform.position = m_spawnPoint.transform.position;
		Rigidbody ballRb = ball.GetComponentInChildren<Rigidbody> ();
		ballRb.velocity = m_spawnPoint.transform.forward * shootSpeed;
		BallBehaviour bb =  ball.GetComponent<BallBehaviour>();
		if (bb != null) {
			bb.ammo = ammoProps;
		}
		if (ballFireClip) {
			AudioSource.PlayClipAtPoint (ballFireClip, m_spawnPoint.transform.position);
		}
	}

	private void MoveLaser(float lineLength)
	{
		// ... set the line renderer to start at the gun and finish forward of the gun the determined distance.
		m_GunFlare.SetPosition(0, m_spawnPoint.transform.position);
		m_GunFlare.SetPosition(1, m_spawnPoint.transform.position + m_spawnPoint.transform.forward * lineLength);
	}

	private IEnumerator MoveLineRenderer (float lineLength)
	{
		// Create a timer.
	
		// While that timer has not yet reached the length of time that the gun effects should be visible for...

		bool useTimer = !continuous || (pulseDelay > 0.0f);
		bool holdAllowed = continuous && (pulseDelay == 0.0f);

		// Fire the laser
		do {
			m_GunFlare.enabled = true;
			float timer = 0f;
			while ((timer < laserTime && useTimer) || (holdAllowed && laserFiring)) {
				// Update the laser position
				MoveLaser (lineLength);

				// Wait for the next frame.
				yield return null;

				// Increment the timer by the amount of time waited.
				timer += Time.deltaTime;
			}

			// Turn the line renderer off again.
			m_GunFlare.enabled = false;
			timer = 0f;

			// Now hold the laser off for the pulse
			while (timer < pulseDelay || !laserFiring) {
				// Wait for the next frame.
				yield return null;

				// Increment the timer by the amount of time waited.
				timer += Time.deltaTime;	
			}
		} while(laserFiring && continuous);

	}
}


