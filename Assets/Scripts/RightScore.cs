using UnityEngine;
using System.Collections;

public class RightScore : MonoBehaviour {

	public int currentScore;
	private LeftGoal LGoal;
	
	// Use this for initialization
	void Start () {
		LGoal = GameObject.FindGameObjectWithTag ("LeftGoal").GetComponent<LeftGoal> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentScore = LGoal.currentScore;
	}

	void OnGUI(){
		GetComponent<GUIText>().text = currentScore.ToString();
	}
}
