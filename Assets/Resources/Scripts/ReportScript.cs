using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReportScript : MonoBehaviour {

	private bool[] Predictions;

	public int wellbeing = 100;

	public GameObject WindStrength;
	public GameObject Temperature;
	public GameObject Humidity;
	public GameObject Pressure;

	public GameObject WindType;
	public GameObject HeatLevel;
	public GameObject SeaState;
	public GameObject Precipitation;
	public GameObject CloudCover;
	public GameObject SkyColor;
	public GameObject PressureLevels;

	public GameObject FogToggle;
	public GameObject DisasterToggle;

	public GameObject WindDirection;
	public WeatherController WC;

	public Button Submit;


	private GameObject[] formElements;

	private Day today;

	public int wellbeingMod;

    public int notice = 0;

	bool haveMadeVariables = false;

	int ourDay = 0;
	public float ourTemp;
	public int ourTempRange;
	public bool ourAnomaly;
	public float ourPressure;
	public int ourPressureLevels;
	public int ourCloudCover;
	public bool ourFoggle;
	public float ourHumidity;
	public int ourSkyColor;
	public int ourPrecipitation;
	public int ourWindType;
	public float ourWindStrength;
	public float ourWindDirection;
	public int ourSeaState;



	// Use this for initialization
	void Start () {
		// temperature, temperature range, anomaly, pressure, pressure range, cloud cover, fog, humidity, sky color, precipitation, wind type, wind speed, wind direction, sea state
		formElements = new GameObject[] { Temperature, HeatLevel, DisasterToggle, Pressure, PressureLevels, CloudCover, FogToggle, Humidity, SkyColor,
			Precipitation, WindType, WindStrength, WindDirection, SeaState};

		Predictions = new bool[] { false, true, true, true, true, false, false, false, true, true, true, true, true, true };

		Button btn = Submit.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

		MakeForm(Predictions);
	}

	void Update()
	{
		if(WC.currentDay != ourDay)
		{
			haveMadeVariables = false;
			ourDay = WC.currentDay;
		}
		if(!(haveMadeVariables))
		{
			ourTemp = WC.days[ourDay].Temperature;
			ourTempRange = (int) WC.days[ourDay].Temprange;
			ourAnomaly = WC.days[ourDay].Anomaly;
			ourPressure = WC.days[ourDay].Pressure;
			ourPressureLevels = (int)WC.days[ourDay].PressureRange;
			ourCloudCover = (int)WC.days[ourDay].Cloudcover;
			ourFoggle = WC.days[ourDay].Fog;
			ourHumidity = WC.days[ourDay].Humidity;
			ourSkyColor = (int)WC.days[ourDay].Skycolor;
			ourPrecipitation = (int)WC.days[ourDay].Precipitation;
			ourWindType = (int)WC.days[ourDay].Windtype;
			ourWindStrength = WC.days[ourDay].WindSpeed;
			ourWindDirection = (float)WC.days[ourDay].WindDirection;
			ourSeaState = (int)WC.days[ourDay].Seastate;

		}
	}

	void MakeForm(bool[] Predictions) {
		for (int i = 0; i < 14; i++) {
			//Debug.Log(i);
			formElements[i].SetActive(Predictions[i]);
			//Debug.Log(Predictions[i]);
		}
	}

	void TaskOnClick() {
		Verify();
	}

	void UpdateDay(Day newDay) {
		WC.incrementDay();
		today = newDay;
	}

	public int Verify() {
        
		wellbeingMod = 0;
        
        if (Predictions[0]) {
			InputField tempInput = Temperature.GetComponentInChildren<InputField>();
			Text tempText = tempInput.textComponent;
            int r = -1000;
            int.TryParse(tempText.text, out r);
            if (r == -1000 || String.IsNullOrEmpty(tempText.text))
            {
                r = 50;
            }
            wellbeingMod -= (int) (( Mathf.Abs(ourTemp - r) - 10.0f)/1.5f);
        }

		if (Predictions[1]) {
			Dropdown tempRange = HeatLevel.GetComponentInChildren<Dropdown>();
            //Debug.Log(tempRange.value);
			int choice = tempRange.value;
			wellbeingMod -= (int)(Mathf.Abs(ourTempRange - choice) - 3) * 2;
		}

		if (Predictions[2]) {
			Toggle disasterToggle = DisasterToggle.GetComponent<Toggle>();
			bool choice = disasterToggle.isOn;
			if (choice == ourAnomaly)
			{
				wellbeingMod += 2;
			}
			else {
				wellbeingMod -= 40;
			}
		}

		if (Predictions[3]) {
			InputField pressInput = Pressure.GetComponentInChildren<InputField>();
			Text pressText = pressInput.textComponent;
            int r = -1000;
            int.TryParse(pressText.text, out r);
            if (r == -1000 || String.IsNullOrEmpty(pressText.text))
            {
                r = 5;
            }
            wellbeingMod -= (int)(Math.Abs(ourPressure - (r))-4)*2;
        }

		if (Predictions[4])
		{
			Dropdown pressRange = PressureLevels.GetComponentInChildren<Dropdown>();
			int choice = pressRange.value;
			wellbeingMod -= (int)(Mathf.Abs(ourPressureLevels - choice) - 1) * 5;
		}

		if (Predictions[5])
		{
			Dropdown cloudRange = CloudCover.GetComponentInChildren<Dropdown>();
			int choice = cloudRange.value;
			wellbeingMod -= (int)(Mathf.Abs(ourCloudCover - choice) - 1) * 3;
		}

		if (Predictions[6])
		{
			Toggle fogToggle = DisasterToggle.GetComponentInChildren<Toggle>();
			bool choice = fogToggle.isOn;
			if (choice == ourFoggle)
			{
				wellbeingMod += 5;
			}
			else {
				wellbeingMod -= 10;
			}
		}

		if (Predictions[7]) {
			InputField humidInput = Humidity.GetComponentInChildren<InputField>();
			Text humidText = humidInput.textComponent;
            int r = -1000;
            int.TryParse(humidText.text, out r);
            if (r == -1000 || String.IsNullOrEmpty(humidText.text))
            {
                r = 45;
            }
            wellbeingMod -= (int) ((Math.Abs(ourHumidity -r) - 10)/1.5f);
        }

		if (Predictions[8]) {
			Dropdown skyRange = SkyColor.GetComponentInChildren<Dropdown>();
			int choice = skyRange.value;
			wellbeingMod -= (int)(Mathf.Abs(ourSkyColor - choice) - 1) * 3;
		}

		if (Predictions[9])
		{
			Dropdown precip = Precipitation.GetComponentInChildren<Dropdown>();
			int choice = precip.value;
			wellbeingMod -= (int)(Mathf.Abs(ourPrecipitation - choice) - 1) * 15;
		}

		if (Predictions[10]) {
			Dropdown windT = WindType.GetComponentInChildren<Dropdown>();
			int choice = windT.value;
			wellbeingMod -= (int)(Mathf.Abs(ourWindType - choice) - 3) * 5;
		}

		if (Predictions[11])
		{
			InputField windInput = WindStrength.GetComponentInChildren<InputField>();
			Text windText = windInput.textComponent;
            int r = -1000;
            int.TryParse(windText.text, out r);
            if (r == -1000 || String.IsNullOrEmpty(windText.text))
            {
                r = 10;
            }
            wellbeingMod -= (int)((Math.Abs(ourWindStrength - r) - 5.0f) * 4.0f);
            //notice = r;
        }

		/*if (Predictions[12]) {
			ToggleGroup windDT = WindDirection.GetComponentInChildren<ToggleGroup>();
			int choice = windDT.;
            int dirDist = Mathf.Min(((8 - Mathf.Max(ourWindDirection, choice)) + (Mathf.Min(ourWindDirection, choice))), Mathf.Abs(ourWindDirection - choice));
            wellbeingMod -= (int)(dirDist * 2);

		}*/

		if (Predictions[13]) {
			Dropdown seaT = SeaState.GetComponentInChildren<Dropdown>();
			int choice = seaT.value;
			wellbeingMod -= (int)(Mathf.Abs(ourSeaState - choice) - 3) * 7;
		}

        if(wellbeingMod < 0)
        {
            wellbeingMod /= 8;

        }

        Debug.Log("wellbeingMod: " + wellbeingMod.ToString());

        return wellbeingMod;
	}
}