using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour {
	
	public Vector3 deathThresholds;
	public Vector3 codeCountThresholds;
	
	public Transform finishedMenuT; 
	public Transform bgMenuT;
	public Vector2 inactivePos; 
	
	public LevelCompleteMenu levelCompleteMenuScript;
	public UILabel loadNewLevelLabel;
	
	private int deathCount;
	private int codeRunCount;
	
	void OnEnable()
	{
		Health.OnDeath += AddDeath;
		PlayButton.PlayAllCode += AddCodeRun;
		
		
	} 
	
	void OnDisable()
	{
		Health.OnDeath -= AddDeath;
		PlayButton.PlayAllCode -= AddCodeRun;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player"){
		
			
			
			Debug.Log("total deaths: " + deathCount);
			//Debug.Log("total codes: " + codeRunCount);
			//Debug.Log("Stars: " + DetermineStars());
			
			MoveMenu(finishedMenuT, new Vector2(0,0));
			MoveMenu(bgMenuT, new Vector2(0,0));
			
			int starsEarned = DetermineStars();
			
			
			levelCompleteMenuScript.SetStars(starsEarned);
			
			//save level complete, stars on that level
			string thisLevelName = Application.loadedLevelName;
			string nextLevelName = GetSaveData.SetLevelComplete(thisLevelName, starsEarned, loadNewLevelLabel);
			//Debug.Log(nextLevelName);
			//if(nextLevelName != null)
			levelCompleteMenuScript.SetNextLevel(nextLevelName);

			//loadNewLevelLabel.SetNextLevel();
			
			collider.enabled = false;
			
		}
		
	}
	
	private int DetermineStars()
	{
		int starCount; 
		
		//check deaths
		if(deathCount <= deathThresholds.x){
			starCount = 3;
		}else if(deathCount <= deathThresholds.y){
			starCount = 2;
		}else{
			return 1;
		}
		
		//Debug.Log("death star" + starCount);
		
		//check code
		if(codeRunCount <= codeCountThresholds.x && starCount == 3){
			return 3;
		}else if(codeRunCount <= codeCountThresholds.y && starCount >= 2){
			return 2;
		}
		
		return 1;
	}
	
	private void MoveMenu(Transform itemToMove, Vector2 moveToV2)
	{
		itemToMove.localPosition = moveToV2;
	}
	
	private void AddDeath()
	{
		deathCount++;
	}
	
	private void AddCodeRun()
	{
		codeRunCount++;
	}
}
