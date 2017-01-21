using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	Vector3 topPosition, midPosition, lowPosition;

	// Use this for initialization
	void Start () {
		topPosition = gameObject.transform.position;
		midPosition = new Vector3 (topPosition.x, topPosition.y - 200, topPosition.z);
		lowPosition = new Vector3 (topPosition.x, topPosition.y - 400, topPosition.z);
		gameObject.transform.RotateAround (gameObject.transform.position, new Vector3 (0, 1, 0), Random.Range(0,360));
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: implement bobbing
	}

	public void moveToLower() {
		gameObject.transform.position = lowPosition;
	}

	public void moveToMiddle() {
		gameObject.transform.position = midPosition;
	}

	public void moveToTop() {
		gameObject.transform.position = topPosition;
	}
}
