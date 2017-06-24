using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRaycaster : MonoBehaviour
{
	[SerializeField]
	private GameObject teleportationIndicator;

	[SerializeField]
	private float teleportCounter = 1.0f;

	private bool canTeleport = false;
	private float counter = 0.0f;
	
	// Update is called once per frame
	void Update ()
	{
		if (counter > -1.0f)
		{
			counter -= Time.deltaTime;
		}
		RaycastHit hit = new RaycastHit();
		Ray ray = new Ray();
		ray.origin = transform.position;
		ray.direction = transform.forward;
		
		if (Physics.Raycast(ray, out hit, GetComponent<Camera>().farClipPlane))
		{
			if (hit.collider.GetComponent<NavTeleportation>() != null)
			{
				canTeleport = true;
				teleportationIndicator.transform.position = hit.point;
			}
			else
			{
				if (hit.collider.tag != "TeleportLocation")
				{
					canTeleport = false;
				}
			}
		}
		else
		{
			canTeleport = false;
		}
		if (counter > 0.0f)
		{
			canTeleport = false;
		}
		teleportationIndicator.SetActive(canTeleport);
	}

	public bool CanTeleport()
	{
		return canTeleport;
	}

	public Vector3 TeleportDestination()
	{
		SetTeleportCounter();
		return teleportationIndicator.transform.position;
	}

	private void SetTeleportCounter()
	{
		counter = teleportCounter;;
	}
}
