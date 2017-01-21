using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherVane : Device {

	float targetDirection;
	float currentDirection;

	float velocity;
	float maxVelocity;

	public Slider slider;

	// Use this for initialization
	void Start () {
		targetDirection = 90;
		velocity = 0;
		maxVelocity = 90;
		currentDirection = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (slider.value > 700) {
			float del = targetDirection - currentDirection;
			float acceleration = 2f * del;
			velocity += acceleration * Time.deltaTime;
			if (velocity > maxVelocity) {
				velocity = maxVelocity;
			} else if (velocity < -1 * maxVelocity) {
				velocity = -1 * maxVelocity;
			}
		}
		velocity *= 0.99f;
		spin (velocity * Time.deltaTime);
	}

	public override void set(Day d) {
		rotateTo (d.WindDirection);
	}

	float directionToDegrees(Day.windDirection r) {
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
		
	void rotateTo (Day.windDirection r) {
		rotateTo (directionToDegrees (r));
	}

	void rotateTo (float degrees) {
		targetDirection = degrees;
	}

	void rotateBy (float degrees) {
		targetDirection += degrees;
	}

	void spin (float degrees) {
		gameObject.transform.RotateAround (gameObject.transform.position, new Vector3 (0, 1, 0), degrees);
		currentDirection = (currentDirection + degrees) % 360;
	}
}
