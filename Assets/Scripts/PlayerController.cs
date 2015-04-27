using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public int playerNum;
	public GameObject topLimit, botLimit;

	public GameObject mainPanel;
	public GameObject achivPanel;
	public GameObject fire;
	public bool onFire;


	public int currentScore;
	public GUIText scoreText;
	
	public KeyCode upKey, downKey;
	private bool CanHitTopWall, CanHitBottomWall;
	
	public Statistics statistics;
	public List<Achievement> achievements = new List<Achievement>();
	
	public float progress;
	
	private int oldCount;
	// Use this for initialization
	void Start () {
		Reset();
		CreateAchievements();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.instance.start)
		{
			if(currentScore > 22){
				onFire = true;
			}
			
			/*if(onFire){
				fire.GetComponent<ParticleSystem>().Play();
			}
			else{
				fire.GetComponent<ParticleSystem>().Pause();
			}*/
			
			if(Input.GetKey(upKey)){
				if(transform.position.y <= topLimit.transform.position.y){
					Vector3 currentPos = transform.position;
					Vector3 newPos = new Vector3(currentPos.x, currentPos.y+0.3f, currentPos.z);
					transform.position = newPos;
					
					statistics.moveUpAmount += 0.3f;
				}
				else
				{
					if (CanHitTopWall)
					{
						statistics.hitTopWallCount++;
						CanHitTopWall = false;
					}
				}
				
				statistics.holdUpDuration += Time.deltaTime;
				statistics.noInputDuration = 0f;
				CanHitBottomWall = true;
			} else {
				statistics.holdUpDuration = 0f;
			}
			
			if(Input.GetKey(downKey)){
				if(transform.position.y >= botLimit.transform.position.y){
					Vector3 currentPos = transform.position;
					Vector3 newPos = new Vector3(currentPos.x, currentPos.y-0.3f, currentPos.z);
					transform.position = newPos;
					
					statistics.moveDownAmount += 0.3f;
				}
				else
				{
					if (CanHitBottomWall)
					{
						statistics.hitBottomWallCount++;
						CanHitBottomWall = false;
					}
				}
				
				statistics.holdDownDuration += Time.deltaTime;
				statistics.noInputDuration = 0f;
				CanHitTopWall = true;
			} else {
				statistics.holdDownDuration = 0f;
			}

			if(!Input.GetKey(upKey) && !Input.GetKey(downKey))
			{
				statistics.noInputDuration += Time.deltaTime;
			}
		}
		
		foreach (Achievement achievement in achievements)
		{
			if (achievement.Progress() >= 1f)
			{
				if (playerNum == 1)
				{
					GameController.instance.DisplayAchievement1(achievement);
				} 
				else
				{
					GameController.instance.DisplayAchievement2(achievement);
				}
				achievements.Remove (achievement);
				Destroy(achievement.panel);
				break;
			}
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

	void OnGUI(){
		// create and update achievement panels
		for(int i = 0; i< achievements.Count; i++){
			//arrange positions of the panels
			if(oldCount != achievements.Count){
				oldCount = achievements.Count;
				resetPanels();
			}

			Achievement achievement = achievements[i];
			if(achievement.panel == null){
				achievement.panel = Instantiate(achivPanel);
				achievement.panel.transform.parent = mainPanel.transform;
				Vector3 currPos = achievement.panel.transform.position;
				if(gameObject.tag == "Player1"){
					currPos.Set(90f, (-60f*(float)i)+520f, 0f);
				}
				else{
					currPos.Set(790f, (-60f*(float)i)+520f, 0f);
				}

				achievement.panel.transform.position = currPos;
				i+=1;
				achievement.activeGUI = true;
			}


			//change info of the panels
			Text text = achievement.panel.transform.Find("desc").gameObject.GetComponent<Text>();
			Slider slider = achievement.panel.transform.Find("Slider").gameObject.GetComponent<Slider>();
			text.text = achievement.name;
			slider.value = achievement.Progress();
		}
		//Debug.Log ("update Achievements");
	}

	void resetPanels(){
		for (int i = 0; i< achievements.Count; i++) {
			Achievement achievement = achievements[i];
			Destroy(achievement.panel);
		}
	}


	public void CreateAchievements()
	{
		Achievement HitBall1Achievement = new Achievement("Hit Ball 10 Times", "Wow, you hit the ball ten times that's really cool buddy.", 10f);
		HitBall1Achievement.progress = HitBallAchievementProgress;
		achievements.Add(HitBall1Achievement);

		
		Achievement HoldUp1Achievement = new Achievement("Held up for 2 seconds", "Wow! You are a button holding expert.", 2f);
		HoldUp1Achievement.progress = HoldUpAchievementProgress;
		achievements.Add(HoldUp1Achievement);

		Achievement HoldDown1Achievement = new Achievement("Held down for 2 seconds", "Wow! You are a button holding expert.", 2f);
		HoldDown1Achievement.progress = HoldDownAchievementProgress;
		achievements.Add(HoldDown1Achievement);

		Achievement WallHit1Achievement = new Achievement("Hit the wall 5 times", "You're bouncing off the walls!", 5f);
		WallHit1Achievement.progress = WallHitAchievementProgress;
		achievements.Add(WallHit1Achievement);

		Achievement Mover1Achievement = new Achievement("Move 100 paddle miles", "You could be a professional endurance paddle mover.", 100f);
		Mover1Achievement.progress = MoverAchievementProgress;
		achievements.Add(Mover1Achievement);

		Achievement Point1Achievement = new Achievement("Score 3 Points", "Three pointers are supposed to be hard, but you make it look easy.", 3f);
		Point1Achievement.progress = PointAchievementProgress;
		achievements.Add(Point1Achievement);

		Achievement NoInput1Achievement = new Achievement("Don't move for 5 seconds", "How do you I play again?", 5f);
		NoInput1Achievement.progress = NoInputAchievementProgress;
		achievements.Add(NoInput1Achievement);

		Achievement HitBall2Achievement = new Achievement("Hit Ball 30 Times", "Wow, you hit the ball ten times that's really cool buddy.", 30f);
		HitBall2Achievement.progress = HitBallAchievementProgress;
		achievements.Add(HitBall2Achievement);
		
		Achievement HoldUp2Achievement = new Achievement("Held up for 5 seconds", "Wow! You are a button holding expert.", 5f);
		HoldUp2Achievement.progress = HoldUpAchievementProgress;
		achievements.Add(HoldUp2Achievement);
		
		Achievement HoldDown2Achievement = new Achievement("Held down for 5 seconds", "Wow! You are a button holding expert.", 5f);
		HoldDown2Achievement.progress = HoldDownAchievementProgress;
		achievements.Add(HoldDown2Achievement);
		
		Achievement WallHit2Achievement = new Achievement("Hit the wall 10 times", "You're bouncing off the walls!", 10f);
		WallHit2Achievement.progress = WallHitAchievementProgress;
		achievements.Add(WallHit2Achievement);
		
		Achievement Mover2Achievement = new Achievement("Move 500 paddle miles", "You could be a professional endurance paddle mover.", 500f);
		Mover2Achievement.progress = MoverAchievementProgress;
		achievements.Add(Mover2Achievement);
		
		Achievement Point2Achievement = new Achievement("Score 10 Points", "Three pointers are supposed to be hard, but you make it look easy.", 10);
		Point2Achievement.progress = PointAchievementProgress;
		achievements.Add(Point2Achievement);
		
		Achievement NoInput2Achievement = new Achievement("Don't move for 10 seconds", "No, seriously, what am I doing?", 10f);
		NoInput2Achievement.progress = NoInputAchievementProgress;
		achievements.Add(NoInput2Achievement);

		Achievement HitBall3Achievement = new Achievement("Hit Ball 60 Times", "Wow, you hit the ball ten times that's really cool buddy.", 60f);
		HitBall3Achievement.progress = HitBallAchievementProgress;
		achievements.Add(HitBall3Achievement);
		
		Achievement HoldUp3Achievement = new Achievement("Held up for 10 seconds", "Wow! You are a button holding expert.", 10f);
		HoldUp3Achievement.progress = HoldUpAchievementProgress;
		achievements.Add(HoldUp3Achievement);
		
		Achievement HoldDown3Achievement = new Achievement("Held down for 10 seconds", "Wow! You are a button holding expert.", 10f);
		HoldDown3Achievement.progress = HoldDownAchievementProgress;
		achievements.Add(HoldDown3Achievement);
		
		Achievement WallHit3Achievement = new Achievement("Hit the wall 25 times", "You're bouncing off the walls!", 25f);
		WallHit3Achievement.progress = WallHitAchievementProgress;
		achievements.Add(WallHit3Achievement);
		
		Achievement Mover3Achievement = new Achievement("Move 1000 paddle miles", "You could be a professional endurance paddle mover.", 1000f);
		Mover3Achievement.progress = MoverAchievementProgress;
		achievements.Add(Mover3Achievement);
		
		Achievement Point3Achievement = new Achievement("Score 20 Points", "Three pointers are supposed to be hard, but you make it look easy.", 20);
		Point3Achievement.progress = PointAchievementProgress;
		achievements.Add(Point3Achievement);

		Achievement NoInput3Achievement = new Achievement("Don't move for 20 seconds", "No, seriously, what am I doing?", 20f);
		NoInput3Achievement.progress = NoInputAchievementProgress;
		achievements.Add(NoInput3Achievement);


		oldCount = achievements.Count;
	}

	public float HitBallAchievementProgress(float count)
	{
		return (statistics.GetHitBallCount() / count);
	}
	
	public float HoldUpAchievementProgress(float duration)
	{
		return (statistics.GetHoldUpDuration() / duration);
	}

	public float HoldDownAchievementProgress(float duration)
	{
		return (statistics.GetHoldDownDuration() / duration);
	}

	public float WallHitAchievementProgress(float count)
	{
		return (((statistics.GetHitBottomWallCount())+(statistics.GetHitTopWallCount())) / count);
	}

	public float MoverAchievementProgress(float distance)
	{	
		return (statistics.GetMoveAmount() / distance);
	}

	public float PointAchievementProgress(float score)
	{
		return (statistics.GetScore() / score);
	}

	public float NoInputAchievementProgress(float duration)
	{
		return (statistics.GetNoInputDuration() / duration);
	}
}