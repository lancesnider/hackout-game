#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestDragDrop : LevelScript2 {
	
	/*public void Reset(){
		defaultOption = "crabs";
	}*/
	
	public void createVisualCode()
	{
		
		visualCode = "//this turns the lights on and off~/n~" +
			"myObject = ~[type='objectInput' var='myObject1' default='crabs']~;";	
		
		parserScript.createCodeVisually(gameObject, visualCode);
	}
	
	public override void codeToRun()
	{
		
		Debug.Log("This is the code on TestDragDrop Running");
		//if(boolsDict.ContainsKey("myBool"))
		//gameObject.SendMessage("disableObject", boolsDict["myBool"]);
		
		/*if(objectsDict.ContainsKey("objectToMove") && objectsDict.ContainsKey("objectTo")){
			objectsDict["objectToMove"].SendMessage("goToPos", objectsDict["objectTo"]);
		}*/
		
	}	
}
