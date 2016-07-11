using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGestureReciever : MonoBehaviour {
	
	private FingerGestures.Finger currHorizFinger;
	
	//Create Events
	public delegate void LRBtnAction(float hValue);
	public static event LRBtnAction OnLRButtonChanged;
	
	public delegate void JumpBtnAction();
	public static event JumpBtnAction OnJump;
	
	public delegate void CodeObjectAction(Transform objClicked);
	public static event CodeObjectAction CodeObjectClicked;
	
	
	
	//public vars
	public int btnSeparatorX = 100;
	public Vector2 screenHalf;
	//private string currentTName;
	
	//-----Recieve Gestures-----//
	
	//on down
	public void ItemDown(Transform thisT)
	{
		//Debug.Log("Down " + thisT.name);
		string thisName = thisT.name;
		//Debug.Log("thisTag" + thisT.tag);
		
		
		
		if(thisName == "jumpBtn"){
			if(OnJump != null)
				OnJump();
		}
	}
	
	//when a CLICK occurs
	public void ItemClicked(FingerUpEvent e, Transform thisT)
	{
		//perform hit action
		//Debug.Log("Clicked " + thisT.name);
		string thisTag = thisT.tag;
		
		
		
		if(thisTag == "fCodeObject"){
			//Debug.Log("clicked cde");
			if(CodeObjectClicked != null){
				CodeObjectClickedLocal(thisT);
			}
		}else if(thisTag == "fClick"){
			
			/*if(PlayAllCode != null){
				PlayAllCode();
			}*/
			thisT.gameObject.SendMessage("ReceiveClick");
			
			
		}
		
		if(draggingItemName != null && currentDraggingLabelFinger == e.Finger){
			
			draggingItemName = null;
			draggingItemLabelScript.KillDragger();
		}
		
		//currentTName = null;
		
		
	}
	
	private string draggingItemName;
	public Transform draggingItemT;
	public DraggingItemLabel draggingItemLabelScript;
	private FingerGestures.Finger currentDraggingLabelFinger;
	
	//when a drag is started (sometimes it's nice to have the pos before it moves
	public void ItemDragStart(FingerDownEvent e, ActiveFinger thisActiveFinger)
	{
		
		string thisHitName = thisActiveFinger.hitT.name;
		//Debug.Log("Drag Start " + e.Position);
		if(thisHitName == "leftRightController" && currHorizFinger == null){
			//Debug.Log("leftRightController " + e.Position);
			currHorizFinger = e.Finger;
		}else if(draggingItemName == null){
			currentDraggingLabelFinger = e.Finger;
			draggingItemName = thisHitName;
			draggingItemLabelScript.SetDragger(thisHitName);
		}
		
		
		//!!!!ONLY IF THIS IS SOMETHING DRAGGABLE
		//currentTName = thisActiveFinger.hitT.name;
		//Debug.Log(currentTName);
		
	}
	
	//when an item is being dragged
	public void ItemDragging(FingerMotionEvent e, ActiveFinger thisActiveFinger)
	{
		
		if(draggingItemName != null && currentDraggingLabelFinger == e.Finger){
			draggingItemT.localPosition = e.Position - screenHalf;
			//Debug.Log("Dragging " + e.Position);
		}
		
	}
	
	//when a drag is ended (sometimes it's nice to have the pos before it moves
	public void ItemDragEnd(FingerUpEvent e, ActiveFinger thisActiveFinger)
	{
		
		Transform thisT = thisActiveFinger.hitT;
		
		
		if(thisActiveFinger.hitT.name == "leftRightController"){
			currHorizFinger = null;
			if(OnLRButtonChanged != null)
				OnLRButtonChanged(0);
		}
		
		
		
	}
	
	//When a dragging item has been dropped on a different Transform
	public void DraggingItemDropped(FingerUpEvent e, ActiveFinger thisActiveFinger, Transform dropHit)
	{
		//Debug.Log("Dropped " + thisActiveFinger.hitT.name + " on " + dropHit.name);
		
		
		Transform thisT = thisActiveFinger.hitT;
		//Debug.Log("droppedsfad");
		//!!it's checking where it came from, not where it landed
		if(dropHit.tag == "ObjectInput"){
			
			//Debug.Log("this boolHIt    " + thisT.gameObject.name);
			//Debug.Log("landed on object input");
			
			//this is trying to send to dropdown
			//if(currentTName != null && currentTName != thisT.name){
			//Debug.Log(currentTName + " " + thisT.name);
			dropHit.gameObject.SendMessage("DroppedOn", thisT.name);
			//}
			
		}
		
		
		if(draggingItemName != null && currentDraggingLabelFinger == e.Finger){
			//Debug.Log("ended");
			draggingItemName = null;
			draggingItemLabelScript.KillDragger();
		}
		
		
		
	}
	
	
	public void DraggingItemDroppedOnNothing(FingerUpEvent e, ActiveFinger thisActiveFinger)
	{
		if(draggingItemName != null  && currentDraggingLabelFinger == e.Finger){
			
			draggingItemName = null;
			draggingItemLabelScript.KillDragger();
		}
	}
	
	//-----End Recieve Gestures-----//
	
	void Update()
	{
		if(currHorizFinger != null){
			
			if(currHorizFinger.Position.x < btnSeparatorX){
				if(OnLRButtonChanged != null)
					OnLRButtonChanged(-1);
			}else{
				if(OnLRButtonChanged != null)
					OnLRButtonChanged(1);
			}
			
		}
	}
	
	void CodeObjectClickedLocal(Transform codeT)
	{
		//Debug.Log("Code clicked " + codeT.name);
		if(CodeObjectClicked != null)
			CodeObjectClicked(codeT);
		//create something that follows code object in 2d space
		
	}
	
}