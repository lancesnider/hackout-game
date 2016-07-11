using UnityEngine;
using System.Collections;

public class DraggingItemLabel : MonoBehaviour {

	public Transform underline;
	public Transform UILabelT;
	public UILabel UILabelScript;
	private Transform thisT;
	private float leftTxtBuffer = 0.5f;
	public Transform centerer;
	public Vector2 disabledPosition;
	public float centererY = 56f;
	
	void Start()
	{
		thisT = transform;
	}
	
	public void SetDragger (string draggerName) 
	{
		UILabelScript.text = draggerName;
		ResizeUnderline();
	}
	
	public void KillDragger () 
	{
		thisT.position = disabledPosition;
	}
	
	private void ResizeUnderline()
	{
	
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(thisT, UILabelT);
		float boundsX = bounds.size.x;
		
		centerer.localPosition = new Vector3(-boundsX/2, centererY, 0);
		
		underline.localScale = new Vector3(boundsX - leftTxtBuffer *2, 1, 1);
		underline.localPosition = new Vector3(boundsX/2 + leftTxtBuffer, -11f, 0);
	}
	
}
