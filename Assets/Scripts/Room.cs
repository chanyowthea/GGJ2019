using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	// public Transform viewCenter;
	public RoomDetector detector;
	//  1 -- 2
	// | \   \
	// 5  0 -- 3
	//  \ |   |
	//   4 -- 7
	public List<Transform> rectList = new List<Transform>();

	public List<GameObject> wallList = new List<GameObject>(); // up, down, front, back, left, right

	public List<GameObject> GetWallFromDirection() {

		var list = new List<GameObject>();

		float dis1 = 999999, dis2 = 999999, dis3 = 999999;
		int pointer1 = -1, pointer2 = -1, pointer3 = -1;

		for (int i = 0; i < wallList.Count; i++) {
			var dis = (Camera.main.transform.position - wallList[i].transform.position).sqrMagnitude;
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
		

		// switch (d) {
		// 	case 0:
		// 		list.Add(wallList[0]);
		// 		list.Add(wallList[3]);
		// 		list.Add(wallList[4]);
		// 		return list;
		// 	case 1:
		// 		list.Add(wallList[0]);
		// 		list.Add(wallList[2]);
		// 		list.Add(wallList[4]);
		// 		return list;
		// 	case 2:
		// 		list.Add(wallList[0]);
		// 		list.Add(wallList[2]);
		// 		list.Add(wallList[5]);
		// 		return list;
		// 	case 3:
		// 		list.Add(wallList[0]);
		// 		list.Add(wallList[5]);
		// 		list.Add(wallList[3]);
		// 		return list;
		// }
		return list;
	}

	public void OnRoomIn() {
		Debug.Log(transform.name + " In ");
	}

	private void Awake() {
		detector.room = this;
	}
}