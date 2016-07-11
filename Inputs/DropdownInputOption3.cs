using UnityEngine;
using System.Collections;

public class DropdownInputOption3 : InputBase {
	
	//MUST BE NAMED dropdownOption3
	
	//this was created by DropdownInput3
	//make sure this is an fClick
	//Maybe make this part of InputBase?
	
	//public override void ReceiveClick()
	
	private float colliderHeight = 44f;
	private float leftTxtBuffer = 0.5f;
	private DropdownInput3 callbackScript;
	
	public override void ReceiveClick()
	{
		
		callbackScript.OptionClicked(uiLabelScript.text);
		
	}
	
	public void ResizeOption(float optionW)
	{
		
		Transform thisT = transform; 
		BoxCollider thisC = NGUITools.AddWidgetCollider(gameObject) as BoxCollider;
		//thisC = collider as BoxCollider;
		ResizeUnderline(thisC.size.x);
		
		thisC.size = new Vector3(optionW, colliderHeight, -0.5f);
		//Debug.Log(thisC.size);
		
		
	}
	
	private void ResizeUnderline(float widthTo)
	{
	
		underline.localScale = new Vector3(widthTo - leftTxtBuffer *2, 1, 1);
		underline.localPosition = new Vector3(widthTo/2 + leftTxtBuffer, -11f, 0);
	}
	
	public void SetCallBackScript(GameObject callbackGO)
	{
		callbackScript = callbackGO.GetComponent<DropdownInput3>() as DropdownInput3;
	}
	
	

}
