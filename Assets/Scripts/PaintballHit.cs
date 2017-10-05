using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballHit : MonoBehaviour {

	public AudioClip completeClip;

	float _colorComplete = 0.0f;
	Renderer _renderer;
	ParticleSystem _particles;
	bool _painted = false;

	public int hitScore = 5;
	public int hitsToBonus = 50;
	public int bonusScore = 35;
	public float colorCompeleteThreshold = 0.5f;

	private int hitCount = 0;


	private GameplayManager gpMan;

	void Awake()
	{
		_renderer = GetComponent<Renderer> ();
		_particles = GetComponentInChildren<ParticleSystem> ();
		gpMan = GameplayManager.GetGameplayManager ();
	}

	void Start()
	{
		gpMan = GameplayManager.GetGameplayManager ();
		RegisterMaxScore ();
	}


	// Steal the colour of the colliding object
    void OnCollisionEnter(Collision collision)
    {
        // Currently this sript handles assumes paintball hit
		GameObject obj = collision.gameObject;
        Color col = obj.GetComponent<Renderer>().material.color;
        _renderer.material.color = col;

		_colorComplete = 1.0f;
        gameObject.layer = LayerMask.NameToLayer("Outline");
    }

	public void LaserHit(Color col)
	{
		// Calculate the color increments
		float colorIncrement = colorCompeleteThreshold / hitsToBonus;
		_colorComplete += colorIncrement;

		// Update the color
		Color newCol = Color.Lerp (_renderer.material.color, col, _colorComplete);
		_renderer.material.color = newCol;

		// Update the hit count, register the score
		if (!_painted) {
			hitCount++;
			gpMan.addScore (hitScore);
		}

	}

	void Update()
	{
		// Check for completion of the color
		if (_colorComplete >= 0.5f && !_painted) {
			ParticleSystem.MainModule main = _particles.main;
			main.startColor = _renderer.material.color;
			_particles.Play ();
			AudioSource.PlayClipAtPoint (completeClip, Camera.main.transform.position, 0.3f);
			_painted = true;

			// We score an instant bonus when the color is complete
			InstantBonus ();
		}
	}

	void UpdateScore(int newHits) 
	{
		// Cap the new hits at the max hits
		if (newHits + hitCount > hitsToBonus) {
			newHits = hitsToBonus - hitCount;
		}

		// Calculate the base score
		int newScore = newHits * hitScore;
		hitCount += newHits;

		// Check for a bonus
		if (hitCount == hitsToBonus) {
			newScore += bonusScore;
		}

		// Register the score with the level manager
		gpMan.addScore (newScore);
	}

	void InstantBonus()
	{
		UpdateScore (hitsToBonus - hitCount);
	}

	void RegisterMaxScore () {
		int maxScore = hitsToBonus * hitScore + bonusScore;
		gpMan.registerItemMaxScore (maxScore);
	}


}
