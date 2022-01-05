using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class NewDialogueEditor : EditorWindow
{
    public string stringIndex;
    private bool confirm;

    private void OnGUI()
    {
        titleContent = new GUIContent("New dialogue index");

        GUILayout.Label("Index");

        stringIndex = EditorGUILayout.TextField(stringIndex);

        EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(true));
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("OK"))
        {
            confirm = true;
            Close();
        }

        if (GUILayout.Button("Cancel"))
        {
            Close();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void OnDestroy()
    {
        if (!confirm)
        {
            stringIndex = null;
        }
    }
}
