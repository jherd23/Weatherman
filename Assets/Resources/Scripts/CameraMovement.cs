using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

	private Vector3 posVelocity = new Vector3 (50, 0, 0);
	private Vector3 rotVelocity = new Vector3 (1, 0, 0);
	private float fovVelocity = 0.2f;

	private bool dir;
	private bool moving;
	private GameObject obj;
	private bool UI;

	private Vector3 targetRotation;
	private Vector3 initRotation;

	private Vector3 targetpos;
	private Vector3 initpos;

	private float targetFOV;
	private float initFOV;

	public Button b;

    public Button i;
    public Button i2;
    public Button i3;
    public Button i4;
    public Button i5;

    // Use this for initialization
    void Start () {
		initpos = transform.position;
		initFOV = Camera.main.fieldOfView;
		initRotation = transform.localEulerAngles;

		targetpos = initpos;
		targetFOV = initFOV;


		resetView ();
	}



	// Update is called once per frame
	void Update() {

		if(targetpos == initpos && targetFOV == initFOV){
			b.image.enabled = false;
			b.GetComponentInChildren<Text> ().enabled = false;
		}

		if (moving) {
			transform.position = Vector3.SmoothDamp (transform.position, targetpos, ref posVelocity, 2.0f);
			Camera.main.fieldOfView = Mathf.SmoothDamp (Camera.main.fieldOfView, targetFOV, ref fovVelocity, 2.0f);
			transform.localEulerAngles = Vector3.SmoothDamp (transform.localEulerAngles, targetRotation, ref rotVelocity, 2.0f);
		} 

	}

	public void resetView() {
		targetpos = initpos;
		targetFOV = initFOV;
        targetRotation = initRotation;
        b.image.enabled = true;
		b.GetComponentInChildren<Text> ().enabled = true;
        i.enabled = true;
        i2.enabled = true;
        i3.enabled = true;
        i4.enabled = true;
        i5.enabled = true;
    }

	public void focusUI(string name) {
		float f = 5.0f;

		if (name == "stormglass note") {
			obj = GameObject.Find ("fitzroystormglass");
			f = 6.0f;
			targetFOV = 8;
		} else if (name == "galilean note") {
			obj = GameObject.Find ("galilean");
			f = 9.0f;
			targetFOV = 13;
		} else if (name == "cardinal1" || name == "cardinal2" || name == "cardinal3" || name == "cardinal4") {
			obj = GameObject.Find ("weatervane");
			f = 10.0f;
			targetFOV = 9;
		}

		if (name == "sidewall") {
			obj = GameObject.Find ("ReportSprite");
			f = 270.0f;

			targetFOV = 20;
			targetpos = new Vector3 (0, obj.transform.position.y+60, obj.transform.position.z);
			targetRotation = new Vector3 (transform.localEulerAngles.x, f, transform.localEulerAngles.z);
		} else {

			targetpos = new Vector3 (obj.transform.position.x, obj.transform.position.y * 2, transform.position.z);

			//targetFOV = 8;
			targetRotation = new Vector3 (/*transform.localEulerAngles.x*/ f, transform.localEulerAngles.y, transform.localEulerAngles.z);
		}
		UI = true;
		moving = true;

        b.image.enabled = true;
        b.GetComponentInChildren<Text>().enabled = true;
        i.enabled = false;
        i2.enabled = false;
        i3.enabled = false;
        i4.enabled = false;
        i5.enabled = false;
    }

    public void focusObj(GameObject obj)
    {
        //moving = true;
        targetpos = new Vector3(obj.transform.position.x, obj.transform.position.y + 200, transform.position.z);
        targetFOV = 18;
        if (transform.position.x > obj.transform.position.x)
        {
            dir = false;
        }
        else
        {
            dir = true;
        }
        moving = true;
        targetRotation = initRotation;
        b.image.enabled = true;
        b.GetComponentInChildren<Text>().enabled = true;
        i.enabled = false;
        i2.enabled = false;
        i3.enabled = false;
        i4.enabled = false;
        i5.enabled = false;
    }

}
