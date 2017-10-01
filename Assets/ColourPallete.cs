using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPallete : MonoBehaviour {

	public Color[] colours;

	public Color GetColour()
	{
		return colours[Random.Range(0, colours.Length)];
	}
}
