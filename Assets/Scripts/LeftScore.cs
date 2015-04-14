using UnityEngine;
using System.Collections;

public class LeftScore : MonoBehaviour {

	public int currentScore;
	private RightGoal RGoal;

	// Use this for initialization
	void Start () {
		RGoal = GameObject.FindGameObjectWithTag ("RightGoal").GetComponent<RightGoal> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentScore = RGoal.currentScore;
	}
	void OnGUI(){
		GetComponent<GUIText>().text = currentScore.ToString();
	}
}
