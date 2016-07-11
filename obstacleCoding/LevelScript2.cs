#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScript2 : MonoBehaviour {

	
	//--------------Do not modify below this line--------------//
	
	[HideInInspector]
	public Parser2 parserScript;
	//public GameObject[] objectsInScene;

	public Dictionary<string, bool> boolsDict = new Dictionary<string, bool>();
	public Dictionary<string, string> dropDownsDict = new Dictionary<string, string>();
	public Dictionary<string, string> textsDict = new Dictionary<string, string>();
	public Dictionary<string, int> numbersDict = new Dictionary<string, int>();
	public Dictionary<string, GameObject> objectsDict = new Dictionary<string, GameObject>();
	//private objectController[] objectControllers;
	//public string defaultOption;
	
	[HideInInspector] 
	public string visualCode;
	[HideInInspector] 
	public GameObject thisGO;
	
	//public string objectIconName;
	//public string objectScriptName;
	
	void OnEnable()
	{
		PlayButton.PlayAllCode += codeToRun;
		//Debug.Log(transform);
		parserScript = GameObject.FindWithTag("ParserGO").GetComponent<Parser2>() as Parser2;
		thisGO = gameObject;
		
	}
	
	void OnDisable()
	{
		PlayButton.PlayAllCode -= codeToRun;
	}
	
	public virtual void codeToRun()
	{
		Debug.Log("This should have been overridden");
	}
	
	//-------------------------set vars
	
	public void ChangeBool(string varToChange, string selectedItem)
	{
		
		bool selectedItemBool = false;
	
		if(selectedItem == "true"){
			selectedItemBool = true;
		}
		
		if(boolsDict.ContainsKey(varToChange)){
			boolsDict[varToChange] = selectedItemBool;
		}else{
			boolsDict.Add(varToChange, selectedItemBool);
		}
		
	}
	
	public void ChangeDropDown(string varToChange, string selectedItem)
	{
		if(dropDownsDict.ContainsKey(varToChange)){
			dropDownsDict[varToChange] = selectedItem;
		}else{
			dropDownsDict.Add(varToChange, selectedItem);
		}
	}
	
	public void ChangeObject(string varToChange, string selectedItem)
	{
		
		GameObject selectedItemGO = GameObject.Find(selectedItem);
		
		if(objectsDict.ContainsKey(varToChange)){
			objectsDict[varToChange] = selectedItemGO;
		}else{
			objectsDict.Add(varToChange, selectedItemGO);
		}
	}
	
	
	//-------------------------get vars
	
	
	
	
	public string getCurrentBool(string boolName)
	{
		
		if(boolsDict.ContainsKey(boolName)){
			return boolsDict[boolName].ToString().ToLower();
		}
		
		return ""; 
		
	}
	
	public string getCurrentDropDown(string dropDownName)
	{
		
		if(dropDownsDict.ContainsKey(dropDownName)){
			return dropDownsDict[dropDownName];
		}
		
		return ""; 
		
	}
	
	public string getCurrentObject(string gameObjectName)
	{
		
		if(objectsDict.ContainsKey(gameObjectName)){
			//Debug.Log(objectsDict[gameObjectName].name);
			return objectsDict[gameObjectName].name;
			
		}
		return ""; 
		
	}
	
	
}
