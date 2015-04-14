using UnityEngine;
using System.Collections;

public class PlayerController1 : MonoBehaviour {
	public GameObject topLimit;
	public GameObject botLimit;
	public bool onFire;
	public int currentScore;
	private LeftScore LeftS;
	private PlayerController2 player2;
	private GameObject fire1;

	// Use this for initialization
	void Start () {
		onFire = false;
		fire1 = GameObject.FindGameObjectWithTag("fire1");
		LeftS = GameObject.FindGameObjectWithTag ("LeftScore").GetComponent<LeftScore> ();
		player2 = GameObject.FindGameObjectWithTag ("Player2").GetComponent < PlayerController2> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentScore = LeftS.currentScore;

		if(player2.currentScore > 22){
			onFire = true;
		}

		if(onFire){
			fire1.GetComponent<ParticleSystem>().Play();
		}
		else{
			fire1.GetComponent<ParticleSystem>().Pause();
		}

		if(Input.GetKey(KeyCode.W)){
			if(transform.position.y <= topLimit.transform.position.y){
				Vector3 currentPos = transform.position;
				Vector3 newPos = new Vector3(currentPos.x, currentPos.y+0.3f, currentPos.z);
				transform.position = newPos;
			}
				
		}
		if(Input.GetKey(KeyCode.S)){
			if(transform.position.y >= botLimit.transform.position.y){
				Vector3 currentPos = transform.position;
				Vector3 newPos = new Vector3(currentPos.x, currentPos.y-0.3f, currentPos.z);
				transform.position = newPos;
			}
		}
	}

	public void Reset(){
		onFire = false;
		fire1 = GameObject.FindGameObjectWithTag("fire1");
	}
}
