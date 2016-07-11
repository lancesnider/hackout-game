using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

	//public delegate void PlayPauseAction(bool isPlaying);
	//public static event PlayPauseAction PlayPause;

	private bool isPlaying = true; 
	public Transform pauseMenuT; 
	public Transform bgMenuT;
	public Vector2 inactivePos; 

	void OnEnable()
	{
		SetPlayPause(true);
	}

	public void ReceiveClick()
	{
		
		//!!!!
		//remove the delates. Instead, just tell inputcontroller2 that it's paused. It should ignore inputs that aren't pause or whatever main menu. 
		//alternate option: Just cover everything with a dark bg so nothing is clickable. 
		
		Debug.Log(isPlaying);
		if(isPlaying){
			SetPlayPause(false);
		}else{
			SetPlayPause(true);
		}
		
	}
	
	public void SetPlayPause(bool isPlay)
	{
		isPlaying = isPlay;
		if(isPlay){
			Time.timeScale = 1;
			MoveMenu(pauseMenuT, inactivePos);
			MoveMenu(bgMenuT, inactivePos);
		}else{
			Time.timeScale = 0;
			MoveMenu(pauseMenuT, new Vector2(0,0));
			MoveMenu(bgMenuT, new Vector2(0,0));
		}
	}
	
	private void MoveMenu(Transform itemToMove, Vector2 moveToV2)
	{
		itemToMove.localPosition = moveToV2;
	}
}
