using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cbInteractiveObject : MonoBehaviour {

	protected bool focussed;

	public virtual void SetGazedAt(bool gazedAt)
	{
		focussed = gazedAt;
	}

	public virtual void Interact()
	{
		
	}
}
