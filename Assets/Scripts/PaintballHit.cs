using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballHit : MonoBehaviour {

	public AudioClip completeClip;

	float _colorComplete = 0.0f;
	Renderer _renderer;
	ParticleSystem _particles;
	bool _painted = false;

	void Awake()
	{
		_renderer = GetComponent<Renderer> ();
		_particles = GetComponent<ParticleSystem> ();
	}

	// Steal the colour of the colliding object
    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        Color col = obj.GetComponent<Renderer>().material.color;
        _renderer.material.color = col;

		_colorComplete = 1.0f;
        gameObject.layer = LayerMask.NameToLayer("Outline");
    }

	public void LaserHit(Color col)
	{
		_colorComplete += 0.01f;
		Color newCol = Color.Lerp (_renderer.material.color, col, _colorComplete);
		_renderer.material.color = newCol;
	}

	void Update()
	{
		if (_colorComplete >= 0.5f && !_painted) {
			ParticleSystem.MainModule main = _particles.main;
			main.startColor = _renderer.material.color;
			_particles.Play ();
			AudioSource.PlayClipAtPoint (completeClip, transform.position);
			_painted = true;
		}
	}
}
