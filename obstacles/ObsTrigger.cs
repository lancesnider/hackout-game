using UnityEngine;
using System.Collections;

public class ObsTrigger : MonoBehaviour {

	public string tagTrigger; 
	public bool isTriggered;
	
	public bool useSendMessage; 
	public GameObject sendMessageGO;
	public string functionName;
	
	void OnTriggerEnter(Collider other) {
		
		if(other.tag == tagTrigger){
			SendTriggerMsg(true);
		}
		
	}
	
	void OnTriggerExit(Collider other) {
		
		if(other.tag == tagTrigger){
			SendTriggerMsg(false);
		}
		
	}
	
	private void SendTriggerMsg(bool getIsTriggered)
	{
	
		isTriggered = getIsTriggered;
		
		if(useSendMessage)	
			sendMessageGO.SendMessage(functionName, isTriggered, SendMessageOptions.DontRequireReceiver);
		
	}
	
}
