using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTeleportation : MonoBehaviour
{
	[SerializeField]
	private NavMeshAgent player = null;

	private TeleportRaycaster teleporter;
	private ScreenFader fader;
	private float counter = 0.0f;
	private bool readyToMove = false;

	void Start()
	{
		teleporter = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TeleportRaycaster>();
		fader = GameObject.FindGameObjectWithTag("Fade").GetComponent<ScreenFader>();
	}

	public void Update()
	{
		if (counter > -1.0f)
		{
			counter -= Time.deltaTime;
		}
		if (Input.touchCount > 0)
		{
			Debug.Log("Touches");
			if (teleporter.CanTeleport())
			{
				StartCoroutine(FadeOut());
			}
		}
		else if (Input.GetMouseButtonDown(0))
		{
			if (teleporter.CanTeleport())
			{
				StartCoroutine(FadeOut());
			}
		}
	}

	IEnumerator FadeOut()
	{
		readyToMove = false;
		counter = fader.fadeTime;
		fader.fadeIn = false;
		while (counter > 0.0f)
		{
			yield return null;
		}
		//readyToMove = true;
		Teleport();
		yield return null;
	}

	private void Teleport()
	{
		if(player.Warp(teleporter.TeleportDestination()))
		{
			Debug.Log("Successful Warp");
			fader.fadeIn = true;
			readyToMove = false;
		}

	}
}
