using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TestSlider : MonoBehaviour {

	public Slider slider;
	public Text text;
	public float value;
	public string desc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = value;
		text.text = desc;
	}
}
