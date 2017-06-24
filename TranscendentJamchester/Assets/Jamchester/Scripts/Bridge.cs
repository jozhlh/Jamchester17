using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {

	[SerializeField]
	private BridgeSection[] bridgeSections;
	[SerializeField]
	private Vector3 rotationAxis = new Vector3(0.0f, 0.0f, 1.0f);

	// Use this for initialization
	void Start ()
	{
		bridgeSections = GetComponentsInChildren<BridgeSection>();
		for (int b = 0; b <bridgeSections.Length; b++)
		{
			bridgeSections[b].SetRotationAxis(rotationAxis);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
