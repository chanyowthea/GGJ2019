using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	public float speed = 30f;
	public CameraController cam;
	private bool isFloating = false;
	private Rigidbody rb;

	[HideInInspector]
	public Room room;
	public GameObject objFlamePrefab;

	//public AudioClip audioClip;
	private AudioSource audioSource;

	private void Awake() {
		room = null;
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	public void PlaySound() {
		audioSource.Play();
	}

	private void Update() {
		if (isFloating) return;
		var h = Input.GetAxis("Horizontal");
		var v = Input.GetAxis("Vertical");

		if (h != 0 || v != 0) {

			Vector3 dir = new Vector3(h, 0, v);

			if (dir.magnitude > 1) {
				dir = dir.normalized;
			}
			rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.U)) {
			room.Rotate(ERotateAxis.Y);
		}
		if (Input.GetKey(KeyCode.O)) {
			room.Rotate(ERotateAxis.AY);
		}
		if (Input.GetKeyDown(KeyCode.I)) {
			room.Rotate(ERotateAxis.X);
		}
		if (Input.GetKey(KeyCode.K)) {
			room.Rotate(ERotateAxis.AX);
		}
		if (Input.GetKeyDown(KeyCode.J)) {
			room.Rotate(ERotateAxis.Z);
		}
		if (Input.GetKey(KeyCode.L)) {
			room.Rotate(ERotateAxis.AZ);
		}
	}

	public void EnterRoom(Room room) {
		if (this.room == room || room == null) return;

		this.room = room;
		transform.parent = this.room.rotateTarget.transform;

		// room.HideWall();

		cam.UpdateRoom(room);
	}

	public void Floating() {
		isFloating = true;
		rb.velocity = Vector3.zero;
		rb.useGravity = false;
	}

	public void UnFloating() {
		isFloating = false;
		rb.useGravity = true;
	}

	public void PickObject(ObjectToPick objInfo) {

		objInfo.transform.parent = transform;
		objList.Add(objInfo);
		objInfo.BeCarried(transform, transform.up * ((capacity - spaceLeft + 1) * 5f) * objInfo.spaceRequired);
		spaceLeft -= objInfo.spaceRequired;
	}
	public void PlaceObject(GameObject flame) {
		foreach (var item in objList) {
			if (item.flame == flame) {
				spaceLeft += item.spaceRequired;
				item.BePlaced();
				return;
			}
		}

	}

	public int capacity = 1;
	public int spaceLeft = 1;
	[HideInInspector]
	public List<ObjectToPick> objList = new List<ObjectToPick>();

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "ObjectToPick") {
			var objInfo = other.gameObject.GetComponent<ObjectToPick>();
			if (objInfo != null && spaceLeft >= objInfo.spaceRequired) {
				PickObject(objInfo);
			}
		}
		// Debug.Log(other.gameObject.name);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "ObjectFlame") {
			PlaceObject(other.gameObject);
		}
	}

}