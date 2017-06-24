using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class AnimStateManager : MonoBehaviour 
{

	[SerializeField]
	private Dictionary<string, AnimPlayer> animClips = new Dictionary<string, AnimPlayer>();

	[SerializeField]
	private List<string> animClipNames = new List<string>();
	[SerializeField]
	private List<AnimPlayer> animClipPlayers = new List<AnimPlayer>();
	[SerializeField]
	private bool updateAnimClips = false;
	private bool clipPlaying = false;
	private bool clipQueued = false;
	private string currentlyPlaying = "";

	// Use this for initialization
	void Start ()
	{
		UpdateAnimClips();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (updateAnimClips)
		{
			UpdateAnimClips();
			updateAnimClips = false;
		}
	}

	public void UpdateAnimClips()
	{
		if (animClipNames.Count != animClipPlayers.Count)
		{
			Debug.LogError("The number of animation names and clips must match");
			return;
		}
		animClips.Clear();
		for (int an = 0; an < animClipNames.Count; an++)
		{
			animClips.Add(animClipNames[an], animClipPlayers[an]);
		}
	}

	public bool PlayAnimClip(string clipName, bool forcePlay)
	{
		if (animClips.ContainsKey(clipName))
		{
			if (!clipPlaying | forcePlay)
			{
				Play(clipName);
			}
			else if (!animClips[clipName].isPlaying)
			{
				StartCoroutine(QueueClip(clipName));
			}

		}
		else
		{
			return false;
		}
		return true;
	}

	private void Play(string name)
	{
		DisableAllClips();
		animClips[name].gameObject.SetActive(true);
		if (clipQueued)
		{
			StopAllCoroutines();
			clipQueued = false;
		}
		currentlyPlaying = name;
		animClips[name].Play();
		SetPlayingTimer();
	}

	private void DisableAllClips()
	{
		foreach (string name in animClips.Keys)
		{
			animClips[name].Stop();
			animClips[name].gameObject.SetActive(false);
		}
	}

	private void SetPlayingTimer()
	{
		float countdown = 0.0f;
		clipPlaying = true;
		if (animClips[currentlyPlaying].wrapMode == AnimPlayer.WrapMode.Oneshot)
		{
			countdown = animClips[currentlyPlaying].NumberOfFrames() / animClips[currentlyPlaying].fps;
			StartCoroutine(PlayingCountdown(countdown));
		}
	}

	IEnumerator PlayingCountdown(float count)
	{
		while(count > 0)
		{
			count -= Time.deltaTime;
			yield return null;
		}
		clipPlaying = false;
		yield return null;
	}

	IEnumerator QueueClip(string name)
	{
		clipQueued = true;
		while(clipPlaying)
		{
			if (animClips[currentlyPlaying].Index() == (animClips[currentlyPlaying].NumberOfFrames() - 1))
			{
				clipPlaying = false;
				animClips[currentlyPlaying].Stop();
			}
			yield return null;
		}
		clipQueued = false;
		Play(name);
		yield return null;
	}
}
