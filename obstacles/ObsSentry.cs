using UnityEngine;
using System.Collections;

public class ObsSentry : MonoBehaviour {

	public LSTurret lsTurretScript;
	public GameObject targetGO;
	
	// Use this for initialization
	void Start () {
		
		lsTurretScript.AssignTarget(targetGO);
		
	}
	
	//public void turn
	
	
	
}
