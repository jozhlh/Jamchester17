using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {

	[SerializeField]
	private List<GameObject> bridgePrefabs = new List<GameObject>();

	private int sectionsPerBridge = 3;
	[SerializeField]
	private float sectionOffset = 1.0f;
	[SerializeField]
	private float sectionYoffset = 1.0f;
	[SerializeField]
	private List<GameObject> bridgeSections = new List<GameObject>();
//	[SerializeField]
//	private BridgeSection[] bridgeSections;
	[SerializeField]
	private Vector3 rotationAxis = new Vector3(0.0f, 0.0f, 1.0f);
	

	// Use this for initialization
	void Start ()
	{
		CreateSections();
		PlaceSections();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateSections()
	{
		for (int b = 0; b < sectionsPerBridge; b++)
		{
			bridgeSections.Add(Instantiate(bridgePrefabs[Random.Range(0, bridgePrefabs.Count)], transform));
		}
	}

	private void PlaceSections()
	{
		int it = 0;
		
		foreach (GameObject section in bridgeSections)
		{
			Vector3 nuPos = section.transform.localPosition;
			nuPos.z = -sectionOffset + (it * sectionOffset);
			nuPos.y += sectionYoffset;
			section.transform.localPosition = nuPos;
			section.GetComponent<BridgeSection>().SetRotationAxis(rotationAxis);
			it++;
		}
	}


}
