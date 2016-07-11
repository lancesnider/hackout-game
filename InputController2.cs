using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*

	//tags needed
	codeObject
	interactiveObject
	click
	dragDrop
	drag

	//types of buttons
	UI Buttons
		Left right controls - drag
		Click (jump, pause) - click
	Code buttons
		Scroll entire view - drag
		Number - drag
		Dropdown - click, click
		Bool - click
		Object - click (to view related obj)
	3d Scene
		Code object - click
		Interactive object (including code objects) - drag

	//types of actions
	click
		Click and release over same object
		Irrelevant to 
			Non-interactive code object
			Scroll
			Number
	click & drag
		Irrelevant to 
			Bool
			Object
			Dropdown

	Long click is okay for all types

	Factors to differentiate
		Movement distance 
			Wait until 5 px until you call it a drag
			Drag can still end up being click if over same GO

	Which drags don't need drops over GO (example, x pos is fine)?
		left, right controls
		Scroll code view
		Number
	Which do need a specific drop over GO
		Interactive 3d object

	FINAL
		1. Determine initial pos
			Vector2, Finger, Object Over
		2. If it's draggable, look for it to move
			Finger
		3. Determine position as it moves
			Vector2, Finger
		4. Determine where it drops
			Vector2, Finger, Object Over

	Types of finger interaction
		Tap - 1, 4
		Drag & drop - 1, 2, 3, 4
		Drag - 1, 2, 3

	What info do I need to know to keep track of this stuff





	//differences between click, drag
	time
	movement
	release on self
	
	
*/

/*

	1. Determine initial pos
		Vector2, Finger, Object Over
	2. If it's draggable, look for it to move
		Finger
	3. Determine position as it moves
		Vector2, Finger
	4. Determine where it drops
		Vector2, Finger, Object Over

*/

public class InputController2 : MonoBehaviour {

	/*
	
		//--types of buttons
		UI Buttons
			Left right controls - drag
			Click (jump, pause) - click
		Code buttons
			Scroll entire view - drag
			Number - drag
			Dropdown - click, click
			Bool - click
			Object - click (to view related obj)
		3d Scene
			Code object - click
			Interactive object (including code objects) - drag
		
		//--tags
		fCodeObject 			click, drag, drop
		fInteractiveObject 		drag, drop
		fClick					click, drop
		fDragDrop				drag, drop
		fDrag					drag
		
	*/
	
	//cameras
	public Camera camera2d; 
	public Camera camera3d; 

	//fingers dictionary
	private Dictionary<FingerGestures.Finger, ActiveFinger> ActiveFingers = new Dictionary<FingerGestures.Finger, ActiveFinger>();
	
	public UIGestureReciever myGestureReciever;
	
	//tag groups
	public string[] clickTags;
	public string[] dragTags;
	public string[] dropTags;
	public string[] downTags;
	
	public LayerMask hackOut3d;
	
	public delegate void NothingClickedAction();
	public static event NothingClickedAction NothingClicked;
	
	//public delegate void SomethinggClickedAction(Transform objectHit);
	//public static event SomethinggClickedAction OnSomethingClicked;
	
	void Awake()
	{
		//register detectors (down, up, move)
		//Register down finger event
		FingerDownDetector downDetector = gameObject.AddComponent<FingerDownDetector>();
		downDetector.OnFingerDown += DetectorOnFingerDown;
		//Register up finger envent
		FingerUpDetector upDetector = gameObject.AddComponent<FingerUpDetector>();
		upDetector.OnFingerUp += DetectorOnFingerUp;
		//register move
		FingerMotionDetector moveDetector = gameObject.AddComponent<FingerMotionDetector>();
		moveDetector.OnFingerMove += DetectorOnFingerMove;

	}

	//-----Main Detection (up, down, move)-----//
	
	public delegate void ItemDownAction(Transform objectHit);
	public static event ItemDownAction OnItemDown;
	
	// Finger down
	void DetectorOnFingerDown(FingerDownEvent e)
	{
		//List - Add Finger, Finger.Position, Object Hit, Object tag
		//create new ActiveFinger
		//Add active finger to ActiveFingers with Finger as key
		Vector2 fingerPos = e.Position;
		Transform objectHit = GetObjectHit(fingerPos);
		//OnSomethingClicked(objectHit);
		
		if(OnItemDown != null){
			OnItemDown(objectHit);
		}
		
		

		//if it hit something, register it
		if(objectHit){
		
			string hitTag = objectHit.tag;
			//Debug.Log("was hit: " + hitTag);
			
			//check to see if it's tagged pause!!!!
			
			if(hitTag != "Untagged"){
				
				
				
				//determine whether this one needs to listen to click, drags, or drops
				bool clickTrue = FindInArray(clickTags, hitTag);
				bool dragTrue = FindInArray(dragTags, hitTag);
				bool dropTrue = FindInArray(dropTags, hitTag);
				bool downTrue = FindInArray(downTags, hitTag);
				
				//register finger
				ActiveFinger newActiveFinger = new ActiveFinger(fingerPos, objectHit, clickTrue, dragTrue, dropTrue, downTrue);
				ActiveFingers.Add(e.Finger, newActiveFinger);
				
				if(dragTrue){
					myGestureReciever.ItemDragStart(e, newActiveFinger);
				}else if(downTrue){
					myGestureReciever.ItemDown(objectHit);
				}else if(dropTrue){
					//myGestureReciever.ItemDown(objectHit);
				}
			}
			
		}else{
			if(NothingClicked != null)
				NothingClicked();
		}

	}

