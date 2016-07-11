using UnityEngine;
using System.Collections;

public class GetSaveData : MonoBehaviour {

	private static string[] levelNames;
	private static int currentLastAccessableLevel = 0;
	
	/*
		SetLevelNames(string[] getLevelNames)
		SetLevelComplete(string levelName, int starsEarned)
		GetLastAccessableLevel()
	*/
	
	//this gets all the level names in order from the level select menu. 
	//it associates names with numbers. 
	//this is run on start. 
	public static void SetLevelNames(string[] getLevelNames)
	{
		
		//------------TESTING-------------//
		//this'll reset the player prefs on last complete level
		//PlayerPrefs.SetInt("lastAccessableLevel", 0);
		//PlayerPrefs.DeleteKey("zTest06StarsEarned");
		
		if(levelNames == null)
			levelNames = getLevelNames;
			
	}
	
	//this is run when a level is done
	public static string SetLevelComplete(string levelName, int starsEarned, UILabel loadNewLevelLabel)
	{
		
		int newAccessableLevel = 0;
		
		//break this if there are no level names (this should only happen when testing)
		if(levelNames == null)
			return "";
		
		//find this level name in the array, get the number
		for(int i = 0; i < levelNames.Length; i++){
		
			if(levelNames[i] == levelName)
				newAccessableLevel = i + 1;
				
		}
		
		//set the stars earned for this level
		//create a new key name based on level name
		string levelStarName = levelName + "StarsEarned";
		CheckExistsReturnLargerInt(levelStarName, starsEarned);
		
		if(newAccessableLevel <= levelNames.Length){
			loadNewLevelLabel.text = "LoadLevel([c46e28]" + newAccessableLevel + "[-]);";
			//see if the new level completed is the highest
			currentLastAccessableLevel = CheckExistsReturnLargerInt("lastAccessableLevel", newAccessableLevel);
			
			//return the name of the new level
			string newLevelName = levelNames[newAccessableLevel];
			return newLevelName;
		}
		
		
		
		return null;
		
		
		
	}
	
	public static int GetNextLevelInt(string levelName)
	{
		
		return 0;
		
	}
	
	public static int GetLastAccessableLevel()
	{
		
		//see if it's been set
		if(PlayerPrefs.HasKey("lastAccessableLevel")){
			
			return PlayerPrefs.GetInt("lastAccessableLevel");
			
		}
		
		//if it hasn't been set, set it and return 0
		PlayerPrefs.SetInt("lastAccessableLevel", 0);
		
		return 0;
		
	}
	
	public static int GetStars(string levelName)
	{
	
		string levelStarName = levelName + "StarsEarned";
		
		if(PlayerPrefs.HasKey(levelStarName)){
			
			return PlayerPrefs.GetInt(levelStarName);
			
		}
		
		return 0;
	
	}
	
	private static int CheckExistsReturnLargerInt(string keyName, int newInt)
	{
		
		
		int existingInt = 0;
		
		//see if it already exists
		if(PlayerPrefs.HasKey(keyName)){
		
			existingInt = PlayerPrefs.GetInt(keyName);
			
		}
		
		//see if the new or old is bigger
		//if it's the new, set the playerpref
		if(existingInt < newInt){
			
			Debug.Log(keyName + " --- " + newInt);
			PlayerPrefs.SetInt(keyName, newInt);
			return newInt;
			
		}
		
		//keep the old
		return existingInt;
	
	}
	
	
	
	
}
