using UnityEngine;
using System.Collections;
using System.IO;


public class newsHub: MonoBehaviour
{
	
	//public string txtFile = "testTelegram";
	string txtContents;
	string txtContents2;
    
	// Use this for initialization
	void Start ()
	{

		TextAsset txtAssetN1 = (TextAsset)Resources.Load("WeathermanNegative1");
		TextAsset txtAssetN2 = (TextAsset)Resources.Load("WeathermanNegative2");
		TextAsset txtAssetN3 = (TextAsset)Resources.Load("WeathermanNegative3");
		TextAsset txtAssetN4 = (TextAsset)Resources.Load("WeathermanNegative4");
		TextAsset txtAssetN5= (TextAsset)Resources.Load("WeathermanNegative5");
		TextAsset txtAssetN6 = (TextAsset)Resources.Load("WeathermanNegative6");
		TextAsset txtAssetN7= (TextAsset)Resources.Load("WeathermanNegative7");
		TextAsset txtAssetN8 = (TextAsset)Resources.Load("WeathermanNegative8");
		TextAsset txtAssetN9= (TextAsset)Resources.Load("WeathermanNegative9");
		TextAsset txtAssetN10 = (TextAsset)Resources.Load("WeathermanNegative10");

		TextAsset txtAssetP1 = (TextAsset)Resources.Load("WeathermanPositive1");
		TextAsset txtAssetP2 = (TextAsset)Resources.Load("WeathermanPositive2");
		TextAsset txtAssetP3 = (TextAsset)Resources.Load("WeathermanPositive3");
		TextAsset txtAssetP4 = (TextAsset)Resources.Load("WeathermanPositive4");
		TextAsset txtAssetP5= (TextAsset)Resources.Load("WeathermanPositive5");
		TextAsset txtAssetP6 = (TextAsset)Resources.Load("WeathermanPositive6");
		TextAsset txtAssetP7= (TextAsset)Resources.Load("WeathermanPositive7");
		TextAsset txtAssetP8 = (TextAsset)Resources.Load("WeathermanPositive8");
		TextAsset txtAssetP9= (TextAsset)Resources.Load("WeathermanPositive9");
		TextAsset txtAssetP10 = (TextAsset)Resources.Load("WeathermanPositive10");

		txtContents = txtAssetN1.text +
		txtAssetN2.text +
		txtAssetN3.text +
		txtAssetN4.text +
		txtAssetN5.text +
		txtAssetN6.text +
		txtAssetN7.text +
		txtAssetN8.text +
		txtAssetN9.text +
		txtAssetN10.text;

		txtContents2 = txtAssetP1.text +
		txtAssetP2.text +
		txtAssetP3.text +
		txtAssetP4.text +
		txtAssetP5.text +
		txtAssetP6.text +
		txtAssetP7.text +
		txtAssetP8.text +
		txtAssetP9.text +
		txtAssetP10.text;

	
		char[] delimiterChars = {'^'};

		string[] negativeTelegrams = txtContents.Split(delimiterChars);
		//string[] positiveTelegrams = txtContents.Split(delimiterChars);

		Debug.Log(giveRandomTelegram(negativeTelegrams));
						
	}


	// takes in a telegram array, returns a random telegram

	string giveRandomTelegram(string [] telegramList){
		int randomNumber = Random.Range(0, telegramList.Length);
		return telegramList [randomNumber];
	}

	// Update is called once per frame
	void Update ()
	{


	}

}

