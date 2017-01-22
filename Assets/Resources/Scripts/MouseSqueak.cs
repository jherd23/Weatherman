using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseSqueak : MonoBehaviour {

	public AudioSource aud;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (0f, 1f) < 0.00003) {
			aud.Play ();
		}
	}
}
