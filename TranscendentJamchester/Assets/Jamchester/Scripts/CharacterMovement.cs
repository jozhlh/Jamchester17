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
	private bool dying = false;
	private float lifetime = 0.0f;
	[SerializeField]
	private ParticleSystem sys;


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
		if (killChar)
		{
			Destroy(gameObject);
		}
		else if (dying)
		{

		}
		else
		{
			lifetime += Time.deltaTime;
			transform.SetPositionAndRotation(transform.position + (moveSpeed * direction), transform.rotation);
		}
	}

	public void SetDirection(Vector3 nuDirection)
	{
		direction = nuDirection;
		//transform.LookAt(direction);
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (!dying)
		{
			if (other.tag == "Hazard")
			{
				Death();
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

	void Death()
	{
		dying = true;
		GetComponentInChildren<AnimPlayer>().gameObject.SetActive(false);
		sys.Play();
		Destroy(gameObject, 0.5f);
	}
}
