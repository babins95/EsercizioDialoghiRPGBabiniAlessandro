using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController instance;
    public Dialogue currentDialogue;
    //public string dialogueIndex;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueCharacterName;
    public GameObject characterImage;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }
    void NextDialogue()
    {
        if(index <= currentDialogue.GetStrings(currentDialogue.sentences[index].index).Length -1)
        {
            dialogueText.text = "";
            StartCoroutine(WriteDialogue());
        }
    }
    IEnumerator WriteDialogue()
    {
        characterImage.GetComponent<Image>().sprite = currentDialogue.GetCharacterImage(currentDialogue.sentences[index].index);
        dialogueCharacterName.text = currentDialogue.GetCharacterName(currentDialogue.sentences[index].index);
        foreach (char Character in currentDialogue.GetStrings(currentDialogue.sentences[index].index).ToCharArray())
        {
            dialogueText.color = currentDialogue.GetColor(currentDialogue.sentences[index].index);
            dialogueText.text += Character;
            yield return new WaitForSeconds(currentDialogue.GetSpeed(currentDialogue.sentences[index].index));
        }
        index++;
    }
}
