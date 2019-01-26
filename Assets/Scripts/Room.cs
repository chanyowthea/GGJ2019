using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum ERotateAxis {
	X,
	Y,
	Z,
}

public class Room : MonoBehaviour {

	// public Transform viewCenter;
	public RoomDetector detector;
	//  1 -- 2
	// | \   \
	// 5  0 -- 3
	//  \ |   |
	//   4 -- 7
	public Transform cameraRect;
	public GameObject rotateTarget;

	public List<GameObject> wallList = new List<GameObject>(); // up, down, front, back, left, right

	public List<GameObject> GetWallFromDirection() {

		var list = new List<GameObject>();

		float dis1 = 999999, dis2 = 999999, dis3 = 999999;
		int pointer1 = -1, pointer2 = -1, pointer3 = -1;

		for (int i = 0; i < wallList.Count; i++) {
			var dis = (cameraRect.position - wallList[i].transform.position).sqrMagnitude;
			if (dis > dis3) continue;
			if (dis > dis2) {
				dis3 = dis;
				pointer3 = i;

				continue;
			}
			if (dis > dis1) {
				dis3 = dis2;
				pointer3 = pointer2;

				dis2 = dis;
				pointer2 = i;
				continue;
			} else {

				dis3 = dis2;
				pointer3 = pointer2;

				dis2 = dis1;
				pointer2 = pointer1;

				dis1 = dis;
				pointer1 = i;
				continue;
			}
		}

		list.Add(wallList[pointer1]);
		list.Add(wallList[pointer2]);
		list.Add(wallList[pointer3]);

		return list;
	}

	public void OnRoomIn() {
		Debug.Log("Enter " + transform.name);
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		player.EnterRoom(this);
	}

	private void Awake() {
		detector.room = this;
	}

	bool _IsPlayingAnim;
	private List<GameObject> hidenWalls = new List<GameObject>();

	public void Rotate(ERotateAxis axis) {
		if (_IsPlayingAnim) {
			return;
		}
		StartCoroutine(RotateRoutine(axis, (int) (ConstValue._RoomRotateSpeed)));
	}

	// public void OperateMeshLoop(MeshRenderer mesh, bool b) {
	// 	if (mesh != null) mesh.enabled = b;
	// 	var meshes = mesh.GetComponentsInChildren<MeshRenderer>();
	// 	foreach (var subItem in meshes) {
	// 		OperateMeshLoop(subItem, b);
	// 	}
	// }

	public void HideWall() {
		Debug.Log("Trigger Hide Wall");
		foreach (var item in hidenWalls) {
			var mesh = item.GetComponent<MeshRenderer>();
			if (mesh != null) mesh.enabled = true;

			var meshes = item.GetComponentsInChildren<MeshRenderer>();
			foreach (var subItem in meshes) {
				if (subItem != null) subItem.enabled = true;
			}
		}
		hidenWalls = GetWallFromDirection();
		foreach (var item in hidenWalls) {
			var mesh = item.GetComponent<MeshRenderer>();
			if (mesh != null) mesh.enabled = false;
			var meshes = item.GetComponentsInChildren<MeshRenderer>();
			foreach (var subItem in meshes) {
				if (subItem != null) subItem.enabled = false;
			}
		}
	}

	IEnumerator RotateRoutine(ERotateAxis axis, int delta) {
		_IsPlayingAnim = true;
		int value = 0;
		while (value < 90) {
			yield return null;
			if (axis == ERotateAxis.X) {
				rotateTarget.transform.RotateAround(transform.position, transform.right, delta);
			} else if (axis == ERotateAxis.Y) {
				rotateTarget.transform.RotateAround(transform.position, transform.up, delta);
			} else if (axis == ERotateAxis.Z) {
				rotateTarget.transform.RotateAround(transform.position, transform.forward, delta);
			}
			value += delta;
		}
		if (value != 90) {
			if (axis == ERotateAxis.X) {
				rotateTarget.transform.RotateAround(transform.position, transform.right, 90 - value);
			} else if (axis == ERotateAxis.Y) {
				rotateTarget.transform.RotateAround(transform.position, transform.up, 90 - value);
			} else if (axis == ERotateAxis.Z) {
				rotateTarget.transform.RotateAround(transform.position, transform.forward, 90 - value);
			}
		}

		HideWall();

		_IsPlayingAnim = false;
	}
}