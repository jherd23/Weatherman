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

    public Button Submit;


    private GameObject[] formElements;

    private Day today;


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

    void MakeForm(bool[] Predictions) {
        for (int i = 0; i < 14; i++) {
            //Debug.Log(i);
            formElements[i].SetActive(Predictions[i]);
            //Debug.Log(Predictions[i]);
        }
    }

    void TaskOnClick() {
        Debug.Log("Button Click");
        MakeForm(Predictions);
    }

    void UpdateDay(Day newDay) {
        today = newDay;
    }

    void Verify() {
        if (Predictions[0]) {
            InputField tempInput = Temperature.GetComponent<InputField>();
            Text tempText = tempInput.textComponent;
            wellbeing -= (int) ( ( Mathf.Abs(50 - Convert.ToInt32(tempText.text) )-10)/1.5f );
        }

        if (Predictions[1]) {
            //wellbeing -= (int) (Mathf.Abs(//integer index of current enum value of Temperature Range\\ - HeatLevel.value)-3)*4;
        }

        if (Predictions[2]) {
            //if (Precipitation == typhoon) {wellbeing += 25;}
            //else {wellbeing -= 35;}
        }

        if (Predictions[3]) {
            //wellbeing -= (int) (Mathf.Abs(Pressure - PressureInput.text)-4)*4;
        }

        if (Predictions[4]) {
            //wellbeing -= (int) (Mathf.Abs(//integer index of current enum value of Pressure Range\\ - PressureLevels.value)-2)*6;
        }

        if (Predictions[5]) { }

        if (Predictions[6]) { }

        if (Predictions[7]) { }

        if (Predictions[8]) { }

        if (Predictions[9]) { }

        if (Predictions[10]) { }

        if (Predictions[11]) { }

        if (Predictions[12]) { }

        if (Predictions[13]) { }
    }
}
