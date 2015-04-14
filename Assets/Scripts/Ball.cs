using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private GameController gameController;
	public float speed = 3.5f;
	private LeftGoal lGoal;
	private RightGoal rGoal;
	public GameObject lightningEffect;
	private Vector2 initialPos;
	private Quaternion initialRot;
	public float speedFactor;
	private GameController gameCtrl;
	public bool onFire;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		gameCtrl = gameController.GetComponent<GameController> ();
		GetComponent<Rigidbody2D>().velocity = randomSpeed () * speed;
		lGoal = GameObject.FindGameObjectWithTag ("LeftGoal").GetComponent<LeftGoal> ();
		rGoal = GameObject.FindGameObjectWithTag ("RightGoal").GetComponent<RightGoal> ();
		//lightningEffect.particleSystem.Pause();
		initialPos = transform.position;
		initialRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (gameController.start);
//		if(gameController.start){
//			//rigidbody2D.velocity = Vector2.Scale (rigidbody2D.velocity, new Vector2(speedFactor, speedFactor));
//			Vector2 currentVel = rigidbody2D.velocity;
//			currentVel = Vector2.Scale (currentVel, new Vector2(speed, speed));
//			rigidbody2D.velocity = currentVel;
//		}
//		else{
//			Vector2 currentVel = rigidbody2D.velocity;
//			currentVel = Vector2.Scale (currentVel, new Vector2(0, 0));
//			rigidbody2D.velocity = currentVel;
//		}
	}

	Vector2 randomSpeed() {
		Vector2 newSpeedV;
		float rand = Random.value;
		if(rand >= 0f && rand < 0.25f){
			newSpeedV = new Vector2(-1, 1);
		}
		else if (rand >= 0.25f && rand < 0.5f){
			newSpeedV = new Vector2(-1, -1);
		}
		else if (rand >= 0.5f && rand < 0.75f){
			newSpeedV = new Vector2(1, -1);
		}
		else{
			newSpeedV = new Vector2(1, 1);
		}
		Debug.Log (newSpeedV.normalized);
		return newSpeedV.normalized;
	}

	public void reset(){
		transform.position = initialPos;
		transform.rotation = initialRot;
		GetComponent<Rigidbody2D>().velocity = randomSpeed() * speed;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player1") {
			// Calculate hit Factor
			float y = hitFactor(transform.position,
			                    col.transform.position,
			                    col.collider.bounds.size.y);
			
			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(1, y).normalized;
			PlayerController1 player1 = col.gameObject.GetComponent<PlayerController1> ();
			if(player1.onFire){
				GetComponent<Rigidbody2D>().velocity = dir * 8;
				onFire = true;
			}
			else{
				GetComponent<Rigidbody2D>().velocity = dir * speed;
			}

			// Set Velocity with dir * speed
			GetComponent<AudioSource>().Play();
		}
		
		// Hit the right Racket?
		if (col.gameObject.tag == "Player2") {
			// Calculate hit Factor
			float y = hitFactor(transform.position,
			                    col.transform.position,
			                    col.collider.bounds.size.y);
			
			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(-1, y).normalized;
			PlayerController2 player2 = col.gameObject.GetComponent<PlayerController2> ();
			if(player2.onFire){
				GetComponent<Rigidbody2D>().velocity = dir * 8;
				onFire = true;
			}
			else{
				GetComponent<Rigidbody2D>().velocity = dir * speed;
			}

			// Set Velocity with dir * speed
			GetComponent<AudioSource>().Play();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "LeftGoal"){
			lGoal.currentScore ++;
			Destroy(gameObject);
			gameCtrl.count--;
		}

		if(col.gameObject.tag == "RightGoal"){
			rGoal.currentScore ++;
			Destroy(gameObject);
			gameCtrl.count--;
		}
		if (col.gameObject.tag == "LightningGate") {
			lightningEffect.GetComponent<ParticleSystem>().Play();
		}

	}

	float hitFactor(Vector2 ballPos, Vector2 racketPos,
	                float racketHeight) {
		// ascii art:
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket
		return (ballPos.y - racketPos.y) / racketHeight;
	}



}
