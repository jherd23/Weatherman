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
    public Button info;
	public Text t;
	public Button tButton;

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
		info.image.enabled = false;
		tButton.image.enabled = false;
		t.enabled = false;
		b.GetComponentInChildren<Text> ().enabled = true;
        i.gameObject.SetActive(true);
        i2.gameObject.SetActive(true);
        i3.gameObject.SetActive(true);
        i4.gameObject.SetActive(true);
        i5.gameObject.SetActive(true);
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

			targetFOV = 22;
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
        i.gameObject.SetActive(false);
        i2.gameObject.SetActive(false);
        i3.gameObject.SetActive(false);
        i4.gameObject.SetActive(false);
        i5.gameObject.SetActive(false);
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
		info.image.enabled = true;

        b.GetComponentInChildren<Text>().enabled = true;
        i.gameObject.SetActive(false);
        i2.gameObject.SetActive(false);
        i3.gameObject.SetActive(false);
        i4.gameObject.SetActive(false);
        i5.gameObject.SetActive(false);

        if (obj.name == "weathervane") {
			t.text = "History tells us that the first Weathercock sat on the roof of the Tower of the Winds in the agora, or marketplace, of Athens, Greece. The Weathercock is used to tell the direction of the wind. However, this one appears to be missing its labels...";
		} else if (obj.name == "rainGauge") {
			t.text = "The Pluviometer was first developed by the people of Magadha in India to record rainfall. Readings were correlated against crop yields for several years until they become reliable enough to be used as a basis for land taxes. Hopefully this instrument can prove as profitable.";
		} else if (obj.name == "galilean") {
			t.text = "Named after Galileo's own Thermoscope, this Thermometer was invented by his pupil, Torricelli of Florance in the seventeenth century. The bobbles inside, filled with arcane elements and compounds rise and fall with the temperature, but which elements indicate heat and which cold?";
		} else if (obj.name == "fitzroystormglass") {
			t.text = "This 'Storm Glass' was invented by Admiral Fitzroy while aboard the HMS Beagle with Charles Darwin. Its results are questionable, but he clained one could read the barometric pressure by the presence of crystals in the flask. On the ship, he and Darwin used it to adhere to the Beaufort wind scale for weather forcasting";
		} else {
			t.text = "Invented by Dr. John Thomas Romney Robinson in 1845, the Cup anemometer is used to tell wind speed. It isn't very precise, but it should reliably tell us when to board up the windows and warn the townsfolk.";
		}
    }

	public void Info(){
		tButton.image.enabled = !tButton.image.enabled;
		t.enabled = !t.enabled;
	}

}
