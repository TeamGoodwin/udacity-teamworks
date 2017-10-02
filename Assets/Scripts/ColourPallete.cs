using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPallete : MonoBehaviour {

	public Color[] _colours;

	public Color GetColour()
	{
		return _colours[Random.Range(0, _colours.Length)];
	}
}