	//Finger up
	void DetectorOnFingerUp(FingerUpEvent e)
	{
		//if drag and drop OR Click
			//Register Finger, Finger.Position, Object Hit
			//if start, stop on same Object Hit
				//ItemClicked();
		
		FingerGestures.Finger thisFinger = e.Finger;
		
		ActiveFinger thisActiveFinger;
		if(ActiveFingers.TryGetValue(thisFinger, out thisActiveFinger)){
		
			//Debug.Log("found");
			
			if(thisActiveFinger.isDrop){
				
				//get the hit on drop
				Transform dropHit = GetObjectHit(e.Position);
				if(dropHit == thisActiveFinger.hitT){
					//this is a click
					//Debug.Log("clicked");
					myGestureReciever.ItemClicked(e, thisActiveFinger.hitT);
				}else if(dropHit != null){
					//this got droped on something
					myGestureReciever.DraggingItemDropped(e, thisActiveFinger, dropHit);
				}else{
					myGestureReciever.DraggingItemDroppedOnNothing(e, thisActiveFinger);
				}
				
			}else{
				
				myGestureReciever.ItemDragEnd(e, thisActiveFinger);
				
			}
			
		}
		
		//Remove Finger from List
		ItemLetGo(e.Finger);
	}

	//Finger move
	void DetectorOnFingerMove(FingerMotionEvent e)
	{
		
		//make sure it's in the array
		FingerGestures.Finger thisFinger = e.Finger;
		ActiveFinger thisActiveFinger;
		if(ActiveFingers.TryGetValue(thisFinger, out thisActiveFinger)){
			
			//determine if it's draggable
			if(thisActiveFinger.isDrag){
				
				myGestureReciever.ItemDragging(e, thisActiveFinger);
			}
			
		}
	}

	//-----Gestures (clicked, dropped, let go, dragging)-----//
	
	/*
	
	//when a CLICK occurs
	void ItemClicked(Transform thisT)
	{
		//perform hit action
		Debug.Log("Clicked " + thisT.name);
	}
	
	//when an item is being dragged
	void ItemDragging(FingerMotionEvent e, ActiveFinger thisActiveFinger)
	{
		Debug.Log("Dragging " + e.Position);
	}
	
	//When a dragging item has been dropped on a different Transform
	void DraggingItemDropped(FingerUpEvent e, ActiveFinger thisActiveFinger, Transform dropHit)
	{
		Debug.Log("Dropped " + thisActiveFinger.hitT.name + " on " + dropHit.name);
	}
	
	*/

	//-----Bear stuffs-----//
	
	void ItemLetGo(FingerGestures.Finger fingerToDrop)
	{
		ActiveFingers.Remove(fingerToDrop);
	}

	Transform GetObjectHit(Vector2 fingerPos)
	{

		//check the 2d scene first

		//determine what you clicked on
		Ray ray2d = camera2d.ScreenPointToRay(fingerPos);
		RaycastHit hit2d;
		
		//see if it hits something in the 2d scene
		if (Physics.Raycast(ray2d, out hit2d, 100)){

			return hit2d.transform;
			
		}

		//check the 3d scene
		//!!!!!! DO THIS
		
		//camera3d
		Ray ray3d = camera3d.ScreenPointToRay(fingerPos);
		RaycastHit hit3d;
		
		if(Physics.Raycast(ray3d, out hit3d, Mathf.Infinity, hackOut3d)){
			//Debug.Log(hit3d.transform.name);
			return hit3d.transform;
		}
		
		/*
		
		
		Ray ray = gameCam.ScreenPointToRay(currTouchPos);
		RaycastHit hit;
		bool hitSceneCollider = false;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, layer3d)){
			hitSceneCollider = true;
		}
		
		*/

		return null;
	}

	bool FindInArray(string[] arrayToCheck, string toMatch)
	{
	
		for(int i = 0;i<arrayToCheck.Length;i++){
			if(arrayToCheck[i] == toMatch)
				//Debug.Log("found");
				return true;
		}
		
		return false;
		
	}

}
