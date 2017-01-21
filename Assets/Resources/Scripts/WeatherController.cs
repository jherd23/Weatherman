using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {

	public int daysPerSeason;

	public int numberOfSeasons;

	public Device[] devices; //contains devices available for each day

	public Day[] days; // contains weather conditions for each day

	public bool[][] predictions; // contains bools for prediction required by each day

	/* 1) Temperature (float)
	 * 2) Temperature Range
	 * 3) Anomaly (bool)
	 * 4) Pressure (int) NEVER USED
	 * 5) PressureRange
	 * 6) Cloudcover
	 * 7) Fog (bool)
	 * 8) Humidity (int)
	 * 9) Sky Color
	 * 10) Precipitation
	 * 11) Wind Type
	 * 12) Wind Speed (int)
	 * 13) Wind Direction
	 * 14) Sea State
	*/

	// Use this for initialization
	void Start () {
		Random rand = Random();
		float instability = 0;
		int numberOfSeasons = 8;
		int daysPerSeason = 12;
		int totDays = numberOfSeasons * daysPerSeason;
		int daysPerYear = 4 * daysPerSeason;

		//sinusoidal model
		float avgTemp = 55;
		float ampTemp = 30;
		float varianceTemp = 2;

		float avgPres  = 100;
		float ampPres = 3;

		float avgHum = 50;
		float ampHum = 45;

		float randomStagger = rand.Range(-Mathf.PI,Mathf.PI);
		float Temperature = 50;
		float Pressure = 100;



		//days = generateDays() //TODO: make that
		for (int i = -6; i < totDays; i++) {
			instability = (float)i * 10 / totDays;

			float old_temp = Temperature;
			Temperature = avgTemp + ampTemp * Mathf.sin((float) i * 2 * Mathf.PI / (float) daysPerYear) + (Mathf.PI / 8)) + (ampTemp / 10)*Mathf.sin((float) i * Mathf.PI / ((daysPerYear / 16)) + (randomStagger)) + rand.Range(-varianceTemp, varianceTemp);
			old_pressure = Pressure;
			Pressure = avgPres + ampPres * Mathf.sin((float) (i+2) * Mathf.PI / (daysPerYear / 8) + randomStagger) + (Temperature - old_temp) / (1440 / daysPerYear);
			Humidity = avgHum + 
				ampHum * Mathf.sin((float) (i+5) * Mathf.PI / (daysPerYear / 33) + randomStagger) + 
				30*Mathf.sin((float) (i-5) * Mathf.PI / ((daysPerYear / 64)) + (randomStagger)) + 
				((float) daysPerYear / 72)*Mathf.abs(old_temp - Temperature)*(old_temp - Temperature)) + 
				((float) daysPerYear / 36)*(old_pressure - Pressure)*(old_pressure - Pressure);

			if(Humidity < 0)
			{
				Humidity = 0;
			}
			if (Humidity > 85)
			{
				Precipitation = 0.5*Mathf.Exp((Pressure-old_pressure)/3) * (Humidity-85) / 5;
					if (Precipitation > 10)
					{
						Precipitation = 10;
					}
					if(Humidity > 100)
					{
						Humidity = 100;
					}
				if(Temperature <= 32){
			}
		}

		// manual set of all predictions (1 means ask, 0 means don't)
		//		predictions[0] = new bool[0,0,1,0,0, 0,0,0,0,0, 1,0,1,0]; //anomaly, wind type, wind direction
		predictions[0] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane
		predictions[1] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane
		predictions[2] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane
		predictions[3] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane
		predictions[4] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane
		predictions[5] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane

		predictions[6] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane, galilean thermometer
		predictions[7] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane, galilean thermometer
		predictions[8] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane, galilean thermometer
		predictions[9] = new bool[] {true,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, temperature -- barometer, wind vane, galilean thermometer
		predictions[10] = new bool[] {true,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, temperature -- barometer, wind vane, galilean thermometer
		predictions[11] = new bool[] {true,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, temperature -- barometer, wind vane, galilean thermometer

		predictions[12] = new bool[] {true,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind direction, wind type, temperature -- barometer, wind vane, galilean thermometer
		predictions[13] = new bool[] {true,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind direction, wind type, temperature -- barometer, wind vane, galilean thermometer
		predictions[14] = new bool[] {true,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind direction, wind type, temperature -- barometer, wind vane, galilean thermometer
		predictions[15] = new bool[] {true,false,true,false,false, false,false,false,false,false, false,true,true,false}; //anomaly, wind direction, wind speed, temperature -- barometer, wind vane, galilean thermometer
		predictions[16] = new bool[] {true,false,true,false,false, false,false,false,false,false, false,true,true,false}; //anomaly, wind direction, wind speed, temperature -- barometer, wind vane, galilean thermometer
		predictions[17] = new bool[] {true,false,true,false,false, false,false,false,false,false, false,true,true,false}; //anomaly, wind direction, wind speed, temperature -- barometer, wind vane, galilean thermometer

		predictions[18] = new bool[] {true,false,true,false,false, false,false,true,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature, humidity -- barometer, wind vane, galilean thermometer, cup anomometer
		predictions[19] = new bool[] {true,false,true,false,false, false,false,true,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature, humidity -- barometer, wind vane, galilean thermometer, cup anemometer
		predictions[20] = new bool[] {true,false,true,false,false, false,false,true,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature, humidity -- barometer, wind vane, galilean thermometer, cup anemometer
		predictions[21] = new bool[] {true,false,true,false,false, false,false,true,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature, humidity -- barometer, wind vane, galilean thermometer, cup anemometer
		predictions[22] = new bool[] {true,false,true,false,false, false,false,true,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature, humidity -- barometer, wind vane, galilean thermometer, cup anemometer
		predictions[23] = new bool[] {true,false,true,false,false, false,false,true,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature, humidity -- barometer, wind vane, galilean thermometer, cup anemometer

		predictions[24] = new bool[] {false,true,true,false,false, false,false,false,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		predictions[25] = new bool[] {false,true,true,false,false, false,false,false,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		predictions[26] = new bool[] {false,true,true,false,false, false,false,false,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		predictions[27] = new bool[] {false,true,true,false,false, false,false,false,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		predictions[28] = new bool[] {false,true,true,false,false, false,false,false,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		predictions[29] = new bool[] {false,true,true,false,false, false,false,false,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer

		predictions[30] = new bool[] {false,true,true,false,false, false,true,false,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		predictions[31] = new bool[] {false,true,true,false,false, false,true,false,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		predictions[32] = new bool[] {false,true,true,false,false, false,true,false,false,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		predictions[33] = new bool[] {false,true,true,false,false, false,true,false,true,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		predictions[34] = new bool[] {false,true,true,false,false, false,true,false,true,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		predictions[35] = new bool[] {false,true,true,false,false, false,true,false,true,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer
		//AFTER 'RAIN GAUGE' NEW INSTRUMENTS HAVE STOPPED BEING INTRODUCED
		predictions[36] = new bool[] {false,true,true,false,false, true,true,false,true,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[37] = new bool[] {false,true,true,false,false, true,true,false,true,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[38] = new bool[] {false,true,true,false,false, true,true,false,true,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[39] = new bool[] {false,true,true,false,false, true,true,false,true,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[40] = new bool[] {false,true,true,false,false, true,true,false,true,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[41] = new bool[] {false,true,true,false,false, true,true,false,true,false, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge

		predictions[42] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[43] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[44] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[45] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[46] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[47] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge

		predictions[48] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[49] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[50] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,false}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[51] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[52] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[53] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge

		predictions[54] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[55] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[56] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[57] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[58] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[59] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge

		predictions[60] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[61] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[62] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[63] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[64] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[65] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge

		predictions[66] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[67] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[68] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[69] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[70] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge
		predictions[71] = new bool[] {false,true,true,false,false, true,true,false,true,true, false,true,true,true}; //anomaly,  wind direction, wind speed, temperature range, fog, sky color, cloud cover, precipitation, sea state -- barometer, wind vane, galilean thermometer, cup anemometer, hygrometer, rain gauge

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