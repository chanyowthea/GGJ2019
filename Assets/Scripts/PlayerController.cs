using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	public float speed = 30f;
	public CameraRotate cameraState;
	private Rigidbody rb;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	private void Update() {
		var h = Input.GetAxis("Horizontal");
		var v = Input.GetAxis("Vertical");

		if (h != 0 || v != 0) {

			Vector3 dir;
			switch (cameraState.rectPointer) {
				case 0:
					dir = new Vector3(v, 0, -h);
					break;
				case 1:
					dir = new Vector3(-h, 0, -v);
					break;
				case 2:
					dir = new Vector3(-v, 0, h);
					break;
				case 3:
					dir = new Vector3(h, 0, v);
					break;
				default:
					dir = Vector3.zero;
					break;
			}

			if (dir.magnitude > 1) {
				dir = dir.normalized;
			}
			rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
		}

	}

}