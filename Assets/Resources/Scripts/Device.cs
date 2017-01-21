using UnityEngine;
using System.Collections;

public abstract class Device : MonoBehaviour
{

	public int unlockSeason;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public abstract void set (Day d);
}

