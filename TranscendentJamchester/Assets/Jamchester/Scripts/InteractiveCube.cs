using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class InteractiveCube : cbInteractiveObject {

	[SerializeField]
	private Material defaultMat;

	[SerializeField]
	private Material focussedMat;

	[SerializeField]
	private Material interactionMat;


	public override void SetGazedAt(bool gazedAt)
	{
		base.SetGazedAt(gazedAt);

		StopAllCoroutines();
		
		if (gazedAt)
		{
			GetComponent<Renderer>().sharedMaterial = focussedMat;
		}
		else
		{
			GetComponent<Renderer>().sharedMaterial = defaultMat;
		}
	}

	public override void Interact()
	{
		base.Interact();
		StartCoroutine(ChangeColourTemp());
	}

	IEnumerator ChangeColourTemp()
	{
		float count = 1.0f;
		
		GetComponent<Renderer>().sharedMaterial = interactionMat;

		while (count > 0)
		{
			count -= Time.deltaTime;
			yield return null;
		}

		GetComponent<Renderer>().sharedMaterial = defaultMat;
	}
}
