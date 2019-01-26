using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum ERotateAxis {
	X,
	Y,
	Z,
	AX,
	AY,
	AZ
}

public class Room : MonoBehaviour {

	// public Transform viewCenter;
	public int roomNo = -1;
	public RoomDetector detector;
	[HideInInspector]
	public RoomManager manager;
	//  1 -- 2
	// | \   \
	// 5  0 -- 3
	//  \ |   |
	//   4 -- 7
	public Transform cameraRect;
	public GameObject rotateTarget;
	private PlayerController player;

	public List<GameObject> wallList = new List<GameObject>(); // up, down, front, back, left, right

	public List<GameObject> doorList = new List<GameObject>();
	public List<bool> doorOpenState = new List<bool>();
	public List<bool> doorLockState = new List<bool>();

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
		manager.CheckDoorState(roomNo);
		player.EnterRoom(this);
	}

	private void Awake() {
		detector.room = this;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		for (int i = 0; i < 6; i++) {
			doorOpenState.Add(false);
		}

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
	// 	foreach (var item in meshes) {
	// 		if (item != null) item.enabled = true;
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
		player.Floating();
		int value = 0;
		while (value < 90 && value > -90) {
			yield return null;
			if (axis == ERotateAxis.X) {
				rotateTarget.transform.RotateAround(transform.position, transform.right, delta);
				player.transform.RotateAround(transform.position, transform.right, -delta);
				value += delta;
			} else if (axis == ERotateAxis.Y) {
				rotateTarget.transform.RotateAround(transform.position, transform.up, delta);
				value += delta;
			} else if (axis == ERotateAxis.Z) {
				rotateTarget.transform.RotateAround(transform.position, transform.forward, delta);
				player.transform.RotateAround(transform.position, transform.forward, -delta);
				value += delta;
			} else if (axis == ERotateAxis.AX) {
				rotateTarget.transform.RotateAround(transform.position, transform.right, -delta);
				player.transform.RotateAround(transform.position, transform.right, delta);
				value -= delta;
			} else if (axis == ERotateAxis.AY) {
				rotateTarget.transform.RotateAround(transform.position, transform.up, -delta);
				value -= delta;
			} else if (axis == ERotateAxis.AZ) {
				rotateTarget.transform.RotateAround(transform.position, transform.forward, -delta);
				player.transform.RotateAround(transform.position, transform.forward, delta);
				value -= delta;
			}
		}
		if (value != 90 || value != -90) {
			if (axis == ERotateAxis.X) {
				rotateTarget.transform.RotateAround(transform.position, transform.right, 90 - value);
				player.transform.RotateAround(transform.position, transform.right, 90 - value);
			} else if (axis == ERotateAxis.Y) {
				rotateTarget.transform.RotateAround(transform.position, transform.up, 90 - value);
			} else if (axis == ERotateAxis.Z) {
				rotateTarget.transform.RotateAround(transform.position, transform.forward, 90 - value);
				player.transform.RotateAround(transform.position, transform.forward, 90 - value);
			} else if (axis == ERotateAxis.AX) {
				rotateTarget.transform.RotateAround(transform.position, transform.right, -90 - value);
				player.transform.RotateAround(transform.position, transform.right, -90 - value);
			} else if (axis == ERotateAxis.AY) {
				rotateTarget.transform.RotateAround(transform.position, transform.up, -90 - value);
			} else if (axis == ERotateAxis.AZ) {
				rotateTarget.transform.RotateAround(transform.position, transform.forward, -90 - value);
				player.transform.RotateAround(transform.position, transform.forward, -90 - value);
			}
		}

		HideWall();
		player.UnFloating();
		manager.CheckDoorState(roomNo);
		_IsPlayingAnim = false;
	}

	public void OpenDoor(int no, Material mat) {
		if (doorLockState.Count > 0 && doorLockState[no] == true) return;
		var door = doorList[no];
		door.GetComponent<Collider>().enabled = false;
		door.GetComponent<MeshRenderer>().material = mat;
		door.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
		doorOpenState[no] = true;
	}

	public void CloseDoors(Material mat) {
		for (int i = 0; i < doorList.Count; i++) {
			if (doorOpenState[i] == true) {
				var door = doorList[i];
				door.GetComponent<Collider>().enabled = true;
				door.GetComponent<MeshRenderer>().material = mat;
				door.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
				doorOpenState[i] = false;
			}
		}
	}

	public void UnlockDoor(int no) {
		doorLockState[no] = false;
		var mark = doorList[no].transform.GetChild(0);
		mark.GetComponent<MeshRenderer>().material = manager.doorClosedMarkMat;
		manager.CheckDoorState(roomNo);
	}
}