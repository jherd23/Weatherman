using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProperPlacement : MonoBehaviour {

    public GameObject r;

	// Use this for initialization
	void Start () {
        Vector3 re = Camera.main.WorldToScreenPoint(r.transform.position);
        this.gameObject.transform.position = new Vector3(re.x, re.y, 0);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
