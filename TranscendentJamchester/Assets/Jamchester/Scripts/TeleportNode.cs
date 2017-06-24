using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNode : cbInteractiveObject
{
	private GameObject player;
	private TeleportationGrid grid;
	[SerializeField]
	private Vector2 nodeCoords = new Vector2();
	private float playerOffset;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void SetNodeCoords(Vector2 coords, float offset)
	{
		nodeCoords = coords;
		grid = GameObject.FindGameObjectWithTag("TeleportGrid").GetComponent<TeleportationGrid>();
		playerOffset = offset;
	}

	public override void Interact()
	{
		Vector3 pos = transform.position;
		pos.y += playerOffset;
		player.transform.position = pos;
	}
}
