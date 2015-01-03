using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

	public Highscore highscore;
	float surviveTime;
	bool display = false;
	bool newScore = false;
	public GUISkin skin;
	
	public void Display (float newTime) {
		surviveTime = newTime;
		display = true;
		highscore.LoadHighscore();
		newScore = highscore.Insert(PlayerPrefs.GetString("playername"), newTime);
	}
	
	void OnGUI () {
		if(display) {
			GUI.skin = skin;
			string labelString = "You took " + surviveTime.ToString() + " seconds to fall 500 metres.";
			if(newScore) {
				labelString += "\nYou made it into the highscore!";
			}
			GUI.Label(new Rect(Screen.width/2 - 200, Screen.height/2 - 96, 400, 64), labelString);
			if(GUI.Button(new Rect(Screen.width/2 - 200, Screen.height/2 - 32, 400, 64), "Try Again")) {
				Application.LoadLevel("Ingame");
			}
			if(GUI.Button(new Rect(Screen.width/2 - 200, Screen.height/2 + 32, 400, 64), "To Menu")) {
				Application.LoadLevel("Menu");
			}
		}
	}
}
