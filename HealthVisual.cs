using UnityEngine;
using System.Collections;

public class HealthVisual : MonoBehaviour {

	public UILabel thisLabelScript; 
	private string healthPrefix = " health = ";
	
	// Use this for initialization
	void Start () {
		
		SetHealth(66);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetHealth(int currentHealth)
	{
		Debug.Log(currentHealth);
		
		float currentHealthFloat = (float)currentHealth/100;
		
		thisLabelScript.text = healthPrefix + currentHealthFloat.ToString("0.0");;
		
	}
}
