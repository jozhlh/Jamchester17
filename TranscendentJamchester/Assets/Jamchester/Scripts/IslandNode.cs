using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandNode : MonoBehaviour
{

	[SerializeField]
	private GameObject townSpawner = null;
	[SerializeField]
	private GameObject townTrigger = null;
	[SerializeField]
	private ParticleSystem particle = null; 
	private IslandGrid grid;
	private TeleportationGrid teleportationGrid;
	[SerializeField]
	private Vector2 nodeCoords = new Vector2();
	float startOffset = -20.0f;

	float introTimePassed = 0.0f;
	bool intro = false;
	float introDuration = 5.0f;
	[SerializeField]
	bool hasIntro = true;

	// Use this for initialization
	void Start ()
	{
		grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<IslandGrid>();
		teleportationGrid = GameObject.FindGameObjectWithTag("TeleportGrid").GetComponent<TeleportationGrid>();
		if (hasIntro)
		{
			CreateIsland();
		}
		
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (intro)
		{
			introTimePassed += Time.deltaTime;
		}
	}

	public void CreateTown()
	{
		townSpawner.SetActive(true);
		townTrigger.SetActive(false);
		townSpawner.GetComponent<Spawner>().SetDirections(grid.AddNodes(nodeCoords));
		StartCoroutine(LerpCityUp(1.0f));
		teleportationGrid.AddNodes(nodeCoords);
	}

	public void SetNodeCoords(Vector2 coords)
	{
		nodeCoords = coords;
	}

	void CreateIsland()
	{
		float targetY = transform.localPosition.y;
		Vector3 startPos = transform.localPosition;
		startPos.y = startOffset;
		transform.localPosition = startPos;
		transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
		StartCoroutine(LerpSectionUp(introDuration, targetY));
	}

	IEnumerator LerpCityUp(float dur)
	{
		float targetY = townSpawner.transform.localPosition.y;
		Vector3 tempPos = townSpawner.transform.localPosition;
		tempPos.y = -10.0f;
		float introStart  = Time.time;
		float scaleMod = 0.0f;
		introTimePassed = 0.0f;
		townSpawner.transform.localPosition = tempPos;
		intro = true;
		particle.Play();
		while(introTimePassed < dur)
		{
			tempPos.y = Mathf.SmoothStep(tempPos.y, targetY, introTimePassed / dur);
			scaleMod = Mathf.SmoothStep(scaleMod, 0.3f, introTimePassed / dur);
			townSpawner.transform.localPosition = tempPos;
			townSpawner.transform.localScale = new Vector3(scaleMod, scaleMod, scaleMod);
			yield return null;
		}
		intro = false;
		yield return null;
	}
	
	IEnumerator LerpSectionUp(float dur, float targetY)
	{
		Debug.Log("Start raising island");
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
