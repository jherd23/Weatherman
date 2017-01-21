using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherVane : Device {

	float dir;
	float lastdir;

	// Use this for initialization
	void Start () {
		dir = 0;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.RotateAround (gameObject.transform.position,new Vector3(0,1),dir - lastdir);
		lastdir = dir;
	}

	override void set(Day d) {
		
	}
}
