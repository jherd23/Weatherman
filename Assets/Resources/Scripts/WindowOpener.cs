using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class WindowOpener : MonoBehaviour {

	public Slider control;

	public AudioMixerGroup outside;
	public AudioMixerGroup rainOutside;
	public AudioMixerGroup windOutside;
	public AudioMixerGroup otherOutside;
	AudioSource aus;

	//gentle
	AudioClip rain;
	AudioClip waves;
	AudioClip wind;

	//mean 
	AudioClip windHowl;
	AudioClip stormSea;
	AudioClip stormWaves;

	// Use this for initialization
	void Start () {
		aus = new AudioSource ();
		aus.outputAudioMixerGroup = outside;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, control.value, gameObject.transform.position.z);
	}
}
