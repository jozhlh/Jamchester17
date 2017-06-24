using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	private Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);

	private bool killChar = false;
	private float lifetime = 0.0f;


	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		lifetime = 0.0f;
	}

	// Update is called once per frame
	void Update ()
	{
		lifetime += Time.deltaTime;
		transform.SetPositionAndRotation(transform.position + (moveSpeed * direction), transform.rotation);
		if (killChar)
		{
			Destroy(gameObject);
		}
	}

	public void SetDirection(Vector3 nuDirection)
	{
		direction = nuDirection;
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Hazard")
		{
			killChar = true;
		}
		else if (other.tag == "Town")
		{
			//Found Town
			other.GetComponentInParent<IslandNode>().CreateTown();
			killChar = true;
		}
		else if (other.tag == "Spawner")
		{
			if (lifetime > 1.0f)
			{
				other.GetComponent<Spawner>().AddPerson();
				killChar = true;
			}
		}
	}
}
