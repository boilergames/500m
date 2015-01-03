using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct HighscorePlace {
	public string playername;
	public float time;
}

public class Highscore : MonoBehaviour {

	public List<HighscorePlace> highscore;

	void Start () {
		highscore = new List<HighscorePlace>();
		for(int i = 0; i < 10; i++) {
			highscore.Add(new HighscorePlace());
		}
	}
	
	public void LoadHighscore () {
		if(PlayerPrefs.HasKey("score_name_0")) {
			for(int i = 0; i < 10; i++) {
				HighscorePlace temp = highscore[i];
				temp.playername = PlayerPrefs.GetString("score_name_" + i.ToString());
				temp.time = PlayerPrefs.GetFloat("score_time_" + i.ToString());
				highscore[i] = temp;
			}
		} else {
			Insert("Daniel", 99.75f);
			Insert("Max", 60.0f);
			Insert("Philip", 93.0f);
			Insert("Alyx", 92.0f);
			Insert("Franklin", 80.0f);
			Insert("Lara", 50.0f);
			Insert("Ripley", 89.0f);
			Insert("Gordon", 55.0f);
			Insert("Hoxton", 110.0f);
			Insert("Trevor", 115.5f);
		}
	}
	
	public void SaveHighscore () {
		for(int i = 0; i < 10; i++) {
			HighscorePlace temp = highscore[i];
			PlayerPrefs.SetString("score_name_" + i.ToString(), temp.playername);
			PlayerPrefs.SetFloat("score_time_" + i.ToString(), temp.time);
		}
	}
	
	public bool Insert (string playername, float time) {
		if(time < highscore[9].time) {
			return false;
		}
		
		HighscorePlace newPlace = new HighscorePlace();
		newPlace.time = time;
		newPlace.playername = playername;
		
		for(int i = 0; i < 10; i++) {
			if(highscore[i].time < time) {
				highscore.Insert(i, newPlace);
				highscore.RemoveAt(10);
				SaveHighscore();
				return true;
			}
		}
		
		return false;
	}
	
}
