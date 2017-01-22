using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

	private Vector3 posVelocity = new Vector3 (50, 0, 0);
	private Vector3 rotVelocity = new Vector3 (1, 0, 0);
	private float fovVelocity = 0.2f;

	public Button b;

	private bool dir;
	private bool moving;
	private GameObject obj;
	private bool UI;

	private Vector3 targetRotation;
	private Vector3 initRotation;

	private Vector3 targetpos;
	private Vector3 initpos;

	private float targetFOV;
	private float initFOV;


	// Use this for initialization
	void Start () {
		initpos = transform.position;
		initFOV = Camera.main.fieldOfView;
<<<<<<< HEAD
		initRotation = transform.localEulerAngles;
=======

		resetView ();
>>>>>>> 7302a20748016cb54d3f257c24ed2f393b185930
	}



	// Update is called once per frame
	void Update() {

		if (targetpos == initpos && targetFOV == initFOV) {
			b.image.enabled = false;
			b.GetComponentInChildren<Text> ().enabled = false;
		} else {
			b.image.enabled = true;
			b.GetComponentInChildren<Text> ().enabled = true;
		}

		if (Input.GetMouseButtonDown(0)) {

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit) && (hit.transform.parent != null)) {
				obj = GameObject.Find(hit.transform.parent.name);
<<<<<<< HEAD
				if (obj.name == "rainGauge") {
					targetpos = new Vector3 (obj.transform.position.x, obj.transform.position.y + 500, transform.position.z);
				} else if (obj.name == "anemometer") {
					targetpos = new Vector3 (obj.transform.position.x, obj.transform.position.y + 400, transform.position.z);
				} else {
					targetpos = new Vector3 (obj.transform.position.x, obj.transform.position.y + 170, transform.position.z);
				}
				targetFOV = 18;
				if (transform.position.x > obj.transform.position.x) {
					dir = false;
				} else { 
					dir = true;
				}
				moving = true;
				UI = false;
				targetRotation = initRotation;
=======
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
>>>>>>> 7302a20748016cb54d3f257c24ed2f393b185930
			}
		}

		if (moving) {
			transform.position = Vector3.SmoothDamp (transform.position, targetpos, ref posVelocity, 2.0f);
			Camera.main.fieldOfView = Mathf.SmoothDamp (Camera.main.fieldOfView, targetFOV, ref fovVelocity, 2.0f);
			transform.localEulerAngles = Vector3.SmoothDamp (transform.localEulerAngles, targetRotation, ref rotVelocity, 2.0f);
		} 

	}

	public void resetView() {
		targetpos = initpos;
		targetFOV = initFOV;

	}

	public void focusUI(string name) {
		if (name == "stormglass note") {
			obj = GameObject.Find ("fitzroystormglass");
		} else if (name == "galilean note") {
			obj = GameObject.Find ("galilean");
		} else if (name == "cardinal1" || name == "cardinal2" || name == "cardinal3" || name == "cardinal4") {
			obj = GameObject.Find ("weatervane");
		}
//
//		if (obj.name == "rainGauge") {
//			targetpos = new Vector3 (obj.transform.position.x, obj.transform.position.y + 500, transform.position.z);
//		} else if (obj.name == "anemometer") {
//			targetpos = new Vector3 (obj.transform.position.x, obj.transform.position.y + 400, transform.position.z);
//		} else {
			targetpos = new Vector3 (obj.transform.position.x, obj.transform.position.y + 170, transform.position.z);
//		}


		targetFOV = 5;
		targetRotation = new Vector3 (transform.localEulerAngles.x + 5.0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
		UI = true;
		moving = true;
	}

}
