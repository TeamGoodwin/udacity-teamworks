using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour {

	public UnityEngine.UI.Text displayText;

	private bool dirty = true;
	private int score = 0;
	private int totalScore = 0;
	private float remainingTime;

	// Update the score/timer  after all updates are in
	void Update () {
		// Check if we need to update the text
		if (dirty) {
			dirty = false;
			string timeString = "";
			string scoreString = "";
			string displayString = "Score: " + score + " /  " + totalScore + "\n"
				+ "Time: " + new System.TimeSpan(0,0,Mathf.RoundToInt(remainingTime));
			if (displayText) {
				displayText.text = displayString;
			}
		}
		
	}

	public float RemainingTime
	{
		set {
			dirty = true;
			remainingTime = value;
		}
		get {
			return remainingTime;
		}
	}

	public int Score
	{
		set 		{
			dirty = false;
			score = value;
		}
		get {
			return score;
		}
	}

	public int TotalScore
	{
		set {
			dirty = false;
			totalScore = value;
		}
		get {
			return totalScore;
		}
	}
}
