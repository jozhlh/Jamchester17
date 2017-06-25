using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	private GameObject helpCanvas;
	// Use this for initialization
	
	public void ShowCanvas()
	{
		helpCanvas.SetActive(true);
	}

	public void HideCanvas()
	{
		helpCanvas.SetActive(false);
	}
}
