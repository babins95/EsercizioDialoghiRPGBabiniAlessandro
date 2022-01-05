using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue.asset", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class DialogueParts
    {
        public string index;
        public string strings;
        public string characterName;
        public float speed;
        public Color color;
        public Sprite characterImage;
    }

    public List<DialogueParts> sentences = new List<DialogueParts>();

    public string GetStrings(string stringIndex)
    {
        DialogueParts stringFound = sentences.Find(s => s.index == stringIndex);
        return stringFound != null ? stringFound.strings : "Errore index sbagliato";
    }
    public float GetSpeed(string stringIndex)
    {
        DialogueParts stringFound = sentences.Find(s => s.index == stringIndex);
        return stringFound != null ? stringFound.speed : 0;
    }
    public Color GetColor(string stringIndex)
    {
        DialogueParts stringFound = sentences.Find(s => s.index == stringIndex);
        return stringFound != null ? stringFound.color : Color.black;
    }
    public string GetCharacterName(string stringIndex)
    {
        DialogueParts stringFound = sentences.Find(s => s.index == stringIndex);
        return stringFound != null ? stringFound.characterName : "Nome non trovato";
    }
    public Sprite GetCharacterImage(string stringIndex)
    {
        DialogueParts stringFound = sentences.Find(s => s.index == stringIndex);
        return stringFound != null ? stringFound.characterImage : null;
    }
}
