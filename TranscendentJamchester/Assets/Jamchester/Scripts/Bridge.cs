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
	private float startSectionOffset = -50.0f;
	//[SerializeField]
	private float stagger = 0.25f;
	//[SerializeField]
	private float introDuration = 3.0f;
	[SerializeField]
	private float sectionYoffset = 1.0f;
	[SerializeField]
	private List<GameObject> bridgeSections = new List<GameObject>();
//	[SerializeField]
//	private BridgeSection[] bridgeSections;
	[SerializeField]
	private Vector3 rotationAxis = new Vector3(0.0f, 0.0f, 1.0f);
	private List<Vector3> targetPositions = new List<Vector3>();

	float staggerCounter = 0.0f;
	bool intro = false;
	

	// Use this for initialization
	void Start ()
	{
		CreateSections();
		GetPositions();
		StartCoroutine(PlaceSections());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (intro)
		{
			staggerCounter -= Time.deltaTime;
		}
	}

	private void CreateSections()
	{
		for (int b = 0; b < sectionsPerBridge; b++)
		{
			bridgeSections.Add(Instantiate(bridgePrefabs[Random.Range(0, bridgePrefabs.Count)], transform));
		}
	}

	private void GetPositions()
	{
		int it = 0;
		
		foreach (GameObject section in bridgeSections)
		{
			Vector3 nuPos = section.transform.localPosition;
			nuPos.z = -sectionOffset + (it * sectionOffset);
			nuPos.y += sectionYoffset;
			targetPositions.Add(nuPos);
			nuPos.y = startSectionOffset;
			section.transform.localPosition = nuPos;
			section.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
			section.GetComponent<BridgeSection>().SetRotationAxis(rotationAxis);
			it++;
		}
	}

	IEnumerator PlaceSections()
	{
		intro = true;
		staggerCounter = stagger;
		
		bridgeSections[0].GetComponent<BridgeSection>().RaiseSection(targetPositions[0], introDuration);
		while (staggerCounter > 0.0f)
		{
			yield return null;
		}
		staggerCounter = stagger;

		bridgeSections[1].GetComponent<BridgeSection>().RaiseSection(targetPositions[1], introDuration);
		while (staggerCounter > 0.0f)
		{
			yield return null;
		}
		staggerCounter = stagger;

		bridgeSections[2].GetComponent<BridgeSection>().RaiseSection(targetPositions[2], introDuration);
		while (staggerCounter > 0.0f)
		{
			yield return null;
		}
		intro = false;
		yield return null;
		
	}

	public float GetStagger()
	{
		return stagger;
	}
}
