using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPallete : MonoBehaviour {

	public Color[] _colours;

    private int lastColorIndex  = -1;

	public Color GetColour()
	{
        int colIndex;
        do
        {
            colIndex = Random.Range(0, _colours.Length);
        } while (lastColorIndex == colIndex && _colours.Length > 1);
        lastColorIndex = colIndex;
        
        return _colours[colIndex];
	}
}
