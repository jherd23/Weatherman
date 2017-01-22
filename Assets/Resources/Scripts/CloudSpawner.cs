using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

	List<GameObject> clouds;
	public string[] prefabList;

	public float maxScale;

	public float height;

	public float approxTiming,timingVariance;

	public Camera mainCamera;

	public WeatherController wc;

	float cooldown;

	// Use this for initialization
	void Start () {
		clouds = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		Day currentday = wc.days [wc.currentDay];
		cooldown -= Time.deltaTime;
		if (cooldown < 0) {
			int index = Random.Range (0, prefabList.Length);
			GameObject newCloud = GameObject.Instantiate(Resources.Load (prefabList [index]) as GameObject);
			clouds.Add (newCloud);
			Cloud cloudScript = newCloud.GetComponent<Cloud> ();
			cloudScript.speed = currentday.WindSpeed * (25 * Random.Range(0.9f,0.1f));
			if (directionToDegrees (currentday.WindDirection) >= 180) {
				cloudScript.speed *= -1;
				newCloud.transform.position = new Vector3 (-1000, height + Random.Range(0,100), -600);
			} else {
				newCloud.transform.position = new Vector3 (1000, height + Random.Range(0,100), -600);
			}
			float newScale = Random.Range (1, maxScale);
			newCloud.transform.localScale = newScale * newCloud.transform.localScale;
			cooldown = Random.Range (approxTiming - timingVariance, approxTiming + timingVariance);
		}
		//Cull naughty clouds
		for (int i = 0; i < clouds.Count; i++) {
			Vector3 cameraPosition = mainCamera.WorldToViewportPoint (clouds [i].transform.position);
			if (cameraPosition.x < 0 && directionToDegrees(currentday.WindDirection) <= 180) {
				GameObject cloud = clouds [i];
				clouds.RemoveAt (i);
				Destroy (cloud);
			} else if (cameraPosition.x > 1 && directionToDegrees(currentday.WindDirection) >= 180) {
				GameObject cloud = clouds[i];
				clouds.RemoveAt (i);
				Destroy (cloud);
			}
		}
	}

	float directionToDegrees(Day.windDirection r) {
		switch (r) {
		case Day.windDirection.W:
			return 0;
		case Day.windDirection.NW:
			return 45;
		case Day.windDirection.N:
			return 90;
		case Day.windDirection.NE:
			return 135;
		case Day.windDirection.E:
			return 180;
		case Day.windDirection.SE:
			return 215;
		case Day.windDirection.S:
			return 270;
		case Day.windDirection.SW:
			return 315;
		default:
			return 0;
		}
	}
}
