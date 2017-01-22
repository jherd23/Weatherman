using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private Vector3 posVelocity = new Vector3 (50, 0, 0);
	private float fovVelocity = 0.2f;

	private bool dir;
	private bool moving;
	private GameObject obj;

	private Vector3 targetpos;
	private Vector3 initpos;

	private float targetFOV;
	private float initFOV;


	// Use this for initialization
	void Start () {
		initpos = transform.position;
		initFOV = Camera.main.fieldOfView;
	}

	// Update is called once per frame
	void Update() {

		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit) && (hit.transform.parent != null)) {
				obj = GameObject.Find(hit.transform.parent.name);
				if (obj.name == "rainGauge" || obj.name == "anemometer") {
					targetpos = new Vector3 (obj.transform.position.x, obj.transform.position.y + 500, transform.position.z);
					targetFOV = initFOV;
				} else {
					targetpos = new Vector3 (obj.transform.position.x, obj.transform.position.y + 200, transform.position.z);
					targetFOV = 18;
				}
//				if (obj.CompareTag ("clickable")) {
					if (transform.position.x > obj.transform.position.x) {
						dir = false;
					} else { 
						dir = true;
					}
					moving = true;
//				}
			}
		}

		if (moving) {
			transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref posVelocity, 2.0f);
			Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView, targetFOV, ref fovVelocity, 2.0f);
		}

	}

	public void resetView() {
		targetpos = initpos;
		targetFOV = initFOV;
	}

}
