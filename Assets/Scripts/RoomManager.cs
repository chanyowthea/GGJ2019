using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public PlayerController player;

	public List<Room> roomList = new List<Room>();
	public Material doorOpenedMat;
	public Material doorClosedMat;
	public Material doorClosedMarkMat;

	private void Awake() {
		foreach (var item in roomList) {
			item.rotateTarget.SetActive(false);
			item.manager = this;
		}
	}

	bool IsInGap(Vector3 eulerAngle, Vector3 targetEulerAngle) {
		if (IsInGap(eulerAngle.x, targetEulerAngle.x)) {
			if (IsInGap(eulerAngle.y, targetEulerAngle.y)) {
				if (IsInGap(eulerAngle.z, targetEulerAngle.z)) {
					return true;
				}
			}
		}
		return false;
	}

	bool IsInGap(float degree, float targetDegree) {
		if (targetDegree == 0) {
			if (degree > 1 && degree < 359) return false;
		}
		if (degree > targetDegree - 1 && degree < targetDegree + 1) return true;
		else return false;
		// if (degree > 0 && degree < targetDegree - 1) return false;
		// if (degree < 360 && degree > targetDegree + 1) return false;
		// return true;
	}

	public void CheckDoorState(int roomNo) {

		var room = roomList[roomNo];
		var eulerAngle = room.rotateTarget.transform.eulerAngles;
		Debug.Log("CheckDoorState " + eulerAngle);
		room.CloseDoors(doorClosedMat);
		switch (roomNo) {
			case 0:
				if (IsInGap(eulerAngle, new Vector3(0, 0, 0))) {
					room.OpenDoor(2, doorOpenedMat);
				}
				break;
			case 1:
				if (IsInGap(eulerAngle, new Vector3(0, 0, 0))) {
					room.OpenDoor(2, doorOpenedMat);
					room.OpenDoor(3, doorOpenedMat);
					room.OpenDoor(5, doorOpenedMat);
				}
				if (IsInGap(eulerAngle, new Vector3(0, 180, 0))) {
					room.OpenDoor(2, doorOpenedMat);
					room.OpenDoor(3, doorOpenedMat);
				}
				break;
			case 2:
				if (IsInGap(eulerAngle, new Vector3(0, 0, 0))) {
					room.OpenDoor(3, doorOpenedMat);
				}
				break;
			case 3:
				if (IsInGap(eulerAngle, new Vector3(0, 0, 0))) {
					room.OpenDoor(4, doorOpenedMat);
					if (IsInGap(roomList[4].rotateTarget.transform.eulerAngles, Vector3.zero)) {
						room.OpenDoor(3, doorOpenedMat);
					}
					if (IsInGap(roomList[5].rotateTarget.transform.eulerAngles, Vector3.zero)) {

						room.OpenDoor(5, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(0, 90, 0))) {
					room.OpenDoor(3, doorOpenedMat);
					if (IsInGap(roomList[4].rotateTarget.transform.eulerAngles, Vector3.zero)) {
						room.OpenDoor(5, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(0, 180, 0))) {
					room.OpenDoor(5, doorOpenedMat);
					if (IsInGap(roomList[5].rotateTarget.transform.eulerAngles, Vector3.zero)) {
						room.OpenDoor(4, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(0, 270, 0))) {
					if (IsInGap(roomList[4].rotateTarget.transform.eulerAngles, Vector3.zero)) {
						room.OpenDoor(4, doorOpenedMat);
					}

					if (IsInGap(roomList[5].rotateTarget.transform.eulerAngles, Vector3.zero)) {
						room.OpenDoor(3, doorOpenedMat);
					}
				}
				break;
			case 4:
				if (IsInGap(eulerAngle, new Vector3(0, 0, 0))) {
					room.OpenDoor(2, doorOpenedMat);
				}
				if (IsInGap(eulerAngle, new Vector3(270, 180, 0))) {
					if (IsInGap(roomList[6].rotateTarget.transform.eulerAngles, new Vector3(90, 0, 0))) {
						room.OpenDoor(2, doorOpenedMat);
					}
				}
				break;
			case 5:
				if (IsInGap(eulerAngle, new Vector3(0, 0, 0))) {
					room.OpenDoor(4, doorOpenedMat);
					if (IsInGap(roomList[8].rotateTarget.transform.eulerAngles, new Vector3(0, 0, 0))) {
						room.OpenDoor(0, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(0, 90, 0))) {
					if (IsInGap(roomList[8].rotateTarget.transform.eulerAngles, new Vector3(0, 90, 0))) {
						room.OpenDoor(0, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(0, 180, 0))) {
					if (IsInGap(roomList[8].rotateTarget.transform.eulerAngles, new Vector3(0, 180, 0))) {
						room.OpenDoor(0, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(0, 270, 0))) {
					if (IsInGap(roomList[8].rotateTarget.transform.eulerAngles, new Vector3(0, 270, 0))) {
						room.OpenDoor(0, doorOpenedMat);
					}
				}
				break;
			case 6:
				if (IsInGap(eulerAngle, new Vector3(0, 90, 0))) {
					if (IsInGap(roomList[7].rotateTarget.transform.eulerAngles, new Vector3(0, 180, 0))) {
						room.OpenDoor(2, doorOpenedMat);
					}

					room.OpenDoor(5, doorOpenedMat);

				}
				if (IsInGap(eulerAngle, new Vector3(90, 0, 0))) {
					if (IsInGap(roomList[7].rotateTarget.transform.eulerAngles, new Vector3(0, 180, 0))) {
						room.OpenDoor(5, doorOpenedMat);
					}
					if (IsInGap(roomList[4].rotateTarget.transform.eulerAngles, new Vector3(270, 180, 0))) {
						room.OpenDoor(2, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(180, 270, 180))) {
					if (IsInGap(roomList[7].rotateTarget.transform.eulerAngles, new Vector3(0, 180, 0))) {
						room.OpenDoor(2, doorOpenedMat);
					}
				}
				break;
			case 7:
				// if (IsInGap(eulerAngle, new Vector3(0, 90, 0))) {
				// 	if (IsInGap(roomList[6].rotateTarget.transform.eulerAngles, new Vector3(0, 90, 0))) {
				// 		room.OpenDoor(3, doorOpenedMat);
				// 	}
				// }
				if (IsInGap(eulerAngle, new Vector3(0, 180, 0))) {
					if (IsInGap(roomList[8].rotateTarget.transform.eulerAngles, new Vector3(0, 0, 0))) {
						room.OpenDoor(3, doorOpenedMat);
					}
					if (IsInGap(roomList[6].rotateTarget.transform.eulerAngles, new Vector3(0, 90, 0))) {
						room.OpenDoor(5, doorOpenedMat);
					}
					if (IsInGap(roomList[6].rotateTarget.transform.eulerAngles, new Vector3(90, 0, 0))) {
						room.OpenDoor(5, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(0, 270, 0))) {
					if (IsInGap(roomList[8].rotateTarget.transform.eulerAngles, new Vector3(0, 0, 0))) {
						room.OpenDoor(5, doorOpenedMat);
					}
				}
				break;
			case 8:
				if (IsInGap(eulerAngle, new Vector3(0, 0, 0))) {
					if (IsInGap(roomList[5].rotateTarget.transform.eulerAngles, new Vector3(0, 0, 0))) {
						room.OpenDoor(1, doorOpenedMat);
					}
					if (IsInGap(roomList[7].rotateTarget.transform.eulerAngles, new Vector3(0, 270, 0))) {
						room.OpenDoor(3, doorOpenedMat);
					}
					if (IsInGap(roomList[7].rotateTarget.transform.eulerAngles, new Vector3(0, 180, 0))) {
						room.OpenDoor(3, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(0, 90, 0))) {
					if (IsInGap(roomList[5].rotateTarget.transform.eulerAngles, new Vector3(0, 90, 0))) {
						room.OpenDoor(1, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(0, 180, 0))) {
					if (IsInGap(roomList[5].rotateTarget.transform.eulerAngles, new Vector3(0, 180, 0))) {
						room.OpenDoor(1, doorOpenedMat);
					}
				}
				if (IsInGap(eulerAngle, new Vector3(0, 270, 0))) {
					if (IsInGap(roomList[5].rotateTarget.transform.eulerAngles, new Vector3(0, 270, 0))) {
						room.OpenDoor(1, doorOpenedMat);
					}

				}
				break;

		}
	}

}