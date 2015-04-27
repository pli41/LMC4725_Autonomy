using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public delegate float ProgressDelegate(float goal);


public class Achievement 
{
	public string name;
	public string description;
	public int value;
	public float goal;
	public ProgressDelegate progress;	// DELEGATE FUNCTION, MUST BE ASSIGNED IN EXTERNAL SCRIPT
	public bool activeGUI;
	public GameObject panel;


	public Achievement(string name, string description, float goal)
	{
		this.name = name;
		this.description = description;
		this.goal = goal;
	}	

	// returns a value between 0f and 1f representing the percent completion of this achievement
	public float Progress()
	{
		return progress(goal);
	}

	// returns a bool, true if achievement has been completed, false otherwise
	public bool IsCompleted()
	{
		return Progress() >= 1f;
	}
}
