using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private CinemachineVirtualCamera vc;

	public Room room;
	private PlayerController player;
	public Transform cameraFollowTarget;

	public void UpdateRoom(Room room) {
		this.room.rotateTarget.gameObject.SetActive(false);
		this.room = room;
		this.room.rotateTarget.gameObject.SetActive(true);
		transform.position = room.cameraRect.transform.position;
		vc.LookAt = room.transform;
		cameraFollowTarget.position = room.transform.position;
		room.HideWall();
	}

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		vc = GetComponent<CinemachineVirtualCamera>();
	}
}