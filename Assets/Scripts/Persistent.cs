using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistent : MonoBehaviour {

	private void Awake() {
		DontDestroyOnLoad(this);
	}

	public void OnStart() {
		SceneManager.LoadScene(1);
	}

}
