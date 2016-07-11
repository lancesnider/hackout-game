using UnityEngine;
using System.Collections;

public class BoolInput2 : InputBase {
	
	public override void ReceiveClick()
	{

		if(currentSelection == "true"){
			OnSelectionChange("booleanInput", "false");
		}else{
			OnSelectionChange("booleanInput", "true");
		}
		
	}
	
}
