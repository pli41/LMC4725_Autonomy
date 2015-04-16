using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public int playerNum;
	public GameObject topLimit, botLimit;
	
	public GameObject fire;
	public bool onFire;
	
	public int currentScore;
	public GUIText scoreText;
	
	public KeyCode upKey, downKey;
	private bool CanHitTopWall, CanHitBottomWall;
	
	public Statistics statistics;
	public List<Achievement> achievements = new List<Achievement>();
	
	public float progress;
	
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
		
		progress = achievements[4].Progress();
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
	
	public void CreateAchievements()
	{
		Achievement HitBallAchievement = new Achievement("Hit Ball 10 Times", "Wow, you hit the ball ten times that's really cool buddy.");
		HitBallAchievement.progress = HitBallAchievementProgress;
		achievements.Add(HitBallAchievement);
		
		Achievement HoldUpAchievement = new Achievement("Held up for 20 seconds", "Wow! You are a button holding expert.");
		HoldUpAchievement.progress = HoldUpAchievementProgress;
		achievements.Add(HoldUpAchievement);

		Achievement WallHitAchievement = new Achievement("Hit the wall 60 times", "You're bouncing off the walls!");
		WallHitAchievement.progress = WallHitAchievementProgress;
		achievements.Add(WallHitAchievement);

		Achievement MoverAchievement = new Achievement("You moved 1,500 paddle miles", "You could be a professional endurance paddle mover.");
		MoverAchievement.progress = MoverAchievementProgress;
		achievements.Add(MoverAchievement);

		Achievement ThreePointerAchievement = new Achievement("You scored 3 Points", "Three pointers are supposed to be hard, but you make it look easy.");
		ThreePointerAchievement.progress = ThreePointerAchievementProgress;
		achievements.Add(ThreePointerAchievement);
	}
	
	public float HitBallAchievementProgress()
	{
		return (statistics.hitBallCount / 10f);
	}
	
	public float HoldUpAchievementProgress()
	{
		return (statistics.GetHoldUpDuration() / 20f);
	}

	public float WallHitAchievementProgress()
	{
		return (((statistics.GetHitBottomWallCount())+(statistics.GetHitTopWallCount())) / 60f);
	}

	public float MoverAchievementProgress()
	{
		return (((statistics.GetMoveDownAmount())+(statistics.GetMoveUpAmount())) / 1500f);
	}

	public float ThreePointerAchievementProgress()
	{
		return (currentScore / 3f);
	}
}