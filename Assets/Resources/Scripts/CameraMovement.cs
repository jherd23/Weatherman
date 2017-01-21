using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private Vector3 velocity = new Vector3 (50, 0, 0);
	private float lerp = 5.0f;
	private bool dir;
	private bool moving;
	private GameObject obj;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update() {

		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				obj = GameObject.Find(hit.transform.parent.name);
				if (transform.position.x > obj.transform.position.x) {
					dir = false;
				} else { 
					dir = true;
				}
				moving = true;
			}
		}

		if (moving) {
			transform.position = Vector3.SmoothDamp(transform.position, new Vector3 (obj.transform.position.x, obj.transform.position.y, transform.position.z), ref velocity, 2.0f);

			//	float fov = Mathf.Lerp(Camera.main.fieldOfView, 20, 100.0f);
			if (Camera.main.fieldOfView > 18) {
				Camera.main.fieldOfView -= 0.2f;
			} 
		}

	}


//
//	void Focus(string name) {
//		GameObject obj = GameObject.Find(name);
//		
//		float fov = Mathf.Lerp(Camera.main.fieldOfView, 20, 100.0f);
////		Camera.main.fieldOfView = fov;
//
//		Debug.Log (transform.position);
//		Debug.Log (Camera.main.fieldOfView);
//	}

}
