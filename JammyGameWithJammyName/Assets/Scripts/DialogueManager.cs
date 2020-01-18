using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public enum DialogueType
    {
        Initial = 0,
        General = 1,
        Combat = 2,
        Funny = 3,
        Suspicious = 4,
        End = 5
    } 
    [SerializeField] private float timer;
    [SerializeField] private TextMeshPro heroText, bossText;
    [SerializeField] private DialogueDataContainer[] initialDialogues, generalDialogues, combatDialogues, funnyDialogues, suspiciousDialogues, endDialogues;


    private List<DialogueDataContainer[]> dialogueContainers;
    private DialogueType dialogueType;
    DialogueDataContainer currentDialogue;
    GameObject currentTextObject, prevTextObject;

    private void Start()
    {
        dialogueContainers = new List<DialogueDataContainer[]>() { initialDialogues, generalDialogues, combatDialogues, funnyDialogues, suspiciousDialogues, endDialogues };
        dialogueType = DialogueType.Initial;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetDialogue();
        }
    }

    private void SetDialogue()
    {
        DialogueDataContainer[] currentDialogueContainer = dialogueContainers[(int)dialogueType];
        int rnd = Random.Range(0, currentDialogueContainer.Length - 1);
        currentDialogue = currentDialogueContainer[rnd];
        StartCoroutine(ViewDialogue());
    }

    private IEnumerator ViewDialogue()
    {
        DialogueData[] dialogue = currentDialogue.dialogueData;

        for(int i = 0; i < currentDialogue.dialogueData.Length; i++)
        {
            Character character = dialogue[i].character;
            switch (character)
            {
                case Character.Hero:
                    heroText.text = dialogue[i].text;
                    currentTextObject = heroText.gameObject;
                    break;
                case Character.Boss:
                    bossText.text = dialogue[i].text;
                    currentTextObject = bossText.gameObject;
                    break;
            }
            if(prevTextObject != null)
            {
                prevTextObject.SetActive(false);
            }
            prevTextObject = currentTextObject;
            currentTextObject.SetActive(true);
            yield return new WaitForSeconds(timer);
        }
        prevTextObject.SetActive(false);
        yield return new WaitForSeconds(timer);
    }
    
}
