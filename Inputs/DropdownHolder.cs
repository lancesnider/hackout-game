using UnityEngine;
using System.Collections;

public class DropdownHolder : MonoBehaviour {

	
	public Transform bg; 
	
	public void MoveBG(int numberOfOptions, float btnHeight, float maxWidth)
	{
		
		Transform thisT = transform; 
		float newHeight = numberOfOptions * btnHeight;
		
		bg.localScale = new Vector3(maxWidth + btnHeight,newHeight,1);
		bg.localPosition = new Vector3(0, -newHeight/2, 0);
		
	}
	
}
