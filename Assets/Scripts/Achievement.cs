using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate float ProgressDelegate();


public class Achievement 
{
	public string name;
	public string description;
	public int value;
	public ProgressDelegate progress;

	public Achievement(string name, string description)
	{
		this.name = name;
		this.description = description;
	}

	// returns a value between 0f and 1f representing the percent completion of this achievement
	// DELEGATE FUNCTION, MUST BE ASSIGNED IN EXTERNAL SCRIPT
	public float Progress()
	{
		return progress();
	}

	// returns a bool, true if achievement has been completed, false otherwise
	public bool IsCompleted()
	{
		return Progress() >= 1f;
	}
}
