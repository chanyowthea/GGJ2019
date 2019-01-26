using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public List<Room> roomList = new List<Room>();

	private void Awake() {
		foreach (var item in roomList) {
			item.rotateTarget.SetActive(false);
		}
	}
}