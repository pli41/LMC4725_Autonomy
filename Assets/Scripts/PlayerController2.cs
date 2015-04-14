using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour {
	public GameObject topLimit;
	public GameObject botLimit;
	public bool onFire;
	public int currentScore;
	private RightScore RightS;
	private PlayerController1 player1;
	private GameObject fire2;

	// Use this for initialization
	void Start () {
		onFire = false;
		fire2 = GameObject.FindGameObjectWithTag("fire2");
		RightS = GameObject.FindGameObjectWithTag ("RightScore").GetComponent<RightScore> ();
		player1 = GameObject.FindGameObjectWithTag ("Player1").GetComponent < PlayerController1> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentScore = RightS.currentScore;
		if(player1.currentScore > 22){
			onFire = true;
		}

		if(onFire){
			fire2.GetComponent<ParticleSystem>().Play();
		}
		else{
			fire2.GetComponent<ParticleSystem>().Pause();
		}

		if(Input.GetKey(KeyCode.UpArrow)){
			if(transform.position.y <= topLimit.transform.position.y){
				Vector3 currentPos = transform.position;
				Vector3 newPos = new Vector3(currentPos.x, currentPos.y+0.3f, currentPos.z);
				transform.position = newPos;
			}
			
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			if(transform.position.y >= botLimit.transform.position.y){
				Vector3 currentPos = transform.position;
				Vector3 newPos = new Vector3(currentPos.x, currentPos.y-0.3f, currentPos.z);
				transform.position = newPos;
			}
		}
	}

	public void Reset(){
		onFire = false;
		fire2 = GameObject.FindGameObjectWithTag("fire1");
	}
}
