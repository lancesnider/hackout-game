using UnityEngine;
using System.Collections;

public class Attackable : MonoBehaviour {
	
	public DealDamage dealDamageScript;
	private GameObject thisGO;
	float hitForce = 10f;
	
	// Use this for initialization
	void Start () {
		thisGO = gameObject;
	}
	
	// Update is called once per frame
	public void On_HitByTurret(LSHit hit)
	{
		//!!!!just call this to health
		//Debug.Log("hit me!!!" + hit.power);
		int hitPower = (int)hit.power;
		//GameObject victim, int dmg, float pushHeight, float pushForce
		dealDamageScript.Attack(thisGO, hitPower, 0, hitPower/2, hit.owner.transform);
	
	}
}
