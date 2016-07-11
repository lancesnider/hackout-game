using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//create a list of things that would move if this was resized

public class MoverCode : MonoBehaviour {

	private Transform thisParent;
	private float currentWidth;
	private Transform thisT;
	private List<Transform> moveableStuff = new List<Transform>();
	private BoxCollider thisC;
	private float colliderHeight = 44f;
	private float leftTxtBuffer = 0.5f;
	public Transform underline; 
	public Transform UILabelT;
	//private float currentColliderW;

	void Start()
	{
		thisT = transform; 
		thisC = NGUITools.AddWidgetCollider(gameObject) as BoxCollider;
		//thisC = collider as BoxCollider;
		
		
		
		//Debug.Log(thisC.size);
		//ResizeUnderline(thisC.size.x);
		ResizeUnderline();
		//currentColliderW = thisC.size.x;
		//thisC.size = new Vector3(currentColliderW, colliderHeight, 1);
	}
	
	

	public void AddMoveableCode(Transform newMoveableItem)
	{
		moveableStuff.Add(newMoveableItem);
	}
	
	//private void ResizeUnderline(float boundsX)
	private void ResizeUnderline()
	{
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(thisParent, UILabelT);
		float boundsX = bounds.size.x;
		
		underline.localScale = new Vector3(boundsX - leftTxtBuffer *2, 1, 1);
		underline.localPosition = new Vector3(boundsX/2 + leftTxtBuffer, -11f, 0);
		
		float newBoundsX; 
		if(boundsX < colliderHeight){
			newBoundsX = colliderHeight;
		}else{
			newBoundsX = boundsX;
		}
		
		thisC.size = new Vector3(newBoundsX, colliderHeight, -0.5f);
		thisC.center = new Vector3(boundsX/2, 0, 1);
	}
	
	public void MoveAllCode()
	{
		
		//Debug.Log("old width: " + currentWidth);
		
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(thisParent, UILabelT);
		float boundsX = bounds.size.x;
		
		
		//ResizeUnderline(boundsX);
		ResizeUnderline();
		
		
		
		//Debug.Log("new width: " + boundsX);
		
		float difference = boundsX - currentWidth;
		
		//Debug.Log("difference: " + difference);
		
		
		//change the size of the collider
		//Debug.Log(difference/currentWidth);
		/*float newColliderW = currentColliderW * (1 + difference/currentWidth);
		thisC.size = new Vector3(newColliderW, colliderHeight, -0.5f);
		thisC.center = new Vector3(newColliderW/2, 0, 1);
		currentColliderW = newColliderW;*/
		
		//thisC.size = new Vector3(boundsX, colliderHeight, -0.5f);
		//thisC.center = new Vector3(boundsX/2, 0, 1);
		
		
		
		currentWidth = boundsX;
		
		//figure out before and after size
		//Debug.Log("move all code");
		for(int i = 0;i<moveableStuff.Count;i++){
			//move the difference
			moveableStuff[i].localPosition += new Vector3(difference, 0, 0);
		}
		
		
		
		
	}
	
	public void DefineSize(Transform getThisParent, float startingSize)
	{
	
		currentWidth = startingSize;
		thisParent = getThisParent;
		
		
	}
	
}
