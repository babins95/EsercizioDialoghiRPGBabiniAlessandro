using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System;

public class DialogueEditor : EditorWindow
{
    [MenuItem("Tools/Dialogue Editor")]
    private static void OpenWindow()
    {
        GetWindow<DialogueEditor>();
    }

    private Vector2 scrollPosition;
    private string[] dialogues;
    private string[] dialogueLabels;
    private int selectedDialogueIndex;

    private void GetAllDialogues()
    {
        dialogues = AssetDatabase.FindAssets("t: Dialogue");
        dialogueLabels = new string[dialogues.Length];

        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogues[i] = AssetDatabase.GUIDToAssetPath(dialogues[i]);
            dialogueLabels[i] = Path.GetFileName(dialogues[i]);
        }
    }

    private void OnGUI()
    {
        titleContent = new GUIContent("Dialogue Editor");

        if (GUILayout.Button("New Dialogue"))
        {
            NewDialogue();
        }

        GUILayout.Space(25f);

        GetAllDialogues();

        if (dialogues.Length == 0)
        {
            EditorGUILayout.HelpBox("No dialogue found", MessageType.Error);
            return;
        }

        selectedDialogueIndex = EditorGUILayout.Popup("Dialogue", selectedDialogueIndex,
            dialogueLabels);
        GUILayout.Label(dialogues[selectedDialogueIndex]);

        Dialogue dialogue = AssetDatabase.LoadAssetAtPath<Dialogue>(dialogues[selectedDialogueIndex]);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < dialogue.sentences.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("E", EditorStyles.miniButtonLeft, GUILayout.Width(20f)))
            {
                EditDialogueEditor editWindow = GetWindow<EditDialogueEditor>();

                editWindow.stringIndex = dialogue.sentences[i].index;
                editWindow.stringSentence = dialogue.sentences[i].strings;
                editWindow.stringCharacterName = dialogue.sentences[i].characterName;
                editWindow.speed = dialogue.sentences[i].speed;
                editWindow.color = dialogue.sentences[i].color;

                editWindow.ShowModalUtility();

                if (editWindow.confirm)
                {
                    dialogue.sentences[i].strings = editWindow.stringSentence;
                    dialogue.sentences[i].characterName = editWindow.stringCharacterName;
                    dialogue.sentences[i].speed = editWindow.speed;
                    dialogue.sentences[i].color = editWindow.color;
                    dialogue.sentences[i].characterImage = editWindow.characterImage;

                    EditorUtility.SetDirty(dialogue);
                }
            }

            if (GUILayout.Button("C", EditorStyles.miniButtonRight, GUILayout.Width(17f)))
            {
                if (EditorUtility.DisplayDialog("Confirm",
                    $"Do you really want to remove the sentence {dialogue.sentences[i].index}?",
                    "Yes",
                    "No"))
                {
                    RemoveString(dialogue.sentences[i].index);
                    break;
                }
            }
            GUI.enabled = false;
            EditorGUILayout.TextField(dialogue.sentences[i].index);
            EditorGUILayout.TextField(dialogue.sentences[i].strings);
            EditorGUILayout.TextField(dialogue.sentences[i].characterName);
            EditorGUILayout.FloatField(dialogue.sentences[i].speed);
            EditorGUILayout.ColorField(dialogue.sentences[i].color);
            EditorGUILayout.ObjectField("Qualunque Sprite", dialogue.sentences[i].characterImage, typeof(Sprite), false);
            //objectrefere EditorGUILayout.ObjectField("RitrattoPG",objectre);
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("New Dialogue"))
        {
            NewDialogueEditor dialogueEditor = EditorWindow.GetWindow<NewDialogueEditor>();
            dialogueEditor.ShowModalUtility();

            if (!string.IsNullOrWhiteSpace(dialogueEditor.stringIndex))
            {
                string newStringIndex = dialogueEditor.stringIndex.ToUpper();

                if (dialogue.sentences.Exists(s => s.index == newStringIndex))
                {
                    EditorUtility.DisplayDialog("Error",
                        $"String {newStringIndex} already exists", "OK");
                }
                else
                {
                    AddString(newStringIndex);
                }
            }
        }
    }

    private void AddString(string stringIndex)
    {
        foreach (string dialoguePath in dialogues)
        {
            Dialogue dialogue = AssetDatabase.LoadAssetAtPath<Dialogue>(dialoguePath);

            Dialogue.DialogueParts newString = new Dialogue.DialogueParts()
            {
                index = stringIndex,
                strings = ""
            };

            dialogue.sentences.Add(newString);

            EditorUtility.SetDirty(dialogue);
        }
    }

    private void NewDialogue()
    {
        string path = EditorUtility.SaveFilePanelInProject("New Dialogue",
            "dialogue", "asset", "New dialogue location");

        if (!string.IsNullOrWhiteSpace(path))
        {
            Dialogue newDialogue = ScriptableObject.CreateInstance<Dialogue>();
            Dialogue currentDialogue = AssetDatabase.LoadAssetAtPath<Dialogue>(dialogues[selectedDialogueIndex]);

            AssetDatabase.CreateAsset(newDialogue, path);

            newDialogue.sentences.AddRange(currentDialogue.sentences);

            EditorUtility.SetDirty(newDialogue);
        }
    }

    private void RemoveString(string stringIndex)
    {
        foreach (string dialoguePath in dialogues)
        {
            Dialogue dialogue = AssetDatabase.LoadAssetAtPath<Dialogue>(dialoguePath);
            dialogue.sentences.RemoveAll(s => s.index == stringIndex);

            EditorUtility.SetDirty(dialogue);
        }
    }
}
