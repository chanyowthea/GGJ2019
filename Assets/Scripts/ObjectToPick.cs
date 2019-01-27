using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToPick : MonoBehaviour {

	public GameObject flamePrefab;
	// public Vector3 orginPosition = Vector3.zero;
	public Transform originTransform;
	// public Quaternion orginRotation;
	[HideInInspector]
	public GameObject flame;

	public Room roomBelongTo;
	public int specialKey = 0;

	private void Awake() {
		if (originTransform == null) {
			flame = Instantiate(flamePrefab, transform.position, transform.rotation, transform.parent);
		} else {
			flame = Instantiate(flamePrefab, originTransform.position, originTransform.rotation, roomBelongTo.rotateTarget.transform);
			originTransform.gameObject.SetActive(false);
		}

		flame.SetActive(false);

		// var randomVector = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		// GetComponent<Rigidbody>().AddForce(randomVector.normalized * 100f);
	}

	public int spaceRequired = 1;
	public bool isCarried = false;
	private Transform playerTrans;
	private Vector3 place;

	public void BeCarried(Transform p, Vector3 v) {
		Debug.Log(transform.name + " be carried");
		playerTrans = p;
		place = v;
		isCarried = true;
		var rb = GetComponent<Rigidbody>();
		if (rb) {
			rb.useGravity = false;
		}
		var col = GetComponent<Collider>();
		col.enabled = false;
		flame.SetActive(true);
	}

	public void BePlaced() {
		transform.parent = flame.transform.parent;
		transform.position = flame.transform.position;
		transform.rotation = flame.transform.rotation;

		var rb = GetComponent<Rigidbody>();
		if (rb) {
			rb.Sleep();
		}
		var col = GetComponent<Collider>();
		col.enabled = false;
		isCarried = false;
		flame.SetActive(false);
		if (specialKey != 0) {
			switch (specialKey) {
				case 1:
					roomBelongTo.UnlockDoor(5);
					break;
				case 2:
					roomBelongTo.UnlockDoor(3);
					break;
				case 3:
					roomBelongTo.UnlockDoor(2);
					roomBelongTo.manager.End();
					break;
			}
			gameObject.SetActive(false);
		}
		roomBelongTo.manager.player.PlaySound();
	}

	private void Update() {
		if (isCarried) {
			transform.position = playerTrans.position + place;
		}
	}
}