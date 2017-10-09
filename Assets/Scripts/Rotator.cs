using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float rotationSpeed = 1.0f;

	// Update is called once per frame
    void Update() {
        transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0f);
	}
}
