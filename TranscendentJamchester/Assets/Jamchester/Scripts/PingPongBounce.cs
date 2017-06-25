using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PingPongBounce : MonoBehaviour
{

	[SerializeField]
	private float startY = 0.0f;

	[SerializeField]
	private float targetY = 0.0f;

	[SerializeField]
	private float cycleLength = 2.0f;
	
	// Update is called once per frame
	void Update ()
	{
		LerpUp();
	}

	void LerpUp()
	{
		Vector3 pos = transform.localPosition;
		pos.y = Mathf.Lerp(startY, targetY,
			Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / cycleLength, 1f)));
		
		transform.localPosition = pos;
	}
}
