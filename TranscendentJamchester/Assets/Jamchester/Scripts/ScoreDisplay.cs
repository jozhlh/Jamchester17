using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	[SerializeField]
	private Text scoreField;

	// Use this for initialization
	void Start ()
	{
		scoreField.text = "SCORE: ";
		scoreField.text += GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreKeeper>().GetLatestScore().ToString();
	}
}
