using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Script : MonoBehaviour {
	// Use this for initialization

	public float alphaTarget;
	public MeshRenderer r;
	public AudioSource aus;
	float timer;

    bool first;

	void Start () {
		r = this.gameObject.GetComponent < MeshRenderer >();
		alphaTarget = 0;
		timer = 0.0f;
        first = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("changing the day");
            ChangeDay();      
		}

		timer += 0.5f * Time.deltaTime;

		if (timer > 1.5f && alphaTarget == 1) {
			alphaTarget = 0;
			GameObject.Find ("WeatherAndSaveController").GetComponent<WeatherController> ().incrementDay ();
		}

		if (timer > 3.0) {
			r.enabled = false;
		}

		if (r.material.color.a >= alphaTarget) {
			r.material.color = new Color (0.0f, 0.0f, 0.0f, r.material.color.a-0.5f * Time.deltaTime);
		} else { 
			r.material.color = new Color (0.0f, 0.0f, 0.0f, r.material.color.a+0.5f * Time.deltaTime);
		}
			
	}

	void ChangeDay(){
		r.enabled = true;
		timer = 0.0f;
		alphaTarget = 1;
        if (!first)
        
        aus.Play();
        
	}
}