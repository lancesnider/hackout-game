using UnityEngine;
using System.Collections;

public class CodePanelPosition : MonoBehaviour {

	public GameObject codePanelGO;
	private Transform codePanelT;
	public Transform arrowT;
	public Camera camera3d;
	public PlayerMove playerScript;
	public Parser2 parserScript;
	
	public UIFollowTarget followScript;
	public float screenW = 1024;
	public float screenH = 768;
	private float screenHalfW;
	private float screenHalfH;
	
	public float codeWindowW = 480;
	public float codeWindowH = 320;
	private bool codePanelEnabled;
	public Vector2 codePanelInactivePos = new Vector2(0, 2000);
	
	public int codePanelDist = 30;
	public int arrowCenter = 15;
	
	private Transform thisT;
	private string currentXQuad; 
	
	/*
		Walls - Preventing the code panel from covering important UI elements
		define where the walls are for each one (based on position of large buttons)
		By default the walls are simply the edges of the screen
		The left x quad, for example, has a floor about 100px away from the edge because of the left/right buttons
		
		top - none
		right - bottom
		bottom - left, right
		left - top, bottom
	*/
	
	//This is the DIFFERENCE from the default. 
	// {top, right, bottom, left}
	
	public float[] defaultWalls = new float[4] {384, 512, -384, -512};
	public float[] topWalls = new float[4] {384, 512, 0, -512};
	public float[] rightWalls = new float[4] {384, 512, -224, 0};
	public float[] bottomWalls = new float[4] {0, 312, -384, -252};
	public float[] leftWalls = new float[4] {314, 0, -224, -512};

	void OnEnable()
	{
		UIGestureReciever.CodeObjectClicked += CodeObjectClicked;
		PlayButton.PlayAllCode += CodePanelDisable;
	}
	
	void OnDisable()
	{
		UIGestureReciever.CodeObjectClicked -= CodeObjectClicked;
		PlayButton.PlayAllCode -= CodePanelDisable;
	}
	
	// Use this for initialization
	void Start () {
		codePanelT = codePanelGO.transform;
		screenHalfW = screenW/2;
		screenHalfH = screenH/2;
		thisT = transform;
		
		//string whichXQuad = XQuadrant.ReturnXQuadrant(followScript.posV2);
		//QuadChanged(whichXQuad);
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		/*if(followScript.enabled == true){
			string whichXQuad = XQuadrant.ReturnXQuadrant(followScript.posV2);
			if(currentXQuad != whichXQuad)
				QuadChanged(whichXQuad);
		}*/
		if(codePanelEnabled){
			MoveCodePanel(followScript.posV2);
		}
		
	}
	
	void CodeObjectClicked(Transform objClicked)
	{
	
		followScript.target = objClicked;
		followScript.enabled = true;
		codePanelEnabled = true;
		
		InputController2.OnItemDown += CodePanelDisableCheckHit;
		
		//tell it to create code!!!!
		objClicked.gameObject.SendMessage("createVisualCode", SendMessageOptions.DontRequireReceiver);
		//Debug.Log(objClicked.gameObject);
		
		//Vector3 pos3d = camera3d.WorldToViewportPoint(objClicked.position);
		Vector3 pos3d = followScript.PerformUpdate();
		//Debug.Log(followScript.posV2 + " " + pos3d);
		
		string whichXQuad = XQuadrant.ReturnXQuadrant(pos3d);
		if(currentXQuad != whichXQuad)
			QuadChanged(whichXQuad);
		
	}
	
	void CodePanelDisable()
	{
		
		followScript.enabled = false;
		codePanelEnabled = false;
		InputController2.OnItemDown -= CodePanelDisableCheckHit;
		followScript.target = null;
		//WHY isn't this the same location each time?
		//maybe need to reset the panel position or move it further away?
		thisT.localPosition = codePanelInactivePos;
		currentXQuad = null;
		parserScript.removePreviousCode();
		
	}
	
	void CodePanelDisableCheckHit(Transform itemHit)
	{
		
		
		if(itemHit == null){
			CodePanelDisable();
		}
		
		/*if(itemHit.tag == null || itemHit.tag != "fCodeObject"){
			CodePanelDisable();
		}*/
	}
	
	
	void QuadChanged(string xQuad)
	{
		currentXQuad = xQuad;
		//Debug.Log("X Quadrant: " + xQuad);
		
		//move the position of the code editor based on xquad
		Vector2 panelV2;
		Vector2 arrowV2;
		Vector3 arrowRot;
		
		if(xQuad == "top"){
			panelV2 = new Vector3(0, -codeWindowH/2 - codePanelDist);
			arrowV2 = new Vector3(0,  -codePanelDist + arrowCenter);
			arrowRot = new Vector3(0,0,180);
		}else if(xQuad == "right"){
			panelV2 = new Vector3(-codeWindowW/2 - codePanelDist, 0);
			arrowV2 = new Vector3(-codePanelDist + arrowCenter, 0);
			arrowRot = new Vector3(0,0,90);
		}else if(xQuad == "bottom"){
			panelV2 = new Vector3(0, codeWindowH/2 + codePanelDist);
			arrowV2 = new Vector3(0, codePanelDist - arrowCenter);
			arrowRot = new Vector3(0,0,0);
		}else{
			panelV2 = new Vector3(codeWindowW/2 + codePanelDist, 0);
			arrowV2 = new Vector3(codePanelDist - arrowCenter, 0);
			arrowRot = new Vector3(0,0,270);
		}
		
		codePanelT.localPosition = panelV2;
		arrowT.localPosition = arrowV2;
		arrowT.localEulerAngles = arrowRot;
		
	}
	
	
	
	void MoveCodePanel(Vector2 objPos)
	{
		
		//float xPos = objPos.x;
		//float yPos = objPos.y;
		
		
		
		//Debug.Log(objPos);
		float xPos = objPos.x;
		float yPos = objPos.y;
		float newCodePosXY; 
		Vector2 newCodePos;
		
		//!!!!maybe check if it's grounded?
		if(Mathf.Abs(xPos) > screenHalfW){
			CodePanelDisable();
			return;
		}
		
		if(Mathf.Abs(yPos) > screenHalfH){
			CodePanelDisable();
			return;
		}
		
		//float wallGreater;
		//float wallLesser;
		//float distanceBetweenWalls;
		//float centerBetweenWalls;
		//float worldToLocalPos;
		//float posPerc = 0;
		
		//!!!!check to see if the code panel would be off the screen
		
		if(currentXQuad == "top"){
			
			if(thisT.localPosition.y - codeWindowH - codePanelDist < -screenHalfH){
				string whichXQuad = XQuadrant.ReturnXQuadrant(objPos);
				QuadChanged(whichXQuad);
				return;
			}
			
			//if(yPos > bottomWalls[3]){
				newCodePosXY = GetNewCodePanelPos(objPos, defaultWalls[3], defaultWalls[1], xPos, codeWindowW);
			//}else{
				//newCodePosXY = GetNewCodePanelPos(objPos, topWalls[3], topWalls[1], xPos, codeWindowW);
			//}
			newCodePos = new Vector2(-newCodePosXY, codePanelT.localPosition.y);
			
		}else if(currentXQuad == "right"){
			
			if(thisT.localPosition.x - codeWindowW - codePanelDist < -screenHalfW){
				string whichXQuad = XQuadrant.ReturnXQuadrant(objPos);
				QuadChanged(whichXQuad);
				return;
			}
			
			//if(yPos > rightWalls[0]){
				newCodePosXY = GetNewCodePanelPos(objPos, defaultWalls[0], defaultWalls[2], yPos, codeWindowH);
			//}else{
			//	newCodePosXY = GetNewCodePanelPos(objPos, rightWalls[0], rightWalls[2], yPos, codeWindowH);
			//}
			
			newCodePos = new Vector2(codePanelT.localPosition.x, newCodePosXY);
			
		}else if(currentXQuad == "bottom"){
			
			if(thisT.localPosition.y + codeWindowH + codePanelDist > screenHalfH){
				string whichXQuad = XQuadrant.ReturnXQuadrant(objPos);
				QuadChanged(whichXQuad);
				return;
			}
			
			//!!!!only CHANGE if grounded maybe?
			
			//if(yPos > bottomWalls[3]){
				newCodePosXY = GetNewCodePanelPos(objPos, defaultWalls[3], defaultWalls[1], xPos, codeWindowW);
			//}else{
				//newCodePosXY = GetNewCodePanelPos(objPos, bottomWalls[3], bottomWalls[1], xPos, codeWindowW);
			//}
			
			
			newCodePos = new Vector2(-newCodePosXY, codePanelT.localPosition.y);
			
		}else{
			//"left"
			
			//!!!!check to see if the code panel is out of bounds
			//Debug.Log("codepos " + thisT.localPosition);
			if(thisT.localPosition.x + codeWindowW + codePanelDist > screenHalfW){
				string whichXQuad = XQuadrant.ReturnXQuadrant(objPos);
				QuadChanged(whichXQuad);
				return;
			}
			
			//if(yPos > leftWalls[0]){
				newCodePosXY = GetNewCodePanelPos(objPos, defaultWalls[0], defaultWalls[2], yPos, codeWindowH);
			//}else{
				//newCodePosXY = GetNewCodePanelPos(objPos, leftWalls[0], leftWalls[2], yPos, codeWindowH);
			//}
			
			//newCodePosXY = GetNewCodePanelPos(objPos, leftWalls[0], leftWalls[2], yPos, codeWindowH);
			newCodePos = new Vector2(codePanelT.localPosition.x, newCodePosXY);
			
		}
		
		codePanelT.localPosition = newCodePos;
		
	}
	
	float GetNewCodePanelPos(Vector2 objPos, float wallLesser, float wallGreater, float xOrYPos, float codeWindowWorH)
	{
	
		float distanceBetweenWalls = wallGreater - wallLesser;
		float centerBetweenWalls = (wallGreater + wallLesser)/2;
		
		float worldToLocalPos = xOrYPos + distanceBetweenWalls/2 - centerBetweenWalls;
		float posPerc = worldToLocalPos/distanceBetweenWalls * 2 - 1;
		posPerc = IsOutOfBounds(posPerc, objPos);//make sure it's in bounds
		
		float codeMaxMove = codeWindowWorH/2 - arrowCenter;
		//float codeMoveTimesPerc = codeMaxMove * posPerc;
		return codeMaxMove * posPerc;
		
		//Debug.Log("new " + codeMoveTimesPerc);
	
	}
	
	//if the code panel gets away from the arrow
	float IsOutOfBounds(float posPerc, Vector2 objPos)
	{	
	
		if(Mathf.Abs(posPerc) > 1 ){ 
			//don't switch stuff if the player is just jumping
			if(!playerScript.IsGrounded()){
				if(posPerc > 1){
					posPerc = 1;
					return posPerc;
				}else if(posPerc < -1){
					posPerc = -1;
					return posPerc;
				}
			}else{
				//reposition it since the player has actually moved
				//string whichXQuad = XQuadrant.ReturnXQuadrant(objPos);
				//QuadChanged(whichXQuad);
				//CodePanelDisable();
			}
		}
		
		return posPerc;
	}
	
}
