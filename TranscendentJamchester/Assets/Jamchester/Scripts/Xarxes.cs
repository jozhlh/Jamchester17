using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xarxes
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public Vector3 LerpRotation(float duration, float startTime, Vector3 targetAngle, Vector3 startAngle)
	{
		float timePassed = Time.time - startTime;
		float progress = timePassed / duration;
		return Vector3.Lerp(startAngle, targetAngle, progress);
	}
}
