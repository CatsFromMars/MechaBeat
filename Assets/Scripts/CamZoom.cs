using UnityEngine;
using System.Collections;

public class CamZoom : MonoBehaviour {
	private bool inTrigger = false;
	public float smooth = 5f;
	public float zoomOut = 10f;
	public float zoomIn = 7f;
	private float startTime;
	private float yVelocity = 0.5F;
	CameraController controller;

	void Start() {
		startTime = Time.time;
		controller = Camera.main.GetComponent<CameraController>();
	}

	void OnTriggerEnter(Collider other) {
		startTime = Time.time;
	}

	void OnTriggerStay(Collider other) {
		inTrigger = true;
	}

	void OnTriggerExit(Collider other) {
		inTrigger = false;

	}

	void Update() {
		if(inTrigger) ZoomOut();
		else ZoomIn();

	}

	void ZoomIn() {
		controller.offset.y = 0;
		Camera.main.orthographicSize = zoomIn;
	}

	void ZoomOut() {
		controller.offset.y = 10;
		Camera.main.orthographicSize = zoomOut;
	}

}
