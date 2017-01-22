using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class WindowOpener : MonoBehaviour {

	public Slider control;

	public bool Fair;
	public bool Rain;

	float lastPos;
	bool moving;

	public AudioMixerGroup roof;
	public AudioMixerGroup roofParent;
	public AudioMixerGroup rain;
	public AudioMixerGroup outside;

	//gentle
	public AudioSource ausRain;
	public AudioSource ausWaves;
	public AudioSource ausWind;

	//mean 
	public AudioSource ausWindHowl;
	public AudioSource ausStormSea;
	public AudioSource ausStormWaves;

	//roof
	public AudioSource ausRoofRain;

	//window itself
	public AudioSource ausWindowSqueak;
	public AudioSource ausWindowShut;


	// Use this for initialization
	void Start () {

		//setup sources
		lastPos = control.value;
		ausWindowShut.volume = 1.0f;
		moving = false;
		DisableAll ();
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, control.value, gameObject.transform.position.z);
		outside.audioMixer.SetFloat ("cutoffOutside", 2102.1486f * Mathf.Log ((control.value - 539.0f)) + 9000.0f);
		roof.audioMixer.SetFloat ("cutoffOutside", 2102.1486f * Mathf.Log ((control.value - 539.0f)) + 9000.0f);

		outside.audioMixer.SetFloat("volOutside", ((6.0f/484.0f) * (control.value - 540.0f)) - 6.00f);
		roofParent.audioMixer.SetFloat("volRoofParent", ((6.0f/484.0f) * (control.value - 540.0f)) - 6.00f);

		if (Fair) {
			ausWaves.volume = 1.0f;
			ausWind.volume = 1.0f;
			ausWindHowl.volume = 0.0f;
			ausStormSea.volume = 0.0f;
			ausStormWaves.volume = 0.0f;
		} else {
			ausWindHowl.volume = 1.0f;
			ausStormSea.volume = 1.0f;
			ausStormWaves.volume = 1.0f;
			ausWaves.volume = 0.0f;
			ausWind.volume = 0.0f;
		}

		if (Rain) {
			ausRoofRain.volume = 1.0f;
			ausRain.volume = 1.0f;
			if (Fair) {
				rain.audioMixer.SetFloat ("volRainOutside", 9);
				roof.audioMixer.SetFloat ("volRoof", 4);
			} else {
				rain.audioMixer.SetFloat ("volRainOutside", 15);
				roof.audioMixer.SetFloat ("volRoof", 14);
			}
		} else {
			ausRoofRain.volume = 0.0f;
			ausRain.volume = 0.0f;
		}

		if (control.value != lastPos) {
			ausWindowSqueak.volume = 0.7f;
			lastPos = control.value;
			moving = true;
		} else {
			Debug.Log ("stopped");
			ausWindowSqueak.volume = 0.0f;
			if (lastPos < 550.0f && moving) {
				ausWindowShut.Play ();
			}
			moving = false;
		}
	}

	void DisableAll (){
		ausRain.volume = 0.0f;
		ausWaves.volume = 0.0f;
		ausWind.volume = 0.0f;

		ausWindHowl.volume = 0.0f;
		ausStormSea.volume = 0.0f;
		ausStormWaves.volume = 0.0f;

		ausRoofRain.volume = 0.0f;

		ausWindowSqueak.volume = 0.0f;
	}
	//2102.1486 * log(x - 539) + 9000
}
