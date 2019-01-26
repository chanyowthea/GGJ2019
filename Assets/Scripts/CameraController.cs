using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {
	// private CinemachineVirtualCamera vc;

	public Room room;
	private PlayerController player;
	public Transform cameraFollowTarget;

	bool isMoving;
	float moveSpeed = 70f;

	public void UpdateRoom(Room room) {
		this.room.rotateTarget.gameObject.SetActive(false);

		this.room = room;

		this.room.rotateTarget.gameObject.SetActive(true);

		transform.position = room.cameraRect.transform.position;
		// vc.LookAt = room.transform;
		isMoving = true;
		counter = 0;
		moveVector = room.transform.position - cameraFollowTarget.position;
		// cameraFollowTarget.position = room.transform.position;
		room.HideWall();
	}

	Vector3 moveVector;
	float counter = 0;
	private void Update() {
		if (isMoving) {

			var len = moveVector.magnitude;

			if (counter < len) {
				cameraFollowTarget.Translate(moveVector.normalized * moveSpeed * Time.deltaTime);
				counter += moveSpeed * Time.deltaTime;
			} else {
				cameraFollowTarget.position = room.transform.position;
				isMoving = false;
			}

		}

	}

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		// vc = GetComponent<CinemachineVirtualCamera>();
	}
}