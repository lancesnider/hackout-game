using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public delegate void PlayCodeAction();
	public static event PlayCodeAction PlayAllCode;
	
	public void ReceiveClick()
	{
		PlayAllCode();
	}
	
}
