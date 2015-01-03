using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public Transform platformPrefab;

	// Use this for initialization
	void Start () {
		
		Vector3 position = Vector3.one;
		Vector3 scale = Vector3.one;
		Vector3 torque = Vector3.one;
		for(int i = 0; i < 800; i++) {
			position.x = Random.Range(-150.0f, 150.0f);
			position.y = Random.Range(-500.0f, -10.0f);
			position.z = Random.Range(-150.0f, 150.0f);
			
			scale.x = Random.Range(3.0f, 30.0f);
			scale.y = Random.Range(0.75f, 3.0f);
			scale.z = Random.Range(3.0f, 30.0f);
			
			torque.x = Random.Range(-400.0f, 400.0f);
			torque.y = Random.Range(-400.0f, 400.0f);
			torque.z = Random.Range(-400.0f, 400.0f);
			
			Quaternion quaternion = Quaternion.identity;
			Transform newPlatform =	Instantiate(platformPrefab, position, quaternion) as Transform;
			newPlatform.localScale = scale;
			newPlatform.rigidbody.AddTorque(torque);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
