using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraRotate : MonoBehaviour {
	// public Transform rotateCenter;
	public float roateSpeed = 50;

	// public List<Transform> rectList = new List<Transform>();
	[HideInInspector]
	public int rectPointer = 0;
	[HideInInspector]
	public int rectPointerDelay = 0;
	private List<GameObject> hidenWalls = new List<GameObject>();

	private int isRotating = 0;
	private float degreeCounter = 0;
	private float targetDegree = 0;

	private CinemachineVirtualCamera vc;

	public Room room;

	public void SmoothRotate(int dir) { // 1: clockwise, -1: anticlockwise
		var degree = roateSpeed * Time.deltaTime * dir;

		if (dir == 1) {
			if (degreeCounter + degree > targetDegree) {
				degree = targetDegree - degreeCounter;
				degreeCounter = targetDegree;
				isRotating = 0;
				transform.position = room.rectList[rectPointer].position;

				foreach (var item in hidenWalls) {
					item.SetActive(true);
				}
				hidenWalls = room.GetWallFromDirection();
				foreach (var item in hidenWalls) {
					item.SetActive(false);
				}
			} else {
				degreeCounter += degree;
				transform.RotateAround(room.transform.position, room.transform.up, degree);
			}

		} else if (dir == -1) {
			if (degreeCounter + degree < targetDegree) {
				degree = degreeCounter - targetDegree;
				degreeCounter = targetDegree;
				isRotating = 0;
				transform.position = room.rectList[rectPointer].position;

				foreach (var item in hidenWalls) {
					item.SetActive(true);
				}
				hidenWalls = room.GetWallFromDirection();
				foreach (var item in hidenWalls) {
					item.SetActive(false);
				}
			} else {
				degreeCounter += degree;
				transform.RotateAround(room.transform.position, room.transform.up, degree);
			}

		}
	}

	private void Update() {
		if (isRotating == 0) {
			if (Input.GetKey(KeyCode.Q)) {
				isRotating = 1;
				rectPointerDelay = rectPointer;
				rectPointer++;
				if (rectPointer >= room.rectList.Count) {
					rectPointer = 0;
				}
				targetDegree = targetDegree + 90;
				Debug.Log("Rotate Q [clockwise]");
			}
			if (Input.GetKey(KeyCode.E)) {
				isRotating = -1;
				rectPointerDelay = rectPointer;
				rectPointer--;
				if (rectPointer < 0) {
					rectPointer = room.rectList.Count - 1;
				}
				targetDegree = targetDegree - 90;
				Debug.Log("Rotate E [anticlockwise]");
			}
		} else {
			SmoothRotate(isRotating);
		}

	}

	private void Start() {

		vc = GetComponent<CinemachineVirtualCamera>();
		// vc.LookAt = room.viewCenter;

		rectPointer = 0;
		rectPointerDelay = 0;

		transform.position = room.rectList[rectPointer].position;
		hidenWalls = room.GetWallFromDirection();
		foreach (var item in hidenWalls) {
			item.SetActive(false);
		}

	}
}