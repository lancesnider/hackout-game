using UnityEngine;
using System.Collections;

public class ObsSmasherCollider : MonoBehaviour {

	public string tagToFind;
	public ObsSmasher smasherScript;
	
	void Start()
	{
		if(smasherScript.isCoroutine)
			collider.enabled = false;
			enabled = false;
	}
	
	void OnTriggerEnter (Collider col)
	{
		if(col.tag == tagToFind)
			smasherScript.Smash();
	}
	
	void OnTriggerExit (Collider col)
	{
		if(col.tag == tagToFind)
			smasherScript.Lift();
	}
}
