using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {

	public int daysPerSeason;

	public int numberOfSeasons;

	public Device[] devices; //contains devices available for each day

	public Day[] days; // contains weather conditions for each day

	public bool[][] predictions;

	public WindowOpener win;

	public int currentDay = 0;
	// contains bools for prediction required by each day

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
		days = new Day[daysPerSeason * numberOfSeasons];
		predictions = new bool[72][];
		Day.cloudCover c;
		Day.skyColor sc;
		Day.precipitation prc;
		bool fog = false;
		Day.windType wt;
		Day.seaState st;
		Day.windDirection wd;
		Day.pressureRange p;
		Day.temperatureRange tr;
		float instability = 0;
		float tempSubtractFromNext = 0;
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

		float randomStagger = Random.Range(-Mathf.PI,Mathf.PI);
		float Temperature = 50;
		float Pressure = 100;
		float Humidity = 70;

		float windSpeed = 0;
		float windDirection = 0;
		int windSign = 1;
		float cloudThickness = 0;
	
		for (int i = 0; i < totDays; i++) {
			//wesley's math
			instability = (float)i * 10 / totDays;
			float old_temp = Temperature;
			Temperature = avgTemp + ampTemp * Mathf.Sin(((float) i * 2 * Mathf.PI / (float) daysPerYear) + (Mathf.PI / 8)) + (ampTemp / 10)*Mathf.Sin((float) i * Mathf.PI / ((daysPerYear / 16)) + (randomStagger)) + Random.Range(-varianceTemp, varianceTemp);
			Temperature -= tempSubtractFromNext;
			float old_pressure = Pressure;
			Pressure = avgPres + ampPres * Mathf.Sin((float) (i+2) * Mathf.PI / (daysPerYear / 8f) + randomStagger) + (Temperature - old_temp) / (1440 / daysPerYear);
			float Precipitation = 0.5f*Mathf.Exp((Pressure-old_pressure)/3) * (Humidity-85) / 5;
			windSpeed = Mathf.Pow(Mathf.Abs((Pressure - old_pressure) / 3 * 20), 1.2f) * (instability / 10);
			windSign = (int) Random.Range(-1,1);
			if (windSign == 0)
			{
				windSign = 1;
			}
			windDirection = Mathf.Exp((old_pressure - Pressure)*5)*windSign;
			if(windDirection > 180)
			{
				windDirection = 180;
			}
			else if(windDirection < -180)
			{
				windDirection = -180;
			}
			Humidity = 
				avgHum + ampHum * Mathf.Sin ((float)(i + 5) * Mathf.PI / (daysPerYear / 33f) + randomStagger) +
				30 * Mathf.Sin ((float)(i - 5) * Mathf.PI / (daysPerYear / 64f) + randomStagger) +
				(((float)daysPerYear / 72) * Mathf.Abs (old_temp - Temperature) * (old_temp - Temperature)) +
				((float)daysPerYear / 36) * (old_pressure - Pressure) * (old_pressure - Pressure) +
				Mathf.Cos(windDirection*Mathf.PI / 180f)*windSpeed;
			float heatIndex = Temperature+(Humidity-50) / 5 - windSpeed / 2;
			float cloudAltitude = Pressure * Mathf.Log10(Mathf.Abs(Humidity))/200;
			if (cloudAltitude > 1)
			{
				cloudAltitude = 1;
			}
			else if (cloudAltitude < 0)
			{
				cloudAltitude = 0;
			}
			old_pressure = Pressure;
			if(Humidity < 0)
			{
				Humidity = 0;
			}
			if (Humidity > 85)
			{
				c = Day.cloudCover.overcast;
				float dailyPrecip = 0.5f * Mathf.Exp((old_pressure-Pressure) / 3) * (Humidity-85)/5;
				if (dailyPrecip > 10)
				{
					cloudThickness = 1;
					dailyPrecip = 10;
				}
				Precipitation += dailyPrecip;
				if(Humidity > 100)
				{
					Humidity = 100;
				}
				if(Temperature < 35)
				{
					sc = Day.skyColor.white;
					if (Pressure < 97.4)
					{
						cloudThickness = 1;
						prc = Day.precipitation.blizzard;
					}
					else{
						cloudThickness = 0.8f;
						prc = Day.precipitation.snow;
					}
				}
				else if (Pressure < 97){
					sc = Day.skyColor.grey;
					cloudThickness = 1;
					prc = Day.precipitation.typhoon;
				}
				else if (Pressure < 98)
				{
					sc = Day.skyColor.grey;
					cloudThickness = 0.9f;
					prc = Day.precipitation.storm;
				}
				else{
					sc = Day.skyColor.grey;
					cloudThickness = 0.8f;
					prc = Day.precipitation.rain;
				}
			}
			else if (Humidity > 60)
			{
				c = Day.cloudCover.partly_cloudy;
				sc = Day.skyColor.blue;
				cloudThickness = (Humidity - 10) / 100;
				prc = Day.precipitation.none;
			}
			else{
				c = Day.cloudCover.sunny;
				cloudThickness = (Humidity - 20) / 150;
				sc = Day.skyColor.blue;
				if (cloudThickness < 0){
					cloudThickness = 0;
				}
				prc = Day.precipitation.none;
			}


			tempSubtractFromNext -= cloudThickness * 10;


			//Day(Temperature, Pressure, pressureRange p, cloudCover c, bool fog, float h, skyColor sc, precipitation prc, windType wt, float ws, 
				//seaState st,tr,wd)

			if (windSpeed < 4 &&  (prc == Day.precipitation.rain || prc == Day.precipitation.storm))
			{
				fog = true;
			}
			// set wind type
			if(windSpeed == 0)
			{
				wt = Day.windType.calm;
				st = Day.seaState.calm;
			}
			else if(windSpeed < 2)
			{
				wt = Day.windType.breeze;
				st = Day.seaState.smooth;
			}
			else if(windSpeed < 6)
			{
				wt = Day.windType.strong_breeze;
				st = Day.seaState.slight;
			}
			else if(windSpeed < 10)
			{
				wt = Day.windType.moderate;
				st = Day.seaState.moderate;
			}
			else if(windSpeed < 14)
			{
				wt = Day.windType.gale;
				st = Day.seaState.rough;
			}
			else if(windSpeed < 18)
			{
				wt = Day.windType.storm;
				st = Day.seaState.very_rough;
			}
			else
			{
				wt = Day.windType.hurricane;
				st = Day.seaState.phenomenal;
			}

			windDirection += 180;
			//set cardinal direction
			if (windDirection <= 22.5f) {
				wd = Day.windDirection.N;
			} else if (windDirection <= 67.5f) {
				wd = Day.windDirection.NE;
			} else if (windDirection <= 112.5f) {
				wd = Day.windDirection.E;
			} else if (windDirection <= 157.5f) {
				wd = Day.windDirection.SE;
			} else if (windDirection <= 202.5f) {
				wd = Day.windDirection.S;
			} else if (windDirection <= 247.5f) {
				wd = Day.windDirection.SW;
			} else if (windDirection <= 292.5f) {
				wd = Day.windDirection.W;
			} else if (windDirection <= 337.5f) {
				wd = Day.windDirection.NW;
			} else {
				wd = Day.windDirection.N;
			}
			//set pressure range
			if(Pressure < 98.3){
				p = Day.pressureRange.low;
			}
			else if(Pressure < 101.6){
				p = Day.pressureRange.moderate;
			}
			else{
				p = Day.pressureRange.high;
			}
			//set temperature range
			if(Temperature < 33){
				tr = Day.temperatureRange.freezing;
			}
			else if(Temperature < 50){
				tr = Day.temperatureRange.cold;
			}
			else if(Temperature < 65){
				tr = Day.temperatureRange.tepid;
			}
			else if(Temperature < 77){
				tr = Day.temperatureRange.warm;
			}
			else if(Temperature < 92){
				tr = Day.temperatureRange.hot;
			}
			else{
				tr = Day.temperatureRange.boiling;
			}
			/*Debug.Log (i);
			Debug.Log (Temperature);
			Debug.Log (Pressure);
			Debug.Log (p);
			Debug.Log (c);
			Debug.Log (fog);
			Debug.Log (Humidity);// NaN
			Debug.Log (sc);
			Debug.Log (prc);
			Debug.Log (wt);
			Debug.Log (windSpeed);
			Debug.Log (st);
			Debug.Log (tr);
			Debug.Log (wd);*/
			days [i] = new Day (i, Temperature, Pressure, p, c, fog, Humidity, sc, prc, wt, windSpeed, st, tr, wd); 
		}

		// manual set of all predictions (1 means ask, 0 means don't)
		//		predictions[0] = new bool[0,0,1,0,0, 0,0,0,0,0, 1,0,1,0]; //anomaly, wind type, wind direction
		predictions[0] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane
		predictions[1] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane
		predictions[2] = new bool[] {false,false,true,false,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction -- barometer, wind vane
		predictions[3] = new bool[] {false,false,true,false,true, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, pressure range -- barometer, wind vane
		predictions[4] = new bool[] {false,false,true,false,true, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, pressure range -- barometer, wind vane
		predictions[5] = new bool[] {false,false,true,false,true, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, pressure range -- barometer, wind vane

		predictions[6] = new bool[] {false,false,true,false,true, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, pressure range -- barometer, wind vane, galilean thermometer
		predictions[7] = new bool[] {false,false,true,false,true, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, pressure range -- barometer, wind vane, galilean thermometer
		predictions[8] = new bool[] {false,false,true,false,true, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, pressure range -- barometer, wind vane, galilean thermometer
		predictions[9] = new bool[] {true,false,true,false,true, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, pressure range, temperature -- barometer, wind vane, galilean thermometer
		predictions[10] = new bool[] {true,false,true,false,true, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, pressure range, temperature -- barometer, wind vane, galilean thermometer
		predictions[11] = new bool[] {true,false,true,false,true, false,false,false,false,false, true,false,true,false}; //anomaly, wind type, wind direction, pressure range, temperature -- barometer, wind vane, galilean thermometer

		predictions[12] = new bool[] {true,false,true,true,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind direction, wind type, pressure, temperature -- barometer, wind vane, galilean thermometer
		predictions[13] = new bool[] {true,false,true,true,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind direction, wind type, pressure, temperature -- barometer, wind vane, galilean thermometer
		predictions[14] = new bool[] {true,false,true,true,false, false,false,false,false,false, true,false,true,false}; //anomaly, wind direction, wind type, pressure, temperature -- barometer, wind vane, galilean thermometer
		predictions[15] = new bool[] {true,false,true,true,false, false,false,false,false,false, false,true,true,false}; //anomaly, wind direction, wind speed, pressure, temperature -- barometer, wind vane, galilean thermometer
		predictions[16] = new bool[] {true,false,true,true,false, false,false,false,false,false, false,true,true,false}; //anomaly, wind direction, wind speed, pressure, temperature -- barometer, wind vane, galilean thermometer
		predictions[17] = new bool[] {true,false,true,true,false, false,false,false,false,false, false,true,true,false}; //anomaly, wind direction, wind speed, pressure, temperature -- barometer, wind vane, galilean thermometer

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

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			incrementDay ();
		}
	}
	void setInstruments(Day d) {
		for (int i = 0; i < devices.Length; i++) {
			if (d.season >= devices[i].unlockSeason) {
				devices [i].set (d);
				}
			}
		}
		
	void incrementDay(){
		currentDay++;
		win.setExterior (days [currentDay]);
		printDay (days [currentDay]);
		setInstruments (days[currentDay]);
	}

	void printDay(Day d) {
		Debug.Log (d.Index);
		Debug.Log (d.season);
		Debug.Log (d.Temperature);
		Debug.Log (d.Temprange);
		Debug.Log (d.Anomaly);
		Debug.Log (d.Pressure);
		Debug.Log (d.PressureRange);
		Debug.Log (d.Cloudcover);
		Debug.Log (d.Fog);
		Debug.Log (d.Humidity);
		Debug.Log (d.Skycolor);
		Debug.Log (d.Precipitation);
		Debug.Log (d.Windtype);
		Debug.Log (d.WindSpeed);
		Debug.Log (d.Seastate);
		Debug.Log (d.WindDirection);
	}
}