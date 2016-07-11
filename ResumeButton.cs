using UnityEngine;
using System.Collections;

public class ResumeButton : MonoBehaviour {

	public PauseButton pauseScript;

	public void ReceiveClick()
	{
		Debug.Log("click");
		pauseScript.SetPlayPause(true);
	}
}
