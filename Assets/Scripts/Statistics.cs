using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour {
	public PlayerController player;
	public int hitBallCount;
	public int hitTopWallCount, hitBottomWallCount;
	public float moveAmount, moveUpAmount, moveDownAmount;
	public float holdUpDuration, holdDownDuration;
	public float maxHoldUpDuration, maxHoldDownDuration;


	// Use this for initialization
	void Start () {
		player = this.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (holdUpDuration > maxHoldUpDuration)
		{
			maxHoldUpDuration = holdUpDuration;
		}

		if (holdDownDuration > maxHoldDownDuration)
		{
			maxHoldDownDuration = holdDownDuration;
		}
	}

	public int GetScore()
	{
		return player.currentScore;
	}

	public int GetHitBallCount()
	{
		return hitBallCount;
	}

	public float GetMoveAmount()
	{
		return GetMoveUpAmount() + GetMoveDownAmount();
	}

	public float GetMoveUpAmount()
	{
		return moveUpAmount;
	}

	public float GetMoveDownAmount()
	{
		return moveDownAmount;
	}

	public float GetHoldUpDuration()
	{
		return maxHoldUpDuration;
	}

	public float GetHoldDownDuration()
	{
		return maxHoldDownDuration;
	}

	public float GetHitBottomWallCount()
	{
		return hitBottomWallCount;
	}

	public float GetHitTopWallCount()
	{
		return hitTopWallCount;
	}
}
