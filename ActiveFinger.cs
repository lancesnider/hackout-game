using UnityEngine;
using System.Collections;

public class ActiveFinger {

	public Vector2 fingerPos;
	public Transform hitT; 
	public bool isClick; 
	public bool isDrag;
	public bool isDrop;
	public bool isDown;

	public ActiveFinger(Vector2 newFingerPos, Transform newHitT, bool newIsClick, bool newIsDrag, bool newIsDrop, bool newIsDown)
	{
		fingerPos = newFingerPos;
		hitT = newHitT;
		isClick = newIsClick;
		isDrag = newIsDrag;
		isDrop = newIsDrop;
		isDown = newIsDown;
	}

}