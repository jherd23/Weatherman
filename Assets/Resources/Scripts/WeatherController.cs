using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {

	public Device[] devices;

	public int daysPerSeason;

	public int numberOfSeasons;

	Day[] days;

	// Use this for initialization
	void Start () {
		//days = generateDays() //TODO: make that
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setInstruments(Day d) {
		for (int i = 0; i < devices.Length; i++) {
			if (d.season >= devices[i].unlockSeason) {
				devices [i].set (d);
			}
		}
	}
}
