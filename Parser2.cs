#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parser2 : MonoBehaviour {
	
	/*void Start () {
		createCodeVisually();
	}*/
	
	private GameObject GOFrom;
	public Transform codeTxtParent;
	
	public GameObject boolInputPrefab;
	public GameObject codeTxtPrefab;
	public GameObject objectInputPrefab;
	//public GameObject textInputPrefab;
	public GameObject dropDownInputPrefab;
	public GameObject numberInputPrefab;
	public GameObject numberLabel;
	
	//public GameObject[] codeAlerts; //NO LONGER NEED THIS BECAUSE OF EVENTS
	
	//public int startX = 11;
	//public int startY = -19;
	public int leading = -22;
	public float startX = 11;
	private float currentX;
	public int startY= -35;
	private int currentY;
	public float numberX = 23;
	
	//private string codeCommentsColor;
	//public string numbersColor;
	//public string numberLabelsColor;
	public Transform editorBg;
	
	private List<GameObject> codeGOs = new List<GameObject>();
	
	//vars for the object icons
	public GameObject objectIcon;
	//!!!!private UISprite objectIconScript;
	public UILabel scriptNameTxt;
	
	//private bool varSizeTextInFront = false;//determines if there's variable width text in a line with stuff after it. 
	//private GameObject VariableSizeMoverGO;
	private List<MoverCode> Movers = new List<MoverCode>();
	private bool previousCodeExists;
	
	//public MovePanel MovePanelScript;
	
	void Awake()
	{
		startY = startY + (int)editorBg.localPosition.y;
		currentY = startY;
		currentX = startX;
		//codeCommentsColor = "[" + CodeColors.ltGray + "]";
		//numbersColor = numbersColor.ToString();

		
	}
	
	public void createCodeVisually(GameObject getGOFrom, string visualCode){
		
		if(previousCodeExists)
			removePreviousCode();
		
		previousCodeExists = true;
			
		GOFrom = getGOFrom;
		parseCode(visualCode);
		
	}
	
	
	//this splits the code up where it sees ~ and puts it into an array
	private void parseCode (string unparsedCode) 
	{
		
		string[] splitString = unparsedCode.Split('~');
		drawCode(splitString);
		
	}
	
	private void drawCode (string[] splitString) 
	{
		
		int currentLine = 0;
		int splitStringLength = splitString.Length;
		
		for(int i = 0;i<splitStringLength;i++){
			
			string thisString = splitString[i];
			//Debug.Log(thisString);
			
			if(thisString == "/n"/* || i == splitStringLength-1*/){
				
				//Debug.Log(thisString);
				//VariableSizeMoverGO = null;
				drawNumber(currentLine + 1);
				currentY += leading;
				currentX = startX;
				currentLine++;
				Movers.Clear();
				
				
				
			}else if(thisString[0].ToString() != "["){
				
				
				//Debug.Log(thisString);
				//if it's just text
				//Debug.Log(splitString[i]);
				createCodeTxt(splitString[i]);
				
			}else{
				
				//Debug.Log(thisString);
				//get the type
				string type = AdvTxt.GetTxtBetwen(thisString, "type='", "'");
				
				//get the associated variable
				string thisVar = AdvTxt.GetTxtBetwen(thisString, "var='", "'");
				
				//get the default input (if applicable)
				string inputDefault = AdvTxt.GetTxtBetwen(thisString, "default='", "'");
				//Debug.Log(inputDefault);
				
				if(type == "booleanInput"){
					CreateGenericInput(thisString, thisVar, inputDefault, boolInputPrefab, type);
				}else if(type == "objectInput"){
					CreateGenericInput(thisString, thisVar, inputDefault, objectInputPrefab, type);
				}else if(type == "dropDownInput"){
					CreateGenericInput(thisString, thisVar, inputDefault, dropDownInputPrefab, type);
				}
				
			}
			
		}
		
		drawNumber(currentLine + 1);
		currentX = startX;
		
	}
	
	public void removePreviousCode()
	{
		
		for(int i = 0;i<codeGOs.Count;i++){
			
			Destroy(codeGOs[i]);
			
		}
		
		currentY = startY;
		codeGOs.Clear();
		Movers.Clear();
		
		previousCodeExists = false;
		
		//Debug.Log(codeGOs.Count);
		
		
	}
	
	private void drawNumber(int lineNumber)
	{
		
		
		GameObject newNumberTxt = Instantiate(numberLabel, Vector3.zero, Quaternion.identity) as GameObject;
		Transform newNumberTxtTransform = newNumberTxt.transform;//define the text's transform
		UILabel newUILabel = newNumberTxt.GetComponent<UILabel>();//find the uilabel script
		float fontScale = newUILabel.transform.localScale.x;//set font size
		
		newUILabel.text = lineNumber.ToString();//set the text
		
		newNumberTxtTransform.parent = codeTxtParent;
		
		newNumberTxtTransform.localScale = new Vector3(fontScale,fontScale,1);
		newNumberTxtTransform.localPosition = new Vector3(numberX, currentY, 0);
		
		codeGOs.Add(newNumberTxt);
		
	}
	
	private void createCodeTxt(string codeTxt)
	{
		
		
		//create the string of code
		GameObject newCodeTxt = Instantiate(codeTxtPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		Transform newCodeTxtTransform = newCodeTxt.transform;//define the text's transform
		UILabel newUILabel = newCodeTxt.GetComponent<UILabel>();//find the uilabel script
		float fontScale = newUILabel.transform.localScale.x;//set font size
		
		
		
		//determine if there's code commenting
		int codeCommentStart = codeTxt.IndexOf("//");
		if(codeCommentStart != -1){
			codeTxt = codeTxt.Insert(codeCommentStart, CodeColors.ltGray);
			//Debug.Log(codeTxt);
		}
		
		newUILabel.text = codeTxt;//set the text
		
		//make it a child of the panel
		newCodeTxtTransform.parent = codeTxtParent;
		
		//size and position the text
		newCodeTxtTransform.localScale = new Vector3(fontScale,fontScale,1);
		newCodeTxtTransform.localPosition = new Vector3(currentX, currentY, 0);
		
		//get the width so the next item is next to it
		//currentX += newUILabel.font.CalculatePrintedSize(newUILabel.text,true,UIFont.SymbolStyle.None).x * fontScale;
		
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(codeTxtParent, newCodeTxtTransform);
		currentX += bounds.size.x;
		
		AddAsMoveable(newCodeTxtTransform);
		codeGOs.Add(newCodeTxt);
		
	}
	
	//-----create the inputs------//
	
	
	
	private void CreateGenericInput(string thisString, string thisVar, string defaultOption, GameObject inputPrefab, string thisType)
	{
		
		//define stuffs
		GameObject newInput = Instantiate(inputPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		Transform newInputTransform = newInput.transform;
		InputBase inputScript = newInput.GetComponent<InputBase>() as InputBase;
		MoverCode moverCode = newInput.GetComponent<MoverCode>() as MoverCode;
		
		//set stuff on the input
		inputScript.setColliderObject(GOFrom, thisVar);
		inputScript.OnSelectionChange(thisType, defaultOption);
		
		//special case for dropdowns
		if(inputPrefab == dropDownInputPrefab){
			//PopulateScrollbar(thisString, defaultOption);
			
			
			//DropDownInput2 DDInput2Script = newInput.GetComponent<DropDownInput3>() as DropDownInput2;
			inputScript.SetOptions(thisString, defaultOption);
			
		}
		
		//position input
		newInputTransform.parent = codeTxtParent;
		newInputTransform.localScale = new Vector3(1,1,1);
		newInputTransform.localPosition = new Vector3(currentX, currentY, 0);
		
		//get the width so the next item is next to it
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(codeTxtParent, newInputTransform);
		//Debug.Log(newInput.name + " " + bounds.size.x);
		
		moverCode.DefineSize(codeTxtParent, bounds.size.x);
		currentX += bounds.size.x;
		
		//make moveable
		AddAsMoveable(newInputTransform);
		Movers.Add(moverCode);
		codeGOs.Add(newInput);
		
	}
	
	
	private void AddAsMoveable(Transform moveable)
	{
		
		//for movers length
		for(int i = 0;i<Movers.Count;i++){
			Movers[i].AddMoveableCode(moveable);
		}
		
	}
	
}
