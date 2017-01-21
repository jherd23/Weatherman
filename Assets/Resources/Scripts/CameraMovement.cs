using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private Vector3 velocity = new Vector3 (50, 0, 0);
	private float lerp = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnMouseDown() {
		Debug.Log ("PLZ");

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Debug.Log (Physics.Raycast (ray, out hit));
		if(Physics.Raycast (ray, out hit))
		{
			Focus (hit.transform.name);
		}

	}

	void Focus(string name) {
		GameObject wv = GameObject.Find (name);
		transform.position = Vector3.SmoothDamp( transform.position, new Vector3(wv.transform.position.x, transform.position.y, transform.position.z), ref velocity, 1.0f);
		float fov = Mathf.SmoothDamp(Camera.main.fieldOfView,20, ref lerp, 1.0f);
		Camera.main.fieldOfView = fov;
	}
}
