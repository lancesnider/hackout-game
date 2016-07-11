#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestDropDown : LevelScript2 {

	void Start()
	{
		ChangeDropDown("myDropDown", "cat");
		ChangeDropDown("myDropDown2", "1");
	}
	
	public void createVisualCode()
	{
		
		visualCode = "//this turns the lights on and off~/n~" +
			"dropDown.~[type='dropDownInput' var='myDropDown' options='enabled,disabled,fish,cat,dog,finished' default='" + getCurrentDropDown("myDropDown") + "']~ = " + CodeColors.boolColor + "true[-];~/n~" +
				"dropDown.~[type='dropDownInput' var='myDropDown2' options='1,2,3,4,5,6' default='" + getCurrentDropDown("myDropDown2") + "']~ = true;";
		
		parserScript.createCodeVisually(gameObject, visualCode);
	}
	
	public override void codeToRun()
	{
		
		Debug.Log("myDropDown = " + getCurrentDropDown("myDropDown"));
		//if(boolsDict.ContainsKey("myBool"))
		//gameObject.SendMessage("disableObject", boolsDict["myBool"]);
		
		/*if(objectsDict.ContainsKey("objectToMove") && objectsDict.ContainsKey("objectTo")){
			objectsDict["objectToMove"].SendMessage("goToPos", objectsDict["objectTo"]);
		}*/
		
	}	
}
