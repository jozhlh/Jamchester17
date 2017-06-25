using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

	private Text[] uiFields;
	// Use this for initialization
	void Start ()
	{
		
	}

	public void GetFields()
	{
		uiFields = GetComponentsInChildren<Text>();
	}

	public void UpdateUi(int val)
	{
		if (val < 0)
		{
			val = 0;
		}
		for (int ui = 0; ui < uiFields.Length; ui++)
		{
			uiFields[ui].text = val.ToString();
		}
	}
}
