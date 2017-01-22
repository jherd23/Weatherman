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
	public AudioMixerGroup inside;
	public AudioMixerGroup roof;

	AudioSource ausOut;
	AudioSource ausIn;
	AudioSource ausRoof;

	//gentle
	AudioClip rain;
	AudioClip waves;
	AudioClip wind;

	//mean 
	AudioClip windHowl;
	AudioClip stormSea;
	AudioClip stormWaves;

	//roof
	AudioClip roofRain;

	//window itself
	AudioClip windowSqueak;
	AudioClip windowShut;

	// Use this for initialization
	void Start () {
		ausOut = new AudioSource ();
		ausOut.outputAudioMixerGroup = outside;

		ausIn = new AudioSource ();
		ausIn.outputAudioMixerGroup = roof;

		ausRoof = new AudioSource ();
		ausRoof.outputAudioMixerGroup = inside;

		rain = Resources.Load ("Audio/LightRain") as AudioClip;
		waves = Resources.Load ("Audio/WavesGentle") as AudioClip;
		wind = Resources.Load ("Audio/Wind+Crickets") as AudioClip;

		windHowl = Resources.Load ("Audio/WindHowl") as AudioClip;
		stormSea = Resources.Load ("Audio/StormSea") as AudioClip; 
		stormWaves = Resources.Load ("Audio/Stormwaves") as AudioClip;

		roofRain = Resources.Load ("Audio/RainRoof") as AudioClip;

		windowSqueak = Resources.Load ("Audio/WindowSqueak") as AudioClip;
		windowShut = Resources.Load ("Audio/WindowShut") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, control.value, gameObject.transform.position.z);
	}
}
