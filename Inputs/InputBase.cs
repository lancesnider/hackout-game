using UnityEngine;
using System.Collections;

public class InputBase : MonoBehaviour {
	
	private GameObject myColliderObject;
	private string varToChange;
	[HideInInspector] 
	public string currentSelection;
	public UILabel uiLabelScript;
	public MoverCode moverCode;
	private bool firstTime = true;
	
	[HideInInspector] 
	public string[] optionsArray = null;
	//public string optionsString;
	[HideInInspector] 
	public string defaultOptionString;
	
	
	
	public Color numberColor;
	public Transform underline; 
		
	//this gets run immediately from Parser2.cs to set default value
	public void OnSelectionChange(string type, string selectedItem)
	{
		
		
		ChangeText(selectedItem);
		currentSelection = selectedItem;
		
		//!!!call different one based on type
		if(type == "booleanInput"){
			//myColliderObject.GetComponent<LevelScript2>().ChangeBool(dictToPass);
			myColliderObject.GetComponent<LevelScript2>().ChangeBool(varToChange, selectedItem);
		}else if(type == "objectInput"){
			//myColliderObject.GetComponent<LevelScript2>().ChangeObject(dictToPass);
			myColliderObject.GetComponent<LevelScript2>().ChangeObject(varToChange, selectedItem);
		}else if(type == "dropDownInput"){
			//myColliderObject.GetComponent<LevelScript2>().ChangeDropDown(dictToPass);
			myColliderObject.GetComponent<LevelScript2>().ChangeDropDown(varToChange, selectedItem);
		}
		
		
	}
	
	//this gets run immediately from Parser2.cs
	public void setColliderObject(GameObject myGO, string getThisVar)
	{
		
		myColliderObject = myGO;
		varToChange = getThisVar;
		
	}
	
	//THIS IS ONLY RUN ON DROPDOWNS
	public void SetOptions(string thisString, string defaultOption)
	{
		string optionsString = AdvTxt.GetTxtBetwen(thisString, "options='", "'");
		optionsArray = optionsString.Split(',');
		defaultOptionString = defaultOption;
		
		float myFloat; 
		if(float.TryParse(defaultOption, out myFloat)){
			uiLabelScript.color = numberColor;
			underline.GetComponent<UISprite>().color = numberColor;
		}
		
	}
	
	public int getDefaultInt(string[] options, string defaultOption)
	{
		
		for(int i = 0;i<options.Length;i++){
			
			if(options[i] == defaultOption)
				return i;
			
		}
		
		return -1;
		
	}
	
	public void ChangeText(string changeTo)
	{
	
		float myFloat; 
		if(float.TryParse(changeTo, out myFloat)){
			uiLabelScript.color = numberColor;
			underline.GetComponent<UISprite>().color = numberColor;
		}
		
		//Debug.Log("from " + uiLabelScript.text + " to " + changeTo);
		uiLabelScript.text = changeTo;
		if(!firstTime){
			moverCode.MoveAllCode();
		}
		firstTime = false;
		
	}
	
	public virtual void ReceiveClick() {}
	
}
