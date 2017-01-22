using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anemometer : Device {

	float targetSpeed;
	float windSpeed;
	float speed;
	float speedDelta;

	public GameObject spinner;
	public Slider slider;


	// Use this for initialization
	void Start () {
		speed = 0;
		targetSpeed = 0;
		speedDelta = 45;
	}
	
	// Update is called once per frame
	void Update () {
		if (slider.value > 700) {
			targetSpeed = windSpeed;
		} else {
			targetSpeed = 0;
		}
		if (speed > targetSpeed) {
			speed = Mathf.Max (targetSpeed, speed - (speedDelta * Time.deltaTime));
		} else {
			speed = Mathf.Min (targetSpeed, speed + (speedDelta * Time.deltaTime));
		}
		spin (speed * Time.deltaTime);
	}

	public override void set (Day d) {
		windSpeed = d.WindSpeed * 72;
	}

	void spin(float degrees) {
		spinner.transform.RotateAround (spinner.transform.position, new Vector3 (0, 1, 0), degrees);
	}
}
