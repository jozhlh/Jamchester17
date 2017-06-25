using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
	private int scoreVal;
	// Use this for initialization
	void Start ()
	{
		scoreVal = 0;
	}

	public void IncreaseScore()
	{
		scoreVal++;
	}

	public int GetScore()
	{
		return scoreVal;
	}
}
