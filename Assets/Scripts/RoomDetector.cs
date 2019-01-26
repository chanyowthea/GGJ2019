using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetector : MonoBehaviour {

	[HideInInspector]
	public Room room;

	private void OnTriggerEnter(Collider other) {
		room.OnRoomIn();
	}

}