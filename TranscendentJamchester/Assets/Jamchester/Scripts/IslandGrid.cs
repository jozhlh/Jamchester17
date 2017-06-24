using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGrid : MonoBehaviour
{
	[Header("Bridge Parameters")]
	[SerializeField]
	private GameObject bridgePrefab;
	[SerializeField]
	private float bridgeYOffset = 0.5f;
	[Header("Island Parameters")]
	[SerializeField]
	private GameObject islandNodePrefab;
	[SerializeField]
	private float spacing = 4.0f;
	[SerializeField]
	private float islandYOffset = 0.5f;
	[Header("Starting Objects")]
	[SerializeField]
	private GameObject startingIsland;
	[SerializeField]
	private int xStartCoord = 0;
	[SerializeField]
	private int zStartCoord = 0;
	[SerializeField]
	private GameObject spaceShipIsland;
	[SerializeField]
	private int xShipCoord = 0;
	[SerializeField]
	private int zShipCoord = 0;
	private Dictionary<Vector2, GameObject> islandNodes = new Dictionary<Vector2, GameObject>();
	private List<GameObject> bridges = new List<GameObject>();
	private Vector2 coord = new Vector2();
	private Vector3 bridgeSpacingModifier = new Vector3(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start ()
	{
		coord.x = xShipCoord;
		coord.y = zShipCoord;
		islandNodes.Add(coord, spaceShipIsland);

		coord.x = xStartCoord;
		coord.y = zStartCoord;
		islandNodes.Add(coord, startingIsland);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public bool AddNodes(Vector2 currentNode)
	{
		bool nodesAdded = false;
		coord = currentNode;
		coord.x -= 1;
		bridgeSpacingModifier.x = 0.5f;
		bridgeSpacingModifier.z = 0.0f;
		if (TryAddIsland())
		{
			nodesAdded = true;
		}
		coord.x = currentNode.x + 1;
		bridgeSpacingModifier.x = -0.5f;
		bridgeSpacingModifier.z = 0.0f;
		if (TryAddIsland())
		{
			nodesAdded = true;
		}
		coord.x = currentNode.x;
		coord.y = currentNode.y - 1;
		bridgeSpacingModifier.x = 0.0f;
		bridgeSpacingModifier.z = 0.5f;
		if (TryAddIsland())
		{
			nodesAdded = true;
		}
		coord.y = currentNode.y + 1;
		bridgeSpacingModifier.x = 0.0f;
		bridgeSpacingModifier.z = -0.5f;
		if (TryAddIsland())
		{
			nodesAdded = true;
		}
		return nodesAdded;
	}

	private bool TryAddIsland()
	{
		if (!islandNodes.ContainsKey(coord))
		{
			islandNodes.Add(coord, Instantiate(islandNodePrefab, GetIslandPosition(), transform.rotation));
			islandNodes[coord].GetComponent<IslandNode>().SetNodeCoords(coord);
			PlaceBridge();
			return true;
		}
		return false;
	}

	private Vector3 GetIslandPosition()
	{
		Vector3 islandPosition = new Vector3(0.0f, islandYOffset, 0.0f);
		islandPosition.x = coord.x * spacing;
		islandPosition.z = coord.y * spacing;
		return islandPosition;
	}

	private void PlaceBridge()
	{
		GameObject bridge = Instantiate(bridgePrefab, GetBridgePosition(), transform.rotation);
		bridge.transform.Rotate(GetBridgeRotation());
		bridges.Add(bridge);
	}

	private Vector3 GetBridgePosition()
	{
		Vector3 bridgePosition = new Vector3(0.0f, bridgeYOffset, 0.0f);
		bridgePosition.x = (coord.x * spacing) + (bridgeSpacingModifier.x * spacing);
		bridgePosition.z = (coord.y * spacing) + (bridgeSpacingModifier.z * spacing);
		return bridgePosition;
	}

	private Vector3 GetBridgeRotation()
	{
		Vector3 rot = new Vector3(0.0f, 0.0f, 0.0f);
		if (bridgeSpacingModifier.x != 0.0f)
		{
			rot.y = 90.0f;
			return rot;
		}
		else
		{
			return rot;
		}
	}
}
