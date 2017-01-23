using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

	List<GameObject> clouds;

	public GameObject[] cirrusClouds;
	public GameObject altoCumulus;
	public string[] partlyCloudyPrefabs;
	public string[] overcastPrefabs;

	public float maxScale;

	public float height;

	public float approxTiming,timingVariance;

	public Camera mainCamera;

	public WeatherController wc;

    public GameObject Rainer;

	float cooldown;

	Day currentday;

	// Use this for initialization
	void Start () {
		clouds = new List<GameObject> ();
		float rand = Random.Range (0f, 1f);
		if (rand < 0.3f) {
			cirrusClouds [Random.Range (0, cirrusClouds.Length)].SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (currentday == null) {
			return;
		}

        if(directionToDegrees(currentday.WindDirection) >= 180)
        {
            Rainer.transform.rotation = new Quaternion(Rainer.transform.rotation.x, 180, Rainer.transform.rotation.z, Rainer.transform.rotation.w);
        } else
        {
            Rainer.transform.rotation = new Quaternion(Rainer.transform.rotation.x, 0, Rainer.transform.rotation.z, Rainer.transform.rotation.w);
        }

		cooldown -= Time.deltaTime;
		if (cooldown < 0) {
			if (currentday.Cloudcover == Day.cloudCover.partly_cloudy) {
				GameObject newCloud = GameObject.Instantiate (Resources.Load (partlyCloudyPrefabs [Random.Range (0, partlyCloudyPrefabs.Length)]) as GameObject);
				newCloud.GetComponent<Cloud> ().speed = currentday.WindSpeed * 30 * (directionToDegrees (currentday.WindDirection) >= 180 ? -1 : 1);
				newCloud.transform.position = new Vector3 (directionToDegrees (currentday.WindDirection) >= 180 ? -1100 : 1100, height + Random.Range (0f, 150f), -600);
				newCloud.transform.localScale = Random.Range (1, maxScale) * newCloud.transform.localScale;
				clouds.Add (newCloud);
			} else if (currentday.Cloudcover == Day.cloudCover.overcast) {
				GameObject newCloud = GameObject.Instantiate (Resources.Load (overcastPrefabs [Random.Range (0, overcastPrefabs.Length)]) as GameObject);
				newCloud.GetComponent<Cloud> ().speed = currentday.WindSpeed * 30 * (directionToDegrees (currentday.WindDirection) >= 180 ? -1 : 1);
				newCloud.transform.position = new Vector3 (directionToDegrees (currentday.WindDirection) >= 180 ? -1100 : 1100, height + Random.Range (0f, 150f), -600);
				newCloud.transform.localScale = Random.Range (1, maxScale) * newCloud.transform.localScale;
				clouds.Add (newCloud);
			}
			cooldown = Random.Range (approxTiming - timingVariance, approxTiming + timingVariance);
		}

		for (int i = 0; i < clouds.Count; i++) {
			if (clouds [i].transform.position.x > 1100 && directionToDegrees (currentday.WindDirection) >= 180) {
				GameObject temp = clouds [i];
				clouds.RemoveAt (i);
				Destroy (temp);
			} else if (clouds[i].transform.position.x < -1100 && directionToDegrees(currentday.WindDirection) < 180) {
               
                GameObject temp = clouds [i];
				clouds.RemoveAt (i);
				Destroy (temp);
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

	public void set (Day d) {
		reset ();
		currentday = d;
		switch (d.Cloudcover) {
		case Day.cloudCover.sunny:
			float rand = Random.Range (0f, 1f);
			if (rand > 0.3f) {
				cirrusClouds [Random.Range (0, cirrusClouds.Length)].SetActive (true);
			}
			break;
		case Day.cloudCover.partly_cloudy:
			int random = Random.Range (0, cirrusClouds.Length + 1);
			if (random == cirrusClouds.Length) {
				altoCumulus.SetActive (true);
			} else {
				cirrusClouds [random].SetActive (true);
			}
			approxTiming = 8f / d.WindSpeed;
			timingVariance = .6f;
			break;
		case Day.cloudCover.overcast:
			altoCumulus.SetActive (true);
			approxTiming = 3f / d.WindSpeed;
			timingVariance = .3f;
			break;
		}
	}

	void reset () {
		for (int i = 0; i < clouds.Count; i++) {
			Destroy (clouds [i]);
		}
		clouds.Clear ();
		for (int i = 0; i < cirrusClouds.Length; i++) {
			cirrusClouds [i].SetActive (false);
		}
		altoCumulus.SetActive (false);
	}
}
