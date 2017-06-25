using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	private List<int> scoreList = new List<int>();

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	public void AddToScores(int newScore)
	{
		scoreList.Add(newScore);
	}

	public int GetLatestScore()
	{
		return scoreList[scoreList.Count - 1];
	}

	public int GetHighestScore()
	{
		int high = scoreList[0];
		foreach (int score in scoreList)
		{
			if (score > high)
			{
				high = score;
			}
		}
		return high;
	}
}
