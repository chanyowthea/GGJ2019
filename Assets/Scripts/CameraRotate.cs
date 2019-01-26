using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {
	public Transform rotateCenter;
	public float roateSpeed = 50;

	public List<Transform> rectList = new List<Transform>();
	private int rectPointer = 0;

	private int isRotating = 0;
	private float degreeCounter = 0;
	private float targetDegree = 0;

	public void SmoothRotate(int dir) { // 1: clockwise, -1: anticlockwise
		var degree = roateSpeed * Time.deltaTime * dir;

		if (dir == 1) {
			if (degreeCounter + degree > targetDegree) {
				degree = targetDegree - degreeCounter;
				degreeCounter = targetDegree;
				isRotating = 0;
				transform.position = rectList[rectPointer].position;
			} else {
				degreeCounter += degree;
				transform.RotateAround(rotateCenter.position, rotateCenter.up, degree);
			}

		} else if (dir == -1) {
			if (degreeCounter + degree < targetDegree) {
				degree = degreeCounter - targetDegree;
				degreeCounter = targetDegree;
				isRotating = 0;
				transform.position = rectList[rectPointer].position;
			} else {
				degreeCounter += degree;
				transform.RotateAround(rotateCenter.position, rotateCenter.up, degree);
			}

		}
	}

	private void Update() {
		if (Input.GetKey(KeyCode.W)) {
			transform.Translate(transform.forward * Time.deltaTime * 10);
		}
		if (isRotating == 0) {
			if (Input.GetKey(KeyCode.Q)) {
				isRotating = 1;
				rectPointer++;
				if (rectPointer >= rectList.Count) {
					rectPointer = 0;
				}
				targetDegree = targetDegree + 90;
				Debug.Log("Rotate Q");
			}
			if (Input.GetKey(KeyCode.E)) {
				isRotating = -1;
				rectPointer--;
				if (rectPointer < 0) {
					rectPointer = rectList.Count - 1;
				}
				targetDegree = targetDegree - 90;
				Debug.Log("Rotate E");
			}
		} else {
			SmoothRotate(isRotating);
		}

	}
}