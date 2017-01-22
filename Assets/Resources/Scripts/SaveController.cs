using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class SaveController : MonoBehaviour {
	//on enable on disable for autosave

	public static SaveController s_instance;
	public GameObject WeatherController;

	// Use this for initialization
	void Awake()
	{
		if (s_instance == null)
		{
			DontDestroyOnLoad(gameObject); // save object on scene mvm
			s_instance = this;
		}
		else if (s_instance != this)
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{

	}

	void OnGUI()
	{

	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/saveInfo" + ".dat");
		SaveData data = WriteToData();
		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/saveInfo" + ".dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/saveInfo" + ".dat",
				FileMode.Open);
			SaveData data = (SaveData) bf.Deserialize(file);
			file.Close();

			WriteFromData (data);
		}
	}

	private void WriteFromData(SaveData data) //loading
	{
		//Health.GetComponent<Slider>().value = data.health;
	}

	private SaveData WriteToData () //saving 
	{
		SaveData data = new SaveData();
		//data.health = Health.GetComponent<Slider>().value;
		return data;
	}
}

[Serializable]
class SaveData 
{
	public int daysPerSeason;
	public int numberOfSeasons;
	public int currentDay;
	public Device[] devices;
	public Day[] days;
	public bool[][] predictions;
}
