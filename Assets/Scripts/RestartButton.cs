using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

	public void OnButtonPressed() {
		// Debug.Log("AA");
		SceneManager.LoadScene(1);
	}
}