using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObsSmasher : MonoBehaviour {

	/*
		smash, wait, lift, wait, 
		smash speed, wait times, lift speed
		coroutines or distance based (maybe with invisible area)
		
		
	*/
	public Transform smasher;
	public GameObject smasherGO;
	public Collider coll;
	public Transform topWaypoint;
	public Hazard hazardScript;
	
	private GameObject thisGO;
	public bool isCoroutine;
	public float liftSpeed;
	public float liftedWaitTime;
	public float smashSpeed;
	public float smashedWaitTime;
	public bool smashImmediately;
	
	private Vector3 posGround;
	private Vector3 posHover;

	// Use this for initialization
	void Start () {
		
		//get waypoint positions
		thisGO = gameObject;
		posHover = topWaypoint.position;
		posGround = smasher.position;
		
		//move to up position
		smasher.position = posHover;
		
		//if it's coroutine, start waiting
		if(isCoroutine){
			
			if(smashImmediately){
				Smash();
			}else{
				StartCoroutine(LiftedWait());
			}
			
		}
		
	}
	
	void Lifted()
	{
		//Debug.Log("lifted");
		if(isCoroutine)
			StartCoroutine(LiftedWait());
	}
	
	IEnumerator LiftedWait()
	{
		yield return new WaitForSeconds(liftedWaitTime);
		//Debug.Log("lift waiting");
		Smash();
	}
	
	public void Smash()
	{
		hazardScript.damage = 100;
		//Debug.Log("Smash");	
		iTween.MoveTo(smasherGO, iTween.Hash("position", posGround, "speed", smashSpeed, "easetype", "easeOutBounce", "oncomplete", "Smashed", "oncompletetarget", thisGO));
	}
	
	void Smashed()
	{
		hazardScript.damage = 0;
		//Debug.Log("smashed");
		if(isCoroutine)
			StartCoroutine(SmashedWait());
			
	}
	
	IEnumerator SmashedWait()
	{
		yield return new WaitForSeconds(smashedWaitTime);
		Lift();
	}
	
	public void Lift()
	{
		//Debug.Log("lift");	
		iTween.MoveTo(smasherGO, iTween.Hash("position", posHover, "speed", liftSpeed, "oncomplete", "Lifted", "oncompletetarget", thisGO));
	}

	
	
	//draw gizmo spheres for waypoints
	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		foreach (Transform child in transform)
		{
			if(child.tag == "Waypoint")
				Gizmos.DrawSphere(child.position, .7f);
		}
	}
	
}
