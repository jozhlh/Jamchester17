using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationGrid : MonoBehaviour
{
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private float playerOffset = 1.0f;
	[Header("Teleport Node Parameters")]
	[SerializeField]
	private GameObject teleportNodePrefab;
	[SerializeField]
	private float spacing = 4.0f;
	[SerializeField]
	private float teleportYOffset = 0.5f;
	[Header("Starting Objects")]
	[SerializeField]
	private GameObject startingTeleportNode;
	[SerializeField]
	private Vector3 startCoords = new Vector3(0.0f, 0.0f, 0.0f);

	private Dictionary<Vector2, GameObject> teleportNodes = new Dictionary<Vector2, GameObject>();

	private Vector2 coord = new Vector2();

	// Use this for initialization
	void Start ()
	{
		coord.x = startCoords.x;
		coord.y = startCoords.z;
		teleportNodes.Add(coord, startingTeleportNode);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public List<Vector3> AddNodes(Vector2 currentNode)
	{
		List<Vector3> nodeDirections = new List<Vector3>();
		Vector3 dir = new Vector3(0.0f, 0.0f, 0.0f);
		coord = currentNode;
		coord.x -= 1;
		if (TryAddTeleportNode())
		{
			dir.x = -1;
			dir.z = 0;
			nodeDirections.Add(dir);
		}
		coord.x = currentNode.x;
		if (TryAddTeleportNode())
		{
			dir.x = 1;
			dir.z = 0;
			nodeDirections.Add(dir);
		}
		coord.x = currentNode.x;
		coord.y = currentNode.y + 1;
		if (TryAddTeleportNode())
		{
			dir.x = 0;
			dir.z = -1;
			nodeDirections.Add(dir);
		}
		coord.x = currentNode.x - 1;
		coord.y = currentNode.y + 1;
		if (TryAddTeleportNode())
		{
			dir.x = 0;
			dir.z = 1;
			nodeDirections.Add(dir);
		}
		return nodeDirections;
	}

	private bool TryAddTeleportNode()
	{
		if (!teleportNodes.ContainsKey(coord))
		{
			teleportNodes.Add(coord, Instantiate(teleportNodePrefab, GetNodePosition(), transform.rotation));
			teleportNodes[coord].GetComponent<TeleportNode>().SetNodeCoords(coord, playerOffset);
			return true;
		}
		return false;
	}

	private Vector3 GetNodePosition()
	{
		Vector3 nodePosition = new Vector3(0.0f, teleportYOffset, 0.0f);
		nodePosition.x = (coord.x * spacing) + (spacing * 0.5f);
		nodePosition.z = (coord.y * spacing) - (spacing * 0.5f);
		return nodePosition;
	}

	public void TeleportPlayer(Vector3 location)
	{

	}
}
