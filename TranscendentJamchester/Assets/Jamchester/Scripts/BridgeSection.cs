using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSection : cbInteractiveObject
{
	private ParticleController particles;
	private Vector3 rotationAxis = new Vector3(0,0,1);
	bool rotate = false;
	float start;
	Vector3 angle;
	Vector3 targetAngle = new Vector3(0.0f, 0.0f, 0.0f);
	[SerializeField]
	private float rotationDuration = 1.5f;
	float introTimePassed = 0.0f;
	bool intro = false;

	// Use this for initialization
	void Start ()
	{
		particles = GetComponentInChildren<ParticleController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (rotate)
		{
			Vector3 newAngle = transform.localEulerAngles;
			
			transform.localEulerAngles = LerpRotation(rotationDuration, start, targetAngle, angle);
			//transform.localEulerAngles = angle	
		}
		if (intro)
		{
			introTimePassed += Time.deltaTime;
		}
	}

	public void RotateSection()
	{
		if (!rotate)
		{
			start = Time.time;
			angle = transform.localEulerAngles;
			CalculateTargetAngle();
			rotate = true;
		}
	}

	private void CalculateTargetAngle()
	{
		targetAngle = angle + (90.0f * rotationAxis);
	}

	public void SetRotationAxis(Vector3 axis)
	{
		rotationAxis = axis;
		int rotAmount = Random.Range(0, 4);
		transform.Rotate(rotationAxis, rotAmount * 90.0f);
	}

	public override void SetGazedAt(bool gazedAt)
	{
		base.SetGazedAt(gazedAt);
	}

	public override void Interact()
	{
		RotateSection();
		particles.PlayEffects();
	}


	public Vector3 LerpRotation(float duration, float startTime, Vector3 targetAngle, Vector3 startAngle)
	{
		float timePassed = Time.time - startTime;
		if (timePassed > duration)
		{
			rotate = false;
		}
		float progress = timePassed / duration;
		
		return Vector3.Lerp(startAngle, targetAngle, progress);
	}

	public void RaiseSection(Vector3 targetPosition, float raiseDuration)
	{
		StartCoroutine(LerpSectionUp(raiseDuration, targetPosition.y));
	}

	IEnumerator LerpSectionUp(float dur, float targetY)
	{
		float introStart  = Time.time;
		float scaleMod = 0.0f;
		introTimePassed = 0.0f;
		Vector3 tempPos = transform.localPosition;
		intro = true;
		while(introTimePassed < dur)
		{
			tempPos.y = Mathf.SmoothStep(tempPos.y, targetY, introTimePassed / dur);
			scaleMod = Mathf.SmoothStep(scaleMod, 1.0f, introTimePassed / dur);
			transform.localPosition = tempPos;
			transform.localScale = new Vector3(scaleMod, scaleMod, scaleMod);
			yield return null;
		}
		intro = false;
		yield return null;
	}
	
}
