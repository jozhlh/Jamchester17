using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimFrame))]
public class AnimFrameEditor : Editor
{
    private AnimFrame _af;

    void OnEnable()
    {
        _af = target as AnimFrame;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (Selection.activeTransform == _af.transform)
            _af.AnimPlayer.SetIndex(_af);

    }
}
