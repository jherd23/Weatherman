using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Script : MonoBehaviour {
	// Use this for initialization

	public float alphaTarget;
	public MeshRenderer r;
	public AudioSource aus;
	float timer;

	float waiting;
	bool wait;

    bool newgame;

    public GameObject RT;

	void Start () {
		r = this.gameObject.GetComponent < MeshRenderer >();
		alphaTarget = 0;
		timer = 0.0f;
		waiting = 0.0f;
		wait = false;
        newgame = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			//Debug.Log ("changing the day");
            ChangeDay();      
		}

		timer += 0.5f * Time.deltaTime;

		if (timer > 1.5f && alphaTarget == 1) {
			alphaTarget = 0;
            WeatherController WC = GameObject.Find("WeatherAndSaveController").GetComponent<WeatherController>();
            if (newgame)
            {
                WC.New();
                WC.win.setExterior(WC.days[0]);
                WC.setInstruments(WC.days[0]);
                newgame = false;
            }
            WC.incrementDay();
        }

		if (timer > 3.0) {
			r.enabled = false;
		}

		if (r.material.color.a >= alphaTarget) {
			r.material.color = new Color (0.0f, 0.0f, 0.0f, r.material.color.a-0.5f * Time.deltaTime);
		} else { 
			r.material.color = new Color (0.0f, 0.0f, 0.0f, r.material.color.a+0.5f * Time.deltaTime);
		}
			
		if (wait) {
			waiting += 0.5f * Time.deltaTime;
			if (waiting > 2.3) {
				waiting = 0;
				ChangeDay ();
			}
		}
	}

	public void ChangeDay(){
		r.enabled = true;
		timer = 0.0f;
		alphaTarget = 1;
		aus.Play ();
		wait = false;
	}

    public void NewDay()
    {
        newgame = true;
        ChangeDay();
    }

	public void ChangeDayOut(){
        RT.GetComponent<ReportScript>().Verify();
		Camera.main.GetComponent<CameraMovement> ().resetView ();
		wait = true;
	}
}