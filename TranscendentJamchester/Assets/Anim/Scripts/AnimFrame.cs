using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kaae;

[ExecuteInEditMode]
public class AnimFrame : MonoBehaviour
{
    private AnimPlayer _animPlayer;
    public AnimPlayer AnimPlayer
    {
        get
        {
            if (_animPlayer == null)
                _animPlayer = GetComponentInParent<AnimPlayer>();
            return _animPlayer;
        }
    }

#if UNITY_EDITOR
    //catch duplication of this GameObject
    [SerializeField]
    [HideInInspector]
    int instanceID = 0;

    [DebugButton]
    void Awake()
    {
        if (Application.isPlaying)
            return;

        if (instanceID == 0)
        {
            instanceID = GetInstanceID();
            return;
        }

        if (instanceID != GetInstanceID() && GetInstanceID() < 0)
        {
            instanceID = GetInstanceID();
            AnimPlayer.Reinitialize(); //Do whatever you need to set up the duplicate
        }
    }
#endif

    /*[SerializeField]
    public List<AnimPlayer> attachedAnims = new List<AnimPlayer>();

    void OnEnable()
    {
        foreach (var anim in attachedAnims)
        {
            anim.Play();
        }
    }*/
}
