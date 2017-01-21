using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour {

	// this class contains all the weather properties for a day

	// state variables for Day

	public int season;
	public int Temperature;
	public temperatureRange Temprange;
	public bool Anomaly;
	int Pressure;
	public pressureRange PressureRange;
	public cloudCover Cloudcover;
	bool Fog;
	public int Humidity;
	public skyColor Skycolor;
	public precipitation Precipitation;
	public windType Windtype;
	int WindSpeed;
	public seaState Seastate;
	public windDirection WindDirection;

	// Use this for initialization
	public void Start () {
	
	}

	// Update is called once per frame
	public void Update () {		
	
	}

	// possible variations of each state 

	public enum temperatureRange {heatstroke1, heatstroke2, heatstroke3, fatigue, caution, fair};
	public enum pressureRange {v_Low, low, moderate, high, v_High};
	public enum cloudCover {sunny, partly_cloudy, very_cloudy, overcast, heavy};
	public enum skyColor {blue, grey, green};
	public enum precipitation {clear, light, mid, hard, torrential};
	public enum windType {calm, breeze, strong_breeze, moderate, gale, storm, hurricane}; // based loosely on beufort scale
	public enum windDirection {N, S, E, W, NW, NE, SW, SE};
	public enum seaState {calm, smooth, slight, moderate, rough, very_rough, phenomenal};

	public Day (){

	}

	/*
	public Day (int p){
		this.Pressure = p;
	}
	*/ 



	// Constructor. Takes in passed values to determine the weather of this day.

	public Day (int temp, int pr, pressureRange p, cloudCover c, bool fog, int h, skyColor sc, precipitation prc, windType wt, int ws, 
		seaState st, temperatureRange tr, windDirection wd) 

	{
		this.Temperature = temp;
		this.Pressure = pr;
		this.PressureRange = p;
		this.Cloudcover = c;
		this.Fog = fog;
		this.Humidity = h;
		this.Skycolor = sc;
		this.Precipitation = prc;
		this.Windtype = wt;
		this.WindSpeed = ws;
		this.Seastate = st;
		this.Temprange = tr;
		this.WindDirection = wd;
	}
		

}