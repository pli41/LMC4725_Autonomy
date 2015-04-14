using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject ball;
	public int count;
	private float timer;
	public float newBallDuration;
	//1 for player1 winning, 2 for player2 winning
	public int winner;
	public bool start;
	private float initialNewBallDuration;
	private int player1score;
	private int player2score;
	private ArrayList balls;
	private Rect windowRect;
	private PlayerController1 player1;
	private PlayerController2 player2;
	private LeftGoal LGoal;
	private RightGoal RGoal;


	// Use this for initialization
	void Start () {
		initialNewBallDuration = newBallDuration;
		windowRect = new Rect (350, 150, 300, 200);
		start = false;
		winner = 0;
		balls = new ArrayList ();
		count = 0;
		timer = 0;
		player1 = GameObject.FindGameObjectWithTag ("Player1").GetComponent <PlayerController1> ();
		player2 = GameObject.FindGameObjectWithTag ("Player2").GetComponent <PlayerController2> ();
		LGoal = GameObject.FindGameObjectWithTag ("LeftGoal").GetComponent<LeftGoal> ();
		RGoal = GameObject.FindGameObjectWithTag ("RightGoal").GetComponent<RightGoal> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (start);
		if (start){
			player2score = player2.currentScore;
			player1score = player1.currentScore;
			
			
			
			timer += Time.deltaTime/2;
			if(timer >= newBallDuration){
				if (newBallDuration > 0.3f){
					newBallDuration -= 0.2f;
				}
				createBall();
				timer = 0;
			}
			if(player1score >= 30){
				winner = 1;
				start = false;
			}
			
			if(player2score >= 30){
				winner = 2;
				start = false;
			}
		}

	}

	void createBall(){
		if(count < 5){
			GameObject newball = (GameObject)Instantiate (ball);
			balls.Add(newball);
			count ++;
			GetComponent<AudioSource>().Play();
		}
	}

	void OnGUI(){
		if (winner == 0 && !start) {
			GUI.Window(0, windowRect, DoMyWindow, "Pong Pong");
		}
		else if (winner != 0 && !start){
			if(winner == 1){
				GUI.color = Color.blue;
			}
			else{
				GUI.color = Color.red;
			}
			GUI.Window(1, windowRect, WinWindow, "Pong Pong");
		}
	}

	void WinWindow(int windowID){
		string WINNER;
		if(winner == 1){
			WINNER = "BLUE";
		}
		else{
			WINNER = "RED";
		}
		GUI.Label (new Rect (25, 20, 250, 150), "Player " + WINNER + " wins . Press button to start another round.");
		if (GUI.Button (new Rect (80, 170, 140, 20), "We're not done yet.")){
			start = true;
			reset();
		}


	}

	void DoMyWindow(int windowID) {
		GUI.Label (new Rect (25, 20, 250, 150), "Welcome to PongPong! The player who gets 30 points first wins the game. Use W&S for blue player and arrows for red player.");
		if (GUI.Button (new Rect (100, 170, 100, 20), "Ready To Go"))
			start = true;
		
	}

	void reset(){
		foreach (GameObject ball in balls){
			Destroy(ball);
		}
		LGoal.currentScore = 0;
		RGoal.currentScore = 0;
		player1.Reset ();
		player2.Reset ();
		winner = 0;
		newBallDuration = initialNewBallDuration;
		Application.LoadLevel ("Pong");
	}
}
