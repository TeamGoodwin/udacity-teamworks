using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    public float lifeTime = 3.0f;

	// Use this for initialization
	void Start () {
        Renderer objRend = GetComponent<Renderer>();
        objRend.material.color = PickColor();
	}

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f) {
            Destroy(gameObject);
        }
    }

    private Color PickColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
