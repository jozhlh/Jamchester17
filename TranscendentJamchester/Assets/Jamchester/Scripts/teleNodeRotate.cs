using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleNodeRotate : MonoBehaviour
{
	[SerializeField]
	private float rotationSpeed = 1.0f;

	[SerializeField]
	private Vector3 rotationAxis = new Vector3(0.0f, 1.0f, 0.0f);

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
	}
}
