using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {

	public int daysPerSeason;

	public int numberOfSeasons;

	public Device[] devices; //contains devices available for each day

	public Day[] days; // contains weather conditions for each day

	public bool[][] predictions; // contains bools for prediction required by each day
	/* 1) Temperature (int)
	 * 2) Temperature Range
	 * 3) Anomaly (bool)
	 * 4) Pressure (int)
	 * 5) PressureRange
	 * 6) Cloudcover
	 * 7) Fog (bool)
	 * 8) Humidity
	 * 9) Sky Color
	 * 10) Precipitation
	 * 11) Wind Type
	 * 12) Wind Speed (int)
	 * 13) Wind Direction
	 * 14) Sea State
	*/

	// Use this for initialization
	void Start () {
		numberOfSeasons = 12;
		daysPerSeason = 6;

		//days = generateDays() //TODO: make that
		for (int i = 0; i < numberOfSeasons * daysPerSeason; i++) {
			//days [i] = Day ();
		}

		// manual set of all predictions (1 means ask, 0 means don't)
//		predictions[0] = new bool[0,0,1,0,0, 0,0,0,0,0, 1,0,1,0]; //anomaly, wind type, wind direction


	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void setInstruments(Day d) {
//		for (int i = 0; i < devices.Length; i++) {
//			if (d.season >= devices[i].unlockSeason) {
//				devices [i].set (d);
//			}
//		}
//	}

}
