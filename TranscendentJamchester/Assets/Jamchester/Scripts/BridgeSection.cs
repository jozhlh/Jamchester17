using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSection : cbInteractiveObject
{

	//[SerializeField]
	private Vector3 rotationAxis = new Vector3(0,0,1);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RotateSection()
	{
		transform.Rotate(rotationAxis, 90.0f);
	}

	public void SetRotationAxis(Vector3 axis)
	{
		rotationAxis = axis;
	}

	public override void SetGazedAt(bool gazedAt)
	{
		base.SetGazedAt(gazedAt);
	}

	public override void Interact()
	{
		RotateSection();
	}
}
