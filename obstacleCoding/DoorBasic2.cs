#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorBasic2 : LevelScript2 {
	
	/*public void Reset(){
		defaultOption = "true";
	}*/
	
	public void createVisualCode()
	{
		
		//Debug.Log("creating code");
		
		//visualCode = "//this turns the door on and off~/n~" +
		//	gameObject.name + ".enabled = ~[type='booleanInput' var='myBool' default='true']~;";
			
			/*visualCode = "//this turns the door on and off~/n~" +
			"thisCube.enabled =";*/
		
		//parserScript.createCodeVisually(gameObject, visualCode);
		
		
		
		visualCode = "//this turns the lights on and off~/n~" +
			"myBool = ~[type='booleanInput' var='myBool' default='false']~;~/n~" +
			"myObject = ~[type='objectInput' var='myObject1']~;~/n~" +
			"dropDown.~[type='dropDownInput' var='myDropDown' options='enabled,disabled,fish']~ = true;~/n~" +
			"Light.position.x = ~[type='numberInput' var='myNumber' default='9']~;";	
				
		parserScript.createCodeVisually(gameObject, visualCode);
	}
	
	public override void codeToRun()
	{
		
		Debug.Log("This is the code on DoorBasic2 Running");
		//if(boolsDict.ContainsKey("myBool"))
			//gameObject.SendMessage("disableObject", boolsDict["myBool"]);
		
		/*if(objectsDict.ContainsKey("objectToMove") && objectsDict.ContainsKey("objectTo")){
			objectsDict["objectToMove"].SendMessage("goToPos", objectsDict["objectTo"]);
		}*/
		
	}	
	
}
