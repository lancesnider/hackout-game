using UnityEngine;
using System.Collections;

public class ObjectInput2 : InputBase {

	
	
	public void DroppedOn(string thisName)
	{
		OnSelectionChange("objectInput", thisName);
	}
	
	
}
