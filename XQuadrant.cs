using UnityEngine;
using System.Collections;

/// <summary>
/// 	This is how you call it
/// 	string whichXQuad = XQuadrant.ReturnXQuadrant(new Vector2(400, 700));
/// </summary>

public class XQuadrant : MonoBehaviour {
	
	private static float halfScreenW = 512;
	private static float halfScreenH = 386;
	private static float tanOfAngle = .75f;
	
	//figure out which part of the x quadrant we're in
	public static string ReturnXQuadrant(Vector2 screenPos)
	{
		
		//find the lenght, width of the horizontal, vertical side side of the triangle
		//!!THIS IS WRONG!!
		float hSideLength = screenPos.x;
		//Debug.Log("hSideLength: " + hSideLength);
		//!!THIS IS WRONG!!
		float vDistanceFromCenter = screenPos.y;
		//Debug.Log("AhSideLength " + hSideLength + " vDistanceFromCenter " + vDistanceFromCenter);
		
		//find the height of the vertical side
		//!!!!WHY IS THIS ALWAYS 0?
		float vSideHeight = tanOfAngle * hSideLength;
		//Debug.Log("Opposite height " + vSideHeight);
		
		//absolute distance between triangle O side and v dist from center
		float absDistance = Mathf.Abs(vDistanceFromCenter) - Mathf.Abs(vSideHeight);
		//Debug.Log("vSideHeight: " + Mathf.Abs(vSideHeight) + "; vDistanceFromCenter: " + Mathf.Abs(vDistanceFromCenter));
		
		if(absDistance >= 0){
			if(vDistanceFromCenter >= 0){
				return "top";
			}else{
				return "bottom";
			}
			
		}else{
			if(hSideLength >= 0){
				return "right";
			}else{
				return "left";
			}
		
		}
		
	}
	
}
