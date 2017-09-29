using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerText : MonoBehaviour {
	private float totalTime;
	private int minite;
	private float second;
	private float oldSecond;

	// Use this for initialization
	void Start () {
		minite = 1;
		second = 30f;
		totalTime = minite * 60 + second;
		oldSecond = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale > 0 && totalTime > 0f) {
			totalTime = minite * 60 + second;
			totalTime -= Time.deltaTime;

			minite = (int)(totalTime / 60);
			second = totalTime - minite * 60;

			if(second != oldSecond) {
				GetComponent<UnityEngine.UI.Text> ().text = minite.ToString("00") + ":" + Mathf.FloorToInt(second).ToString("00");
			}
			oldSecond = second;
			if(totalTime <= 0f) {
				GetComponent<UnityEngine.UI.Text> ().text = "Game Over";
				//Debug.Log("Game Over");
			}
		}
	}
}
