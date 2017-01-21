using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {

	public int daysPerSeason;

	public int numberOfSeasons;

	public Device[] devices; //contains devices available for each day

	public Day[] days; // contains weather conditions for each day

	public bool[][] predictions; // contains bools for prediction required by each day
	/* 1) Temperature
	 * 2) Temperature Range
	 * 3) Anomaly
	 * 4) Pressure
	 * 5) PressureRange
	 * 6) Cloudcover
	 * 7) Humidity
	 * 8) Sky Color
	 * 9) Precipitation
	 * 10) Wind Type
	 * 11) Wind Speed
	 * 12) Wind Direction
	 * 13) Sea State
	 * 
	*/

	// Use this for initialization
	void Start () {
		numberOfSeasons = 12;
		daysPerSeason = 6;

		//days = generateDays() //TODO: make that
		for (int i = 0; i < numberOfSeasons + daysPerSeason; i++) {
			//days [i] = Day ();
		}

		// manual set of all predictions
		//predictions[0] = ;


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
