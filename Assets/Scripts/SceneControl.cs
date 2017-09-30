using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {
	public static int gun = 0;

	public void LoadSceneWithUzi () {
		SceneManager.LoadScene ("ColorPrototype");
	}

	public void LoadSceneWithM200 () {
		gun = 1;
		SceneManager.LoadScene ("ColorPrototype");
	}

	public void LoadSceneWithSpas () {
		gun = 2;
		SceneManager.LoadScene ("ColorPrototype");
	}
}
