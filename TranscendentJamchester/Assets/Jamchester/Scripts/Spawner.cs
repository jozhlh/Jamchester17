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
	//[SerializeField]
	private float waitTime = 8.0f;
	private float waitCounter = 0;
	[SerializeField]
	private float yOffset = 0.0f;

	private float counter = 0.0f;
	private List<GameObject> activePeople = new List<GameObject>();
	private bool baseFinished = false;
	private List<Vector3> spawnDirections = new List<Vector3>();
	[SerializeField]
	private bool spaceshipIsland = false;
	[SerializeField]
	private UiManager ui;
	
	// Use this for initialization
	void Start ()
	{
		counter = spawnCounter;
		ui.GetFields();
		ui.UpdateUi(peopleToSpawn);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!baseFinished)
		{
			ReduceCounter();
			CheckCounter();
		}
		else
		{
			if (peopleToSpawn > 0)
			{
				baseFinished = false;
				if (waitCounter < 0)
				{
					GetComponentInParent<IslandNode>().SetCityActive();
				}
			}
			else
			{
				waitCounter -= Time.deltaTime;
			}
		}
	}

	public void SetPeopleToSpawn(int numOfPeople)
	{
		peopleToSpawn = numOfPeople;
		ui.UpdateUi(peopleToSpawn);
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
		ui.UpdateUi(peopleToSpawn);
		if (peopleToSpawn < 1)
		{
			if (!baseFinished)
			{
				baseFinished = true;
				waitCounter = waitTime;
				StartCoroutine(WaitForInactivity());
			}
		}
	}

	public void AddPerson()
	{
		peopleToSpawn++;
		ui.UpdateUi(peopleToSpawn);
	}

	private bool CheckCounter()
	{
		if (counter < 0.0f)
		{
			if (spaceshipIsland)
			{
				SpawnPerson(defaultSpawnRotation);
				RemovePeopleToSpawn();
			}
			else
			{
				SpawnPeople();
			}
			ResetCounter();
			return true;
		}
		else
		{
			return false;
		}
	}

	private void SpawnPeople()
	{
		foreach (Vector3 direction in spawnDirections)
		{
			SpawnPerson(direction);
			RemovePeopleToSpawn();
		}
	}

	private void SpawnPerson(Vector3 spawnDirection)
	{
		Vector3 spawnPos = transform.position;
		spawnPos.y += yOffset;
		//Vector3 cross = Vector3.Cross(spawnDirection, Vector3.back);
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, Vector3.back);
		Vector3 eulers = transform.rotation.eulerAngles;
		if (spawnDirection == Vector3.right)
		{
			eulers.y = 0.0f;
		}
		else if (spawnDirection == -Vector3.right)
		{
			eulers.y = 180.0f;
		}
		else if (spawnDirection == Vector3.back)
		{
			eulers.y = 90.0f;
		}
		else if (spawnDirection == Vector3.forward)
		{
			eulers.y = -90.0f;
		}
		rot = Quaternion.Euler(eulers);
		activePeople.Add(Instantiate(personToSpawn, spawnPos, rot));
		activePeople[activePeople.Count - 1].GetComponent<CharacterMovement>().SetDirection(spawnDirection);
	}

	public void SetDirections(List<Vector3> directions)
	{
		spawnDirections = directions;
	}

	IEnumerator WaitForInactivity()
	{
		while (waitCounter > 0.0f)
		{
			yield return null;
		}
		GetComponentInParent<IslandNode>().SetCityInactive();
		yield break;
	}

}
