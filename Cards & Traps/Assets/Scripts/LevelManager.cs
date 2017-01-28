using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadNextLevel(){
		Application.LoadLevel (Application.loadedLevel + 1);
	}
	public void LoadGivenLevel(string level){
		Application.LoadLevel (level);
	}
	public void LoadGivenLevelI(int level){
		Application.LoadLevel (level);
	}
	public void Exit(){
		Application.Quit ();
	}
}
