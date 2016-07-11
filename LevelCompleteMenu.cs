using UnityEngine;
using System.Collections;

public class LevelCompleteMenu : MonoBehaviour {

	public UISprite[] starGraphicScript;
	public LoadNewScene loadSceneScript;
	
	
	public Color starActiveColor;
	public Color starInactiveColror;
	
	public void SetStars(int starCount)
	{
		
		for(int i=0;i<3;i++){
			
			if(starCount <= i){
				starGraphicScript[i].color = starActiveColor;
			}else{
				starGraphicScript[i].color = starInactiveColror;
			}
			
		}
		
		
	}
	
	public void SetNextLevel(string nextName)
	{
		if(nextName != null){
			loadSceneScript.SetLevel(nextName);
		}	
			
	}
	
}
