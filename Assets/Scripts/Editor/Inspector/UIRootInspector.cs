using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIRoot)), ExecuteInEditMode]
public class UIRootInspector : Editor
{
    private SerializedProperty curScene;
    private SerializedProperty bottomLayer;
    private SerializedProperty middleLayer;
    private SerializedProperty topLayer;
    private SerializedProperty overallLayer;

    private void OnEnable()
    {
        curScene = serializedObject.FindProperty("CurrentScene");
        bottomLayer = serializedObject.FindProperty("Bottom");
        middleLayer = serializedObject.FindProperty("Middle");
        topLayer = serializedObject.FindProperty("Top");
        overallLayer = serializedObject.FindProperty("OverAll");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(curScene, true);

        if ((EScene)curScene.enumValueIndex == EScene.Main)
        {
            EditorGUILayout.PropertyField(bottomLayer, true);
            EditorGUILayout.PropertyField(middleLayer, true);
            EditorGUILayout.PropertyField(topLayer, true);
            EditorGUILayout.PropertyField(overallLayer, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
