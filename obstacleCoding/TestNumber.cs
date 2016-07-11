#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestNumber : LevelScript2 {
	
	/*public void Reset(){
		defaultOption = "true";
	}*/
	
	public void createVisualCode()
	{
		
		
		visualCode = "//this turns the lights on and off~/n~" +
			"Light.position.x = ~[type='numberInput' var='myNumber' default='true']~;";	
		
		parserScript.createCodeVisually(gameObject, visualCode);
	}
	
	public override void codeToRun()
	{
		
		Debug.Log("This is the code on TestNumber Running");
		//if(boolsDict.ContainsKey("myBool"))
		//gameObject.SendMessage("disableObject", boolsDict["myBool"]);
		
		/*if(objectsDict.ContainsKey("objectToMove") && objectsDict.ContainsKey("objectTo")){
			objectsDict["objectToMove"].SendMessage("goToPos", objectsDict["objectTo"]);
		}*/
		
	}	
}
