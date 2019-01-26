using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	public float speed = 30f;
	public CameraController cam;
	private Rigidbody rb;

	[HideInInspector]
	public Room room;

	private void Awake() {
		room = null;
		rb = GetComponent<Rigidbody>();
	}

	private void Update() {
		var h = Input.GetAxis("Horizontal");
		var v = Input.GetAxis("Vertical");

		if (h != 0 || v != 0) {

			Vector3 dir = new Vector3(h, 0, v);

			if (dir.magnitude > 1) {
				dir = dir.normalized;
			}
			rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.Y)) {
			room.Rotate(ERotateAxis.Y);
		}
		if (Input.GetKeyDown(KeyCode.X)) {
			room.Rotate(ERotateAxis.X);
		}
		if (Input.GetKeyDown(KeyCode.Z)) {
			room.Rotate(ERotateAxis.Z);
		}
	}

	public void EnterRoom(Room room) {
		if (this.room == room || room == null) return;

		this.room = room;

		// room.HideWall();

		cam.UpdateRoom(room);

	}

}