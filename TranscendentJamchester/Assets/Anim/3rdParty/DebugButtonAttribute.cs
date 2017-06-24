using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using Assets._3rdParty.Kaae;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kaae
{
    public class DebugButtonAttribute : Attribute
    {
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class StupidMethodDrawingTesting : Editor
    {
        private class ParamTuple
        {
            public object Obj;
            public ParameterInfo ParamInfo;
        }

        private class MethodCallInfo
        {
            public MethodInfo MethodInfo;
            public List<ParamTuple> Parameters;
        }

        private List<MethodCallInfo> _methodCallInfo;

        public void OnEnable()
        {
            var methods =
                target.GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                if (method.GetCustomAttributes(typeof(DebugButtonAttribute), false).Any())
                {
                    if (_methodCallInfo == null)
                        _methodCallInfo = new List<MethodCallInfo>();

                    var mci = new MethodCallInfo
                    {
                        MethodInfo = method,
                        Parameters = new List<ParamTuple>()
                    };

                    foreach (var p in method.GetParameters().ToList())
                    {
                        mci.Parameters.Add(new ParamTuple { Obj = p.DefaultValue, ParamInfo = p });
                    }

                    _methodCallInfo.Add(mci);
                }
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_methodCallInfo == null || !_methodCallInfo.Any())
            {
                return;
            }

            GUILayout.BeginVertical("Box");
            var title = "<size=15><color=ffffff>Debug Buttons</color></size>";
            EditorGUILayout.LabelField(title, GUIStyle.none);
            foreach (var info in _methodCallInfo)
            {
                for (int i = 0; i < info.Parameters.Count; i++)
                {
                    var p = info.Parameters[i];
                    CheckNull<float>(ref p, f => EditorGUILayout.FloatField(p.ParamInfo.Name, f));
                    CheckNull<int>(ref p, f => EditorGUILayout.IntField(p.ParamInfo.Name, f));
                    CheckNull<bool>(ref p, f => EditorGUILayout.Toggle(p.ParamInfo.Name, f));
                    CheckNull<string>(ref p, f => EditorGUILayout.TextField(p.ParamInfo.Name, f));
                    CheckNull<Vector2>(ref p, f => EditorGUILayout.Vector2Field(p.ParamInfo.Name, f));
                    CheckNull<Vector3>(ref p, f => EditorGUILayout.Vector3Field(p.ParamInfo.Name, f));
                    CheckNull<Vector4>(ref p, f => EditorGUILayout.Vector4Field(p.ParamInfo.Name, f));
                    CheckNull<Transform>(ref p,
                        f => (Transform)EditorGUILayout.ObjectField(p.ParamInfo.Name, f, typeof(Transform), true));
                    CheckNull<ScriptableObject>(ref p,
                        f => (ScriptableObject)EditorGUILayout.ObjectField(p.ParamInfo.Name, f, typeof(Object), true));

                }

                if (GUILayout.Button(info.MethodInfo.Name))
                {
                    var result = info.MethodInfo.Invoke(target, info.Parameters.Select(p => p.Obj).ToArray());
                    if (result != null)
                    {
                        Debug.Log("Result of " + info.MethodInfo.Name + ": " + result);
                    }
                }
            }
            GUILayout.EndVertical();
        }

        private bool CheckNull<T>(ref ParamTuple tup, Func<T, T> drawMethod)
        {
            if (typeof(T) == tup.ParamInfo.ParameterType)
            {
                if (DBNull.Value.Equals(tup.Obj))
                    tup.Obj = default(T);

                tup.Obj = drawMethod((T)tup.Obj);
                return true;
            }
            return false;
        }
    }
#endif
}