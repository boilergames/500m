using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public enum GUIMode {MAIN, LEADER, SURE, SETTINGS, ABOUT}
	public GUIMode mode;
	public GUISkin skin;

	public Texture2D logo;
	
	public string playername;
	
	public Highscore highscore;
	
	public int aa_slider_level;
	
	public bool hasShadows;
	
	Vector2 scrollPos;
	
	// Use this for initialization
	void Start () {
		highscore.LoadHighscore();
		LoadSettings();
		ApplySettings();
	}
	
	int aa_convert(int aa_base) {
		switch(aa_base) {
			case 0:
				return 0;
			case 1:
				return 2;
			case 2:
				return 4;
			case 3:
				return 8;
		}
		return 0;
	}
	
	void LoadSettings () {
		if(PlayerPrefs.HasKey("playername")) {
			playername = PlayerPrefs.GetString("playername");
		} else {
			playername = "John T.";
		}
		aa_slider_level = PlayerPrefs.GetInt("antialiasing");
		hasShadows = PlayerPrefs.GetInt("shadows") == 1;
	}
	
	void ApplySettings () {
		QualitySettings.antiAliasing = aa_convert(aa_slider_level);
		if(hasShadows) {
			QualitySettings.shadowDistance = 200.0f;
		} else {
			QualitySettings.shadowDistance = 0.0f;
		}
	}
	
	void SaveSettings () {
		PlayerPrefs.SetString("playername", playername);
		PlayerPrefs.SetInt("antialiasing", aa_slider_level);
		if(hasShadows) {
			PlayerPrefs.SetInt("shadows", 1);
		} else {
			PlayerPrefs.SetInt("shadows", 0);
		}
	}
	
	void DoGUIMain() {
		if(GUILayout.Button("Play")) {
			Application.LoadLevel("Ingame");
		}
		if(GUILayout.Button("Local Leaderboards")) {
			mode = GUIMode.LEADER;
		}
		if(GUILayout.Button("Settings")) {
			LoadSettings();
			mode = GUIMode.SETTINGS;
		}
		if(GUILayout.Button("About")) {
			mode = GUIMode.ABOUT;
		}
		#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
		if(GUILayout.Button("Quit")) {
			Application.Quit();
		}
		#endif
	}
	
	void DoGUILeader() {
		for(int i = 0; i < 10; i++) {
			GUILayout.Label(highscore.highscore[i].playername + ": " + highscore.highscore[i].time.ToString() + "s");
		}
	}
	
	void DoGUIAbout() {
		GUILayout.Label("In this game you fall 500 metres. Try to take as long as possible.");
		GUILayout.Label("The best way to do this is to stay on platforms.");
		GUILayout.Label("To steer your ball use the arrow keys or wasd.");
		GUILayout.Label("For air control hold down shift (this is not unlimited).");
		GUILayout.Label("Made by Jan W");
		GUILayout.Label("Please send feedback to:");
		GUILayout.Label("slartibartfast@klockenschooster.de");
		GUILayout.Label("For more info see: https://klockenschooster.de");
	}
	
	void DoGUISettings() {
		GUILayout.Label("Username");
		playername = GUILayout.TextField(playername, 16);
		GUILayout.Label("AntiAliasing: " + aa_convert(aa_slider_level).ToString());
		aa_slider_level = Mathf.RoundToInt(GUILayout.HorizontalSlider(aa_slider_level, 0, 3));
		hasShadows = GUILayout.Toggle(hasShadows, "Enable Shadows");
		if(GUILayout.Button("Save")) {
			SaveSettings();
			ApplySettings();
		}
		GUI.color = Color.red;
		if(GUILayout.Button("Delete all saved data")) {
			mode = GUIMode.SURE;
		}
		GUI.color = Color.white;
	}
	
	void DoGUISure() {
		GUI.color = Color.red;
		GUILayout.Label("This will delete all saved data. Are you sure?");
		if(GUILayout.Button("Yes")) {
			PlayerPrefs.DeleteAll();
			Application.LoadLevel("Menu");
		}
		GUI.color = Color.white;
	}
	
	void OnGUI () {
		GUI.skin = skin;
		GUI.DrawTexture(new Rect(Screen.width/2 - 200, 0, 400, 100), logo);
		GUI.Box(new Rect(Screen.width/2 - 200, 100, 400, Screen.height - 100), "");
		GUILayout.BeginArea(new Rect(Screen.width/2 - 200, 100, 400, Screen.height - 100));
		scrollPos = GUILayout.BeginScrollView(scrollPos);
		
		switch(mode) {
			case GUIMode.MAIN:
				DoGUIMain();
				break;
			case GUIMode.LEADER:
				DoGUILeader();
				break;
			case GUIMode.ABOUT:
				DoGUIAbout();
				break;
			case GUIMode.SETTINGS:
				DoGUISettings();
				break;
			case GUIMode.SURE:
				DoGUISure();
				break;
		}
		
		if(mode != GUIMode.MAIN) {
			if(GUILayout.Button("Back")) {
				mode = GUIMode.MAIN;
			}
		}
		
		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}
}
