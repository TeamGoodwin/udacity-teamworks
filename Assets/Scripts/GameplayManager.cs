using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayState
{
	Playing = 0,
	LevelEnding,
	EndOfLevel
};

public class GameplayManager : MonoBehaviour {



	public float gameTimeSeconds = 90;
	public HudManager hudManager;
	public GameObject goLevelSumamry;
	public float endSceneTime = 3.0f;

	// Timing
	private float timeRem = 0;
	private bool done = false;
	private float endSceneCountDown = 0f;

	// Scorekeeping
	private int currentScore = 0;
	private int maxScore = 0;

	// Hidden public members
	public Action levelOver;

	private bool dirty = false;

	private PlayState playState = PlayState.Playing;

	// Use this for initialization
	void Start () {
		ResetTimer ();
		StartTimer ();

		if (goLevelSumamry) {
			goLevelSumamry.SetActive (false);
		} else {
			Debug.Log ("No Level Summary Object set on Gameplay Mangager");
		}
	}

	void StartTimer()
	{
		timeRem = gameTimeSeconds;
	}

	void ResetTimer()
	{
		done = false;
	}

	// Get the current time
	public float CurrentTime {
		get {
			return timeRem;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (playState == PlayState.Playing) {
			// Manage the timer
			if (timeRem > 0f && !done) {
				timeRem -= Time.deltaTime;
				if (timeRem <= 0f) {
					done = true;
				}

				// Update the HUD manager
				if (hudManager) {
					hudManager.RemainingTime = timeRem;
				}
			}

			// If the score changed updated the HUD
			if (dirty) {
				dirty = false;
				hudManager.TotalScore = maxScore;
				hudManager.Score = currentScore;
			}

			// Check if we beat the leavel
			if (currentScore >= maxScore || done) {
				EndOfLevel ();
			}
		} else if (playState == PlayState.LevelEnding){
			UpdateLevelEnding ();
		} else if (playState == PlayState.EndOfLevel){
			// Wait for the fire button and restart
			if (Input.GetButtonDown("Fire1")) {
				UnityEngine.SceneManagement.SceneManager.LoadScene ("StartScene2");
			}
		}
	}

	// Increment the score when a suitable game event occurs
	public void addScore(int value)
	{
		currentScore += value;
		dirty = true;
	}

	// Register the maximum scores an item can provide so we can track possible max score
	public void registerItemMaxScore(int value)
	{
		maxScore += value;
		dirty = true;
	}

	void EndOfLevel()
	{
		// Begin the scene ending process
		playState = PlayState.LevelEnding;
		endSceneCountDown = endSceneTime;

		string displayText = "";

		if (currentScore == maxScore) {
			displayText = "You're Awesome!\n"
				+ "You beat the level with " + new System.TimeSpan (0, 0, Mathf.RoundToInt(timeRem)) + " to spare\n"
			+ "You get the top level score of " + maxScore + "\n";
		} else {
			displayText = "Great job guys!\n"
			+ "Your score was " + currentScore + "\n";
		}

		// Get end of level text screen
		UnityEngine.UI.Text uiText = goLevelSumamry.GetComponent<UnityEngine.UI.Text>();
		if (uiText) {
			uiText.text = displayText;
		}

		if (goLevelSumamry) {
			goLevelSumamry.SetActive(true);
		}

		// Signal to all subscribers that the level is over
		if (levelOver != null) {
			levelOver ();
		}
	}

	void UpdateLevelEnding()
	{
		endSceneCountDown -= Time.deltaTime;
		if (endSceneCountDown <= 0f) {
			playState = PlayState.EndOfLevel;

			string displayText = "\nPress fire to restart";

			// Get end of level text screen
			UnityEngine.UI.Text uiText = goLevelSumamry.GetComponent<UnityEngine.UI.Text>();
			if (uiText) {
				uiText.text += displayText;
			}
		}
	}

	public static GameplayManager GetGameplayManager()
	{		
		GameplayManager levMan = null;

		// Get the Level manager
		GameObject goLev = GameObject.Find("GameplayManager");
		if (goLev) {
			// Find the Level Manager script
			levMan = goLev.GetComponent<GameplayManager>();
			if (!levMan) {
				Debug.Log ("Cannot find Gameplay Manager script");
			}
		} else {
			Debug.Log ("Error finding Gameplay Manager Game Object");
		}

		return levMan;
	}

	public PlayState State {
		get {
			return playState;
		}
	}
}
