using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObsDoor : MoveToPoints {

	public bool startOpen;
	private Transform openWaypoint;
	
	void Start()
	{
		
		if(startOpen){
			
			currentWp = 1;
			arrived = false;
			openWaypoint = waypoints[0];
			rigidbody.MovePosition(openWaypoint.position);
			Debug.Log("current" + currentWp);
			
			
		}
		
		enabled = false;
		
	}
	
}
