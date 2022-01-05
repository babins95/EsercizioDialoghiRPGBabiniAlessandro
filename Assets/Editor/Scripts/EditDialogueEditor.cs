using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class EditDialogueEditor : EditorWindow
{
    public string stringIndex;
    public string stringSentence;
    public string stringCharacterName;
    public float speed;
    public Color color;
    public Sprite characterImage;
    public bool confirm;

    private void OnGUI()
    {
        titleContent = new GUIContent("Edit string");

        GUILayout.Label("Index");

        GUI.enabled = false;
        EditorGUILayout.TextField(stringIndex);
        GUI.enabled = true;

        GUILayout.Label("Sentence");
        stringSentence = EditorGUILayout.TextArea(stringSentence, GUILayout.ExpandHeight(true));
        GUILayout.Label("Character Name");
        stringCharacterName = EditorGUILayout.TextArea(stringCharacterName, GUILayout.ExpandHeight(true));
        GUILayout.Label("Speed");
        speed = EditorGUILayout.FloatField(speed, GUILayout.ExpandHeight(false));
        GUILayout.Label("Color");
        color = EditorGUILayout.ColorField(color, GUILayout.ExpandHeight(false));
        GUILayout.Label("Character Image");
        characterImage = (Sprite)EditorGUILayout.ObjectField("Qualunque Sprite", characterImage, typeof(Sprite),false);
        GUILayout.Space(25f);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("OK"))
        {
            confirm = true;
            Close();
        }

        if (GUILayout.Button("Cancel"))
        {
            Close();
        }

        GUILayout.EndHorizontal();
    }
}
