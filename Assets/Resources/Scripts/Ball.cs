using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	Vector3 topPosition, midPosition, lowPosition;

	float sec;

	// Use this for initialization
	void Start () {
		topPosition = gameObject.transform.position;
		midPosition = new Vector3 (topPosition.x, topPosition.y - 150, topPosition.z);
		lowPosition = new Vector3 (topPosition.x, topPosition.y - 300, topPosition.z);
		sec = Random.Range(0,2 * Mathf.PI);
	}
	
	// Update is called once per frame
	void Update () {
		sec += Time.deltaTime;
		if (sec > 2 * Mathf.PI) {
			sec -= 2 * Mathf.PI;
		}
		gameObject.transform.Translate (new Vector3 (0, 0.07f * (float)Mathf.Sin (sec), 0));
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
