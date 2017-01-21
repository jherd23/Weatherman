using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour {

	// this class contains all the weather properties for a day

	// state variables for Day

	public int season;
	public int Temperature;
	public bool Anomaly;
	 pressure Pressure;
	 cloudCover Cloudcover;
	 humidity Humidity;
	 skyColor Skycolor;
	 precipitation Precipitation;
	 windType Windtype;
	 seaState Seastate;
	 temperatureRange Temprange;
	 windDirection Windirection;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {		
	}

	// possible variations of each state 

	enum pressure {v_Low, low, moderate, high, v_High};
	enum cloudCover {sunny, partly_cloudy, very_cloudy, overcast, heavy};
	enum humidity {arid, dry, neutral, moist, sticky};
	enum skyColor {blue, grey, green};
	enum precipitation {clear, light, mid, hard, torrential};
	enum windType {calm, breeze, strong_breeze, moderate, gale, storm, hurricane}; // based loosely on beufort scale
	enum seaState {calm, smooth, slight, moderate, rough, very_rough, phenomenal};
	enum temperatureRange {heatstroke1, heatstroke2, heatstroke3, fatigue, caution, fair};
	enum windDirection {N, S, E, W, NW, NE, SW, SE};

	public Day (){

	}

	/*
	public Day (int p){
		this.Pressure = p;
	}
	*/ 



	// Constructor. Takes in passed values to determine the weather of this day.

	Day (int temp, pressure p, cloudCover c, humidity h, skyColor sc, precipitation prc, windType wt, 
		seaState st, temperatureRange tr, windDirection wd) 

	{
		this.Temperature = temp;
		this.Pressure = p;
		this.Cloudcover = c;
		this.Humidity = h;
		this.Skycolor = sc;
		this.Precipitation = prc;
		this.Windtype = wt;
		this.Seastate = st;
		this.Temprange = tr;
		this.Windirection = wd;
	}

}
