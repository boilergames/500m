using UnityEngine;
using System.Collections;

public class KeepUpright : MonoBehaviour {

	void LateUpdate () {
		Vector3 tempEuler = transform.eulerAngles;
		tempEuler.x = 0;
		tempEuler.z = 0;
		transform.eulerAngles = tempEuler;
	}
}
