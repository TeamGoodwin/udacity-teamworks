using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballHit : MonoBehaviour {



    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        Color col = obj.GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = col;

        Destroy(obj);
        
        gameObject.layer = LayerMask.NameToLayer("Outline");
    }
}
