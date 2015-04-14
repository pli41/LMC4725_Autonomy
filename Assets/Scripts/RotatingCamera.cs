using UnityEngine;
using System.Collections;

public class RotatingCamera : MonoBehaviour {
	// Update is called once per frame
	public float rotatingSpeed;
	void Update () {
		transform.Rotate (new Vector3 (0, 0, rotatingSpeed));
	}
}
