using UnityEngine;
using System.Collections;

public class ObsRotater : MonoBehaviour {

	public GameObject pivotGO;
	private Transform pivotT;
	public float rotationSpeed = 2;
	
	public bool isCoroutine;
	public float rotateAddAmount;
	public bool startImmediately;
	public float waitTime = 1;
	private GameObject thisGO;
	
	// Use this for initialization
	void Start () {
		
		pivotT = pivotGO.transform;
		thisGO = gameObject;
		
		//RotateTo(5);
		//RotateAdd(30);
		if(isCoroutine){
			
			if(startImmediately){
				RotateAdd();
			}else{
				StartCoroutine(WaitToRotate());
			}
			
			
		}
		
	}
	
	public IEnumerator WaitToRotate()
	{
		
		yield return new WaitForSeconds(waitTime);
		RotateAdd();
		
	}
	
	public void RotateTo(float newAngle)
	{
		iTween.RotateTo(pivotGO, iTween.Hash("z", newAngle, "speed", rotationSpeed, "includechildren", true));
	}
	
	public void RotateAdd()
	{
		iTween.RotateAdd(pivotGO, iTween.Hash("z", rotateAddAmount, "speed", rotationSpeed, "includechildren", true, "oncomplete", "RotateAddComplete", "oncompletetarget", thisGO));
	}
	
	public void RotateAddComplete()
	{
		if(isCoroutine){
			StartCoroutine(WaitToRotate());
		}
	}
	
	
}
