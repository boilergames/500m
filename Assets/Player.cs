using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public int maxDepth;
	public bool startedTimer = false;
	public float startTime;
	public Rigidbody myRigidbody;
	public float rocketsSpeed;
	public float speed;
	public bool useRockets;
	public float rocketFuel;
	float startFuel;
	public ScoreGUI score;
	public GUISkin skin;
	
	Vector2 input;
	
	float roundSeconds () {
		return Mathf.RoundToInt((Time.time - startTime) * 100.0f) / 100.0f;
	}
	
	int roundSecondsToInt () {
		return Mathf.RoundToInt(Time.time - startTime);
	}
	
	void Start() {
		startFuel = rocketFuel;
	}
	
	void Die () {
		score.Display(roundSeconds());
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftShift) && rocketFuel > 0.0f) {
			useRockets = true;
			rocketFuel -= Time.deltaTime * 15.0f;
		} else {
			useRockets = false;
		}
		input.x = Input.GetAxis("Horizontal");
		input.y = Input.GetAxis("Vertical");
		if(transform.position.y < -1.0f && !startedTimer) {
			startedTimer = true;
			startTime = Time.time;
		}
		if(transform.position.y < -maxDepth) {
			Die();
		}
	}
	
	void FixedUpdate () {
		if(!useRockets) {
			myRigidbody.AddTorque(new Vector3(input.y, 0, -input.x) * speed);
		} else {
			myRigidbody.AddForce(new Vector3(input.x, 0, input.y) * rocketsSpeed);
		}
	}
	
	void OnGUI () {
		GUI.skin = skin;
		GUI.color = Color.green;
		int height = maxDepth + Mathf.RoundToInt(transform.position.y);
		int fuel = Mathf.RoundToInt(rocketFuel);
		int seconds = roundSecondsToInt();
		string timeDisplay = "";
		string metres = height.ToString() + "m left!\n";
		string fuelDisplay = fuel.ToString() + "l fuel left!\n";
		if(startedTimer)
			timeDisplay = seconds.ToString() + "s since start!";
		GUI.Box(new Rect(Screen.width/2 - 200, Screen.height - 96, 400, 96), "");
		
		float metresWidth = ((float)height/500.0f) * 400.0f;
		GUI.Box(new Rect(Screen.width/2 - 200, Screen.height - 96, metresWidth, 32), "");
		GUI.Label(new Rect(Screen.width/2 - 200, Screen.height - 96, 400, 96), metres);
		
		if(rocketFuel > 0.0f) {
			float fuelWidth = (rocketFuel/startFuel) * 400.0f;
			GUI.Box(new Rect(Screen.width/2 - 200, Screen.height - 64, fuelWidth, 32), "");
		}
		GUI.Label(new Rect(Screen.width/2 - 200, Screen.height - 64, 400, 32), fuelDisplay);
	
		if(startedTimer)
			GUI.Label(new Rect(Screen.width/2 - 200, Screen.height - 32, 400, 32), timeDisplay);
	}
}
