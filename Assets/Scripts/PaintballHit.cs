using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballHit : MonoBehaviour {


	// Steal the colour of the colliding object
    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        Color col = obj.GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = col;

        gameObject.layer = LayerMask.NameToLayer("Outline");
    }
}
