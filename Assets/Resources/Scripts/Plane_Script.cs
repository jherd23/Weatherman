using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Script : MonoBehaviour {
	// Use this for initialization

	public float alphaTarget;
	public MeshRenderer r;

	void Start () {
		r = this.gameObject.GetComponent < MeshRenderer >();
		alphaTarget = 1;
	}

	// Update is called once per frame
	void Update () {
		if (r.material.color.a >= alphaTarget) {
			r.material.color = new Color (0.0f, 0.0f, 0.0f, r.material.color.a-0.5f * Time.deltaTime);
		} else { 
			r.material.color = new Color (0.0f, 0.0f, 0.0f, r.material.color.a+0.5f * Time.deltaTime);
		}

		/*if (Input.GetKeyDown (KeyCode.Space)) {
			alphaTarget = 1 - alphaTarget;
		}*/
	}
}