using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo  {

	public Color color;

	public Ammo() {
        GameplayManager gpMan = GameplayManager.GetGameplayManager();
        ColourPallete cp = gpMan.GetComponent<ColourPallete>();
        Color newCol = cp.GetColour();
        color = new Color(newCol.r, newCol.g, newCol.b);
	}
}
