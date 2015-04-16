using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public int playerNum;
	public GameObject topLimit, botLimit;

	public GameObject fire;
	public bool onFire;

	public int currentScore;
	private GUIText scoreText;

	public KeyCode upKey, downKey;

	public Statistics statistics;

	// Use this for initialization
	void Start () {
		Reset();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentScore > 22){
			onFire = true;
		}

		if(onFire){
			fire.GetComponent<ParticleSystem>().Play();
		}
		else{
			fire.GetComponent<ParticleSystem>().Pause();
		}

		if(Input.GetKey(upKey)){
			if(transform.position.y <= topLimit.transform.position.y){
				Vector3 currentPos = transform.position;
				Vector3 newPos = new Vector3(currentPos.x, currentPos.y+0.3f, currentPos.z);
				transform.position = newPos;

				statistics.moveUpAmount += 0.3f;
			}

			statistics.holdDownDuration += Time.deltaTime;
		} else {
			statistics.holdDownDuration = 0f;
		}

		if(Input.GetKey(downKey)){
			if(transform.position.y >= botLimit.transform.position.y){
				Vector3 currentPos = transform.position;
				Vector3 newPos = new Vector3(currentPos.x, currentPos.y-0.3f, currentPos.z);
				transform.position = newPos;

				statistics.moveDownAmount += 0.3f;
			}

			statistics.holdUpDuration += Time.deltaTime;
		} else {
			statistics.holdUpDuration = 0f;
		}
	}

	public void ScorePoint()
	{
		currentScore++;
		scoreText.text = currentScore.ToString();
	}

	public void Reset(){
		onFire = false;
		currentScore = 0;
	}
}
