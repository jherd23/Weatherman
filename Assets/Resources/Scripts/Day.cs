using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day {

	// this class contains all the weather properties for a day

	// state variables for Day

	public int Index;
	public int season;
	public float Temperature;
	public temperatureRange Temprange;
	public bool Anomaly;
	public float Pressure;
	public pressureRange PressureRange;
	public cloudCover Cloudcover;
	bool Fog;
	public float Humidity;
	public skyColor Skycolor;
	public precipitation Precipitation;
	public windType Windtype;
	public float WindSpeed;
	public seaState Seastate;
	public windDirection WindDirection;

	// Use this for initialization
	public void Start () {
	
	}

	// Update is called once per frame
	public void Update () {		
	
	}

	// possible variations of each state 

	public enum temperatureRange {subfreezing, freezing, cold, tepid, warm, hot, boiling};
	public enum pressureRange {low, moderate, high};
	public enum cloudCover {sunny, partly_cloudy, overcast};
	public enum skyColor {blue, grey, white};
	public enum precipitation {none,rain,typhoon,storm,snow,blizzard};
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

	public Day (int index, float temp, float pr, pressureRange p, cloudCover c, bool fog, float h, skyColor sc, precipitation prc, windType wt, float ws, 
		seaState st, temperatureRange tr, windDirection wd) 

	{
		this.Index = index;
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