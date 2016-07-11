#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestBool : LevelScript2 {
	
	/*public void Reset(){
		defaultOption = "true";
	}*/
	
	public void createVisualCode()
	{
		
		visualCode = "//this turns the lights on and off~/n~" +
			"[type='booleanInput' var='myBool2' default='false']~.myBool = ~[type='booleanInput' var='myBool' default='true']~;";	
		
		parserScript.createCodeVisually(gameObject, visualCode);
	}
	
	public override void codeToRun()
	{
		
		Debug.Log("This is the code on TestBool Running");
		if(boolsDict.ContainsKey("myBool"))
			//gameObject.SendMessage("disableObject", boolsDict["myBool"]);
			Debug.Log("TestBool: " + boolsDict["myBool"]);
		
	}	
}
