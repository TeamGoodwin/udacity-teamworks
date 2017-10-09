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
	public float colorCompeleteThreshold = 0.1f;

	private int hitCount = 0;
    private Color startColor;
    private Color hitColor;


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
        startColor = _renderer.material.color;
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
        if (!_painted)
        {
            // TODO: Color should only be set-able once ultimately, for now, just store the last hit color:
            hitColor = col;
            
            // Calculate the color increments
            float colorIncrement = colorCompeleteThreshold / hitsToBonus;
            _colorComplete += colorIncrement;

            // Update the color
            Color newCol = Color.Lerp(startColor, col, _colorComplete);
            _renderer.material.color = newCol;

            // Update the hit count, register the score
            if (!_painted)
            {
                hitCount++;
                gpMan.addScore(hitScore);
            }
        }
	}

	void Update()
	{
		// Check for completion of the color
		if (_colorComplete >= colorCompeleteThreshold && !_painted) {
			ParticleSystem.MainModule main = _particles.main;
            _renderer.material.color = hitColor;
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
