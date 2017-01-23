using System.Collections;
using UnityEngine;

public class Clickable : MonoBehaviour {

    public int i;

	// Use this for initialization
	void Start () {
        i = 0;
	}

    // Update is called once per frame
    void Update() {
    
        if (Input.GetMouseButtonDown(0))
        {
            //i++;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
               i++;
               Debug.Log("My object is clicked by mouse"); 
            }
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("kill me");
        }
    }
}
