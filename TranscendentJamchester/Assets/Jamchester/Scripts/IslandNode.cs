using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandNode : MonoBehaviour
{

	[SerializeField]
	private GameObject townSpawner = null;
	[SerializeField]
	private GameObject townTrigger = null;
	private IslandGrid grid;
	private TeleportationGrid teleportationGrid;
	[SerializeField]
	private Vector2 nodeCoords = new Vector2();

	// Use this for initialization
	void Start ()
	{
		grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<IslandGrid>();
		teleportationGrid = GameObject.FindGameObjectWithTag("TeleportGrid").GetComponent<TeleportationGrid>();
	}

	public void CreateTown()
	{
		townSpawner.SetActive(true);
		townTrigger.SetActive(false);
		townSpawner.GetComponent<Spawner>().SetDirections(grid.AddNodes(nodeCoords));
		teleportationGrid.AddNodes(nodeCoords);
	}

	public void SetNodeCoords(Vector2 coords)
	{
		nodeCoords = coords;
	}
}
