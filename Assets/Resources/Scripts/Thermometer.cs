using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermometer : Device {

	public Ball orange, green, blue, red;

	float sec;

	// Use this for initialization
	void Start () {
		sec = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (sec > 0) {
			sec -= Time.deltaTime;
		} else {
			set (new Day (52, 0, Day.pressureRange.high, Day.cloudCover.heavy, false, 0, Day.skyColor.blue, Day.precipitation.clear,Day.windType.breeze, 0, Day.seaState.calm, Day.temperatureRange.caution, Day.windDirection.E));
		}
	}

	public override void set(Day d) {
		
		int temperature = d.Temperature;
		if (temperature < 20) {
			orange.moveToTop ();
			green.moveToTop ();
			blue.moveToTop ();
			red.moveToTop ();
			if (Random.Range (0, 100) < 2 + (48f / 900) * (temperature + 10) * (temperature + 10)) {
				red.moveToMiddle ();
			}
		} else if (temperature < 45) {
			orange.moveToTop ();
			green.moveToTop ();
			blue.moveToTop ();
			red.moveToLower ();
			if (Random.Range (0, 100) < 2 + (48f / 156.25) * (temperature - 32.5) * (temperature - 32.5)) {
				if (temperature > 32.5) {
					blue.moveToMiddle ();
				} else {
					red.moveToMiddle ();
				}
			}
		} else if (temperature < 60) {
			orange.moveToTop ();
			green.moveToTop ();
			blue.moveToLower ();
			red.moveToLower ();
			if (Random.Range (0, 100) < 2 + (48f / 56.25) * (temperature - 52.5) * (temperature - 52.5)) {
				if (temperature > 52.5) {
					green.moveToMiddle ();
				} else {
					blue.moveToMiddle ();
				}
			}
		} else if (temperature < 80) {
			orange.moveToTop ();
			green.moveToLower ();
			blue.moveToLower ();
			red.moveToLower ();
			if (Random.Range (0, 100) < 2 + (48f / 100) * (temperature - 70) * (temperature - 70)) {
				if (temperature > 70) {
					orange.moveToMiddle ();
				} else {
					green.moveToMiddle ();
				}
			}
		} else {
			orange.moveToLower ();
			green.moveToLower ();
			blue.moveToLower ();
			red.moveToLower ();
			if (Random.Range (0, 100) < 2 + (48f / 400) * (temperature - 100) * (temperature - 100)) {
				orange.moveToMiddle ();
			}
		}
	}
}
