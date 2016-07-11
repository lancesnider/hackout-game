using UnityEngine;
using System.Collections;

public class DropdownInput3 : InputBase {
	
	/// <summary>
	///
	/// ACCESSABLE VARS: 
	///		optionsArray
	///		defaultOptionString
	///
	///	SETTING A NEW VARIABLE FOR THE INPUT: 
	///		string varToChangeTo = "blah";
	///		OnSelectionChange("dropDownInput", varToChangeTo);
	///
	///	QUESTIONS: 
	///		What happens if the code panel goes away before dropdown killed?
	///
	/// </summary>
	
	//private bool ddActive;
	private GameObject currentDD;
	public float btnHeight = 44f;
	public GameObject dropdownHolder;
	public GameObject dropdownOption;
	private Transform thisT;
	
	void Start()
	{
		thisT = transform; 
	}
	
	public override void ReceiveClick()
	{
		
		PopulateDD();
		
	}
	
	public void OptionClicked(string thisOption)
	{
		OnSelectionChange("dropDownInput", thisOption);
		KillDD();
	}
	
	private void PopulateDD()
	{
		
		KillDD();
		
		//create the holder
		GameObject newDDHolderGO = Instantiate(dropdownHolder, Vector3.zero, Quaternion.identity) as GameObject;
		Transform newDDHolderT = newDDHolderGO.transform;
		newDDHolderT.parent = thisT.parent;
		newDDHolderT.localScale = new Vector3(1,1,1);
		
		//add each options
		float maxWidth = AddOptions(newDDHolderT);
		
		//set the holder & bg position
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(thisT.parent, thisT);
		newDDHolderT.localPosition = new Vector3(thisT.localPosition.x + bounds.extents.x, thisT.localPosition.y - btnHeight/2, 0);
		DropdownHolder ddHolderScript = newDDHolderGO.GetComponent<DropdownHolder>() as DropdownHolder;
		int numberOfOptions = optionsArray.Length;
		ddHolderScript.MoveBG(numberOfOptions, btnHeight, maxWidth);
		
		//share a parent with this
		//position based on this transform.position
		
		//optionsArray
		//defaultOptionString
		
		//set this script to call back to (don't use listeners)
		
		//create an array of each option created
		
		//listen for nothing clicked
		InputController2.OnItemDown += CheckIfDDOptionClicked;
		
		//ddActive = true;
		currentDD = newDDHolderGO;
		
	}
	
	private float AddOptions(Transform thisParent)
	{
	
		float maxWidth = 0; 
		DropdownInputOption3[] ddInputScripts = new DropdownInputOption3[optionsArray.Length]; 
		
		for(int i = 0;i<optionsArray.Length;i++){
		
			GameObject newOptionGO = Instantiate(dropdownOption, Vector3.zero, Quaternion.identity) as GameObject;
			newOptionGO.name = "dropdownOption3";
			Transform newOptionT = newOptionGO.transform;
			newOptionT.parent = thisParent;
			newOptionT.localScale = new Vector3(1,1,1);
			
			InputBase inputBaseScript = newOptionGO.GetComponent<InputBase>() as InputBase;//find the uilabel script
			inputBaseScript.ChangeText(optionsArray[i]);
			
			
			
			//ResizeCollider
			DropdownInputOption3 ddInputScript = newOptionGO.GetComponent<DropdownInputOption3>() as DropdownInputOption3;
			ddInputScript.SetCallBackScript(gameObject);
			ddInputScripts[i] = ddInputScript;
			
			
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(thisParent, newOptionT);
			newOptionT.localPosition = new Vector3(-bounds.extents.x, -i * btnHeight - btnHeight/2, 0);
			
			if(maxWidth < bounds.size.x){
				maxWidth = bounds.size.x;
			}
				
		}
		
		ResizeColliders(maxWidth, ddInputScripts);
		
		return maxWidth;
		
	}
	
	private void ResizeColliders(float maxWidth, DropdownInputOption3[] ddInputScripts)
	{
		for(int i = 0;i<ddInputScripts.Length;i++){
			ddInputScripts[i].ResizeOption(maxWidth + btnHeight);
		}
	}
	
	
	private void CheckIfDDOptionClicked(Transform objectHit)
	{
	
		
		if(objectHit == null || objectHit.name != "dropdownOption3"){
			KillDD();
		}
	}
	
	private void KillDD()
	{
		
		if(currentDD != null){
			
			Destroy(currentDD);
			InputController2.OnItemDown -= CheckIfDDOptionClicked;
		
		}
		
		currentDD = null;
	}
	
	private void OnDisable()
	{
		
		KillDD();
		
	}
	
	
}
