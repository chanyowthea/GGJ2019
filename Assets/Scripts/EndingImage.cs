using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingImage : MonoBehaviour {

	public Image image;
	public float timeCost = 2f;

	public void TriggerEnding() {
		if(image) {
			image.enabled = true;
		}
	}

	// Update is called once per frame
	void Update() {

	}
}