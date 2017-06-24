using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kaae;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[ExecuteInEditMode]
public class AnimPlayer : MonoBehaviour {

	public enum WrapMode {Loop, Oneshot}
    public WrapMode wrapMode;
    public float fps = 12;
    public bool playOnStart;


    public bool isPlaying { get; set; }

    [SerializeField]
    private List<AnimFrame> frames = new List<AnimFrame>();
    private int frameIndex;

#if UNITY_EDITOR

    void OnEnable()
    {
        EditorApplication.hierarchyWindowChanged += HierarchyChanged;
    }

    void OnDisable()
    {
        EditorApplication.hierarchyWindowChanged -= HierarchyChanged;
    }

    private void HierarchyChanged()
    {
        Reinitialize();
    }
#endif




    public void SetIndex(AnimFrame f)
    {
        //if(frames.Count == 0) Reinitialize();
        frameIndex = frames.FindIndex(i => i == f);
        UpdateView();
    }

    // Use this for initialization
	void Start () {
		Reset();
	    if(playOnStart && Application.isPlaying) Play();
	}
	
	// Update is called once per frame
	void UpdateView () {
	    for (int i = 0; i < frames.Count; i++)
	    {
	        frames[i].gameObject.SetActive(false);
	    }
	    frames[frameIndex].gameObject.SetActive(true);
	}




    [DebugButton]
    public void Play()
    {
        if (isPlaying) return;
        Reset();

        isPlaying = true;
        StopAllCoroutines();

        if(Application.isEditor && !Application.isPlaying)
        {
            EditorCoroutine.StartCoroutine(FpsStepper(), this);
        }
        else
        {
            StartCoroutine(FpsStepper());
        }
    }

    [DebugButton]
    public void Stop()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            EditorCoroutine.StopAllCoroutines(this);
        }
        else
        {
            StopAllCoroutines();
        }

        isPlaying = false;
        UpdateView();
    }


    [DebugButton]
    public void Next()
    {
        frameIndex++;
        frameIndex = (int)Mathf.Repeat(frameIndex, frames.Count);
        UpdateView();
    }

    [DebugButton]
    public void Previous()
    {
        frameIndex--;
        frameIndex = (int)Mathf.Repeat(frameIndex, frames.Count);
        UpdateView();
    }

    public void Reset()
    {
        frameIndex = 0;
        UpdateView();
    }

    [DebugButton]
    public void Reinitialize()
    {
        frames.Clear();
        frames = GetComponentsInChildren<AnimFrame>(true).ToList();
        UpdateView();
    }

    public int Index()
    {
        return frameIndex;
    }

    public int NumberOfFrames()
    {
        return frames.Count;
    }

    IEnumerator FpsStepper()
    {
        while (true)
        {
            if (wrapMode == WrapMode.Oneshot && frameIndex == frames.Count - 1)
            {
                if (Application.isEditor && !Application.isPlaying)
                {
                    EditorCoroutine.StopAllCoroutines(this);
                }
                else
                {
                    StopAllCoroutines();
                }
            }
            Next();



            yield return new WaitForSeconds(1 / fps);
        }
    }

}
