using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainGauge : Device {

	public GameObject arrow;
	public GameObject center;

	float inches;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void set (Day d) {
		rotateArrow (-1 * inches * 10);
		switch (d.Precipitation) {
		case Day.precipitation.rain:
			inches = Random.Range (1f, 4f);
			break;
		case Day.precipitation.storm:
			inches = Random.Range (4f, 7f);
			break;
		case Day.precipitation.typhoon:
			inches = 8;
			break;
		default:
			inches = 0;
			break;
		}
		rotateArrow (inches * 10);
	}

	void rotateArrow (float degrees) {
		arrow.transform.RotateAround (center.transform.position, new Vector3 (0, 0, 1), -1 * degrees);
	}
}
