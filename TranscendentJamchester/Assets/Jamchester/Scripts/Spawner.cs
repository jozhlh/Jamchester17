using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField]
	private Vector3 defaultSpawnRotation = new Vector3(0.0f, 0.0f, 1.0f);
	[SerializeField]
	private int peopleToSpawn = 10;
	[SerializeField]
	private float spawnCounter = 10.0f;
	[SerializeField]
	private GameObject personToSpawn = null;
	[SerializeField]
	private float yOffset = 0.0f;

	private float counter = 0.0f;
	private List<GameObject> activePeople = new List<GameObject>();
	private bool baseFinished = false;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!baseFinished)
		{
			ReduceCounter();
			CheckCounter();
		}
	}

	public void SetPeopleToSpawn(int numOfPeople)
	{
		peopleToSpawn = numOfPeople;
	}

	private void ResetCounter()
	{
		counter = spawnCounter;
	}

	private void ReduceCounter()
	{
		counter -= Time.deltaTime;
	}

	private void RemovePeopleToSpawn()
	{
		peopleToSpawn--;
		if (peopleToSpawn < 1)
		{
			baseFinished = true;
		}
	}

	public void AddPerson()
	{
		peopleToSpawn++;
	}
	private bool CheckCounter()
	{
		if (counter < 0.0f)
		{
			SpawnPerson(defaultSpawnRotation);
			RemovePeopleToSpawn();
			ResetCounter();
			return true;
		}
		else
		{
			return false;
		}
	}

	private void SpawnPerson(Vector3 spawnDirection)
	{
		Vector3 spawnPos = transform.position;
		spawnPos.y += yOffset;
		activePeople.Add(Instantiate(personToSpawn, spawnPos, transform.rotation));
		activePeople[activePeople.Count - 1].GetComponent<CharacterMovement>().SetDirection(spawnDirection);
	}

}
