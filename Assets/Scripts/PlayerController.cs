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
			else
			{
				if (CanHitTopWall)
				{
					statistics.hitTopWallCount++;
					CanHitTopWall = false;
				}
			}
			
			statistics.holdUpDuration += Time.deltaTime;
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
			CanHitTopWall = true;
		} else {
			statistics.holdDownDuration = 0f;
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
			slider.value = achievement.progress();
		}
		Debug.Log ("update Achievements");
	}

	void resetPanels(){
		for (int i = 0; i< achievements.Count; i++) {
			Achievement achievement = achievements[i];
			Destroy(achievement.panel);
		}
	}


	public void CreateAchievements()
	{
		Achievement HitBallAchievement = new Achievement("Hit Ball 10 Times", "Wow, you hit the ball ten times that's really cool buddy.");
		HitBallAchievement.progress = HitBallAchievementProgress;
		achievements.Add(HitBallAchievement);
		
		Achievement HoldUpAchievement = new Achievement("Held up for 2 seconds", "Wow! You are a button holding expert.");
		HoldUpAchievement.progress = HoldUpAchievementProgress;
		achievements.Add(HoldUpAchievement);

		Achievement WallHitAchievement = new Achievement("Hit the wall 5 times", "You're bouncing off the walls!");
		WallHitAchievement.progress = WallHitAchievementProgress;
		achievements.Add(WallHitAchievement);

		Achievement MoverAchievement = new Achievement("You moved 100 paddle miles", "You could be a professional endurance paddle mover.");
		MoverAchievement.progress = MoverAchievementProgress;
		achievements.Add(MoverAchievement);

		Achievement ThreePointerAchievement = new Achievement("You scored 3 Points", "Three pointers are supposed to be hard, but you make it look easy.");
		ThreePointerAchievement.progress = ThreePointerAchievementProgress;
		achievements.Add(ThreePointerAchievement);



		oldCount = achievements.Count;
	}
	
	public float HitBallAchievementProgress()
	{
		return (statistics.GetHitBallCount() / 10f);
	}
	
	public float HoldUpAchievementProgress()
	{
		return (statistics.GetHoldUpDuration() / 2f);
	}

	public float WallHitAchievementProgress()
	{
		return (((statistics.GetHitBottomWallCount())+(statistics.GetHitTopWallCount())) / 5);
	}

	public float MoverAchievementProgress()
	{	
		return (statistics.GetMoveAmount() / 100);
	}

	public float ThreePointerAchievementProgress()
	{
		return (statistics.GetScore() / 3f);
	}
}