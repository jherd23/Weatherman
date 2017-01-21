using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherVane : Device {

	Day.windDirection dir;

	float seconds = 1;
	bool thing = true;

	// Use this for initialization
	void Start () {
		dir = Day.windDirection.N;
	}
	
	// Update is called once per frame
	void Update () {
		if (seconds > 0) {
			seconds -= Time.deltaTime;
		} else if(thing) {
			rotate (90);
			thing = false;
		}
	}

	public override void set(Day d) {
		reverse ();
		rotate (d.WindDirection);
		dir = d.WindDirection;
	}

	int directionToDegrees(Day.windDirection r) {
		switch (r) {
			case Day.windDirection.N:
				return 0;
			case Day.windDirection.NE:
				return 45;
			case Day.windDirection.E:
				return 90;
			case Day.windDirection.SE:
				return 135;
			case Day.windDirection.S:
				return 180;
			case Day.windDirection.SW:
				return 215;
			case Day.windDirection.W:
				return 270;
			case Day.windDirection.NW:
				return 315;
			default:
				return 0;
		}
	}

	void reverse () {
		rotate (-1 * directionToDegrees (dir));
	}

	void rotate (Day.windDirection r) {
		rotate (directionToDegrees (r));
	}
	void rotate (int degrees) {
		gameObject.transform.RotateAround (gameObject.transform.position,new Vector3(0,1),degrees);
	}
}
