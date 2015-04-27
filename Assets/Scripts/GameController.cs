using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public static GameController instance;
	public GameObject ball;
	public float newBallDuration;
	private float initialNewBallDuration;

	public int count;
	private float timer;
	//1 for player1 winning, 2 for player2 winning
	public int winner;
	public bool start;

	
	private ArrayList balls;
	private Rect windowRect;
	private Rect achieveRectP1;
	private Rect achieveRectP2;
	public PlayerController player1, player2;

	public bool displayAchievement1, displayAchievement2;
	public Achievement achievement1, achievement2;


	// Use this for initialization
	void Start () {
		instance = this;

		initialNewBallDuration = newBallDuration;
		windowRect = new Rect (330, 150, 300, 200);
		//(10, 410, 175, 80), (775, 410, 175, 80)
		achieveRectP1 = new Rect (40, 150, 300, 200);
		achieveRectP2 = new Rect (600, 150, 300, 200);
		start = false;
		winner = 0;
		balls = new ArrayList ();
		count = 0;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (start);
		if (start){			
			
			timer += Time.deltaTime/2;
			if(timer >= newBallDuration){
				if (newBallDuration > 0.3f){
					newBallDuration -= 0.2f;
				}
				createBall();
				timer = 0;
			}
			if(player1.currentScore >= 30){
				winner = 1;
				start = false;
			}
			
			if(player2.currentScore >= 30){
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
			GUI.Window (0, windowRect, DoMyWindow, "Pong Pong");
		} else if (winner != 0 && !start) {
			if (winner == 1) {
				GUI.color = Color.blue;
			} else {
				GUI.color = Color.red;
			}
			GUI.Window (1, windowRect, WinWindow, "Pong Pong");
		} else {
			//Debug.Log("running");
			/*if (displayAchievement1) {	
				GUI.Window (2, achieveRectP1, AchieveWindowP1, achievement1.name);
			}
			//achievement1.name
			if (displayAchievement2) {
				GUI.Window (3, achieveRectP2, AchieveWindowP2, achievement2.name);
			}*/
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
		GUI.Label (new Rect (25, 20, 250, 150), "Welcome to Achievement Pong! Your goal is to get as many achievements as possible in two minutes. The player with the most achievements wins the game. The W and S keys control the blue paddle and the up and down arrow keys control the red paddle.");
		if (GUI.Button (new Rect (100, 170, 100, 20), "Click To Start"))
			start = true;
		
	}

	void AchieveWindowP1(int windowID) {
		GUI.Label (new Rect (10, 20, 140, 80), achievement1.description + "\n" + achievement1.value + " Points");
		//chievement1.description + "\n" + achievement1.value + " Points"
	}

	void AchieveWindowP2(int windowID) {
		GUI.Label (new Rect (10, 20, 140, 80), achievement2.description + "\n" + achievement2.value + " Points");
	}

	public void DisplayAchievement1(Achievement achievement1)
	{
		this.achievement1 = achievement1;
		Debug.Log (achievement1.description);
		StartCoroutine(DisplayAchievement1ForFiveSeconds());
	}

	public void DisplayAchievement2(Achievement achievement2)
	{
		this.achievement2 = achievement2;
		Debug.Log (achievement2.description);;
		StartCoroutine(DisplayAchievement2ForFiveSeconds());
	}

	IEnumerator DisplayAchievement1ForFiveSeconds()
	{
		Timer timer = new Timer(5f);
		while (timer.Percent() < 1f)
		{
			displayAchievement1 = true;
			yield return 0;
		}
		
		displayAchievement1 = false;
	}

	IEnumerator DisplayAchievement2ForFiveSeconds()
	{
		Timer timer = new Timer(5f);
		while (timer.Percent() < 1f)
		{
			displayAchievement2 = true;
			yield return 0;
		}
		
		displayAchievement2 = false;
	}


	void reset(){
		foreach (GameObject ball in balls){
			Destroy(ball);
		}
		player1.Reset ();
		player2.Reset ();
		winner = 0;
		newBallDuration = initialNewBallDuration;
		Application.LoadLevel ("Pong");
	}
}
