using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

	//[SerializeField]
	private ParticleSystem[] particles;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		particles = GetComponentsInChildren<ParticleSystem>();
	}

	public void PlayEffects()
	{
		foreach (ParticleSystem particle in particles)
		{
			particle.Play();
		}
	}
}
