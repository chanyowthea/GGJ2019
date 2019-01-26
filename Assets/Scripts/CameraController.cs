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
		// var numb = room.rotateTarget.transform.childCount;
		// for (int i = 0; i < numb; i++) {
		// 	var item = room.rotateTarget.transform.GetChild(i);
		// 	var mesh = item.GetComponent<MeshRenderer>();
		// 	if (mesh != null) mesh.enabled = true;

		// 	var meshes = item.GetComponentsInChildren<MeshRenderer>();
		// 	foreach (var subItem in meshes) {
		// 		if (subItem != null) subItem.enabled = true;
		// 	}
		// }

		this.room = room;

		this.room.rotateTarget.gameObject.SetActive(true);
		// numb = room.rotateTarget.transform.childCount;
		// for (int i = 0; i < numb; i++) {
		// 	var item = room.rotateTarget.transform.GetChild(i);
		// 	var mesh = item.GetComponent<MeshRenderer>();
		// 	if (mesh != null) mesh.enabled = true;

		// 	var meshes = item.GetComponentsInChildren<MeshRenderer>();
		// 	foreach (var subItem in meshes) {
		// 		if (subItem != null) subItem.enabled = true;
		// 	}
		// }

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