using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cbNavmeshInteraction : cbInteractiveObject
{
	[SerializeField]
	private NavMeshAgent agent = null;

	private GameObject reticle;

	void Start()
	{
		reticle = GameObject.FindGameObjectWithTag("Reticle");
	}

	public override void SetGazedAt(bool gazedAt)
	{
		base.SetGazedAt(gazedAt);
	}

	public override void Interact()
	{
		if (focussed)
		{
			agent.SetDestination(reticle.GetComponent<GvrReticlePointer>().GetEndPoint());
		}
	}

}
