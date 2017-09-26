using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballHit : MonoBehaviour {

	float _colorComplete = 0.0f;

	// Steal the colour of the colliding object
    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        Color col = obj.GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = col;

		_colorComplete = 1.0f;
        gameObject.layer = LayerMask.NameToLayer("Outline");
    }

	public void LaserHit(Color col)
	{
		_colorComplete += 0.001f;
		Color newCol = Color.Lerp (GetComponent<Renderer> ().material.color, col, _colorComplete);
		GetComponent<Renderer>().material.color = newCol;
	}
}
