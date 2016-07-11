using UnityEngine;
using System.Collections;

public class AdvTxt : MonoBehaviour {

	
	public static string GetTxtBetwen(string thisString, string betweenStart, string betweenEnd)
	{
		
		int startStart = thisString.IndexOf(betweenStart);
		if(startStart == -1)return "";//if there's nothing, return ""
		int startingPoint = startStart + betweenStart.Length;
		int endingPoint = thisString.IndexOf(betweenEnd,startingPoint+1);
		string newText = thisString.Substring(startingPoint, endingPoint-startingPoint);
		
		return newText;
		
	}	
	
	
}
