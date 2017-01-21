using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMusicPlayer : MonoBehaviour {

	public AudioSource aud;
	public AudioMixerGroup amg;

	List<AudioClip> sounds;

	// Use this for initialization
	void Start () {
		aud.outputAudioMixerGroup = amg;
		sounds = new List<AudioClip> ();
		sounds.Add (Resources.Load ("Audio") as AudioClip);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
