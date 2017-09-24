using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    public float lifeTime = 3.0f;

	// Properties of the ammo
	private Ammo ammoProps;

	public Ammo ammo
	{
		get {
			return ammo;
		}
		set {
			ammoProps = value;
			Renderer objRend = GetComponent<Renderer>();
			if (objRend != null) {
				objRend.material.color = ammoProps.color;
			}
		}
	}

	// Use this for initialization
	void Start () {
 
	}

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f) {
            Destroy(gameObject);
        }
    }

	// If this object hits something, the object destroys itself
	void OnCollisionEnter(Collision collision)
	{
		Destroy (gameObject);
	}




}
