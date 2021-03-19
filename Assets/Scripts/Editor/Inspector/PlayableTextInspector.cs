using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayableText)), ExecuteInEditMode]
public class PlayableTextInspector : Editor
{
    private SerializedProperty btnList;
    private SerializedProperty playMode;
    private SerializedProperty duration;
    private SerializedProperty timeScale;

    private void OnEnable()
    {
        btnList = serializedObject.FindProperty("ButtonList");
        playMode = serializedObject.FindProperty("PlayMode");
        duration = serializedObject.FindProperty("Duration");
        timeScale = serializedObject.FindProperty("TimeScale");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();
        SerializedProperty property = serializedObject.GetIterator();
        if (property.NextVisible(true))
        {
            using (new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
            {
                EditorGUILayout.PropertyField(property, true);
            }
        }

        EditorGUILayout.PropertyField(btnList, true);
        EditorGUILayout.PropertyField(playMode, true);
        switch ((ETextPlayMode)playMode.enumValueIndex)
        {
            case ETextPlayMode.Normal:
                EditorGUILayout.LabelField("正常播放无加速效果");
                break;
            case ETextPlayMode.ShowAll:
                EditorGUILayout.LabelField("点击直接展示全部文本并结束播放");
                break;
            case ETextPlayMode.SpeedUp:
                EditorGUILayout.LabelField("点击加速播放");
                EditorGUILayout.PropertyField(timeScale);
                break;
            case ETextPlayMode.Pass:
                EditorGUILayout.LabelField("直接跳过该文本并结束播放");
                break;
            case ETextPlayMode.Pausable:
                EditorGUILayout.LabelField("可暂停播放");
                break;
        }
        if ((ETextPlayMode)playMode.enumValueIndex != ETextPlayMode.Normal)
            EditorGUILayout.PropertyField(duration, true);

        serializedObject.ApplyModifiedProperties();
    }
}
