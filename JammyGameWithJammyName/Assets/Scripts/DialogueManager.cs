using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private Transform hero, boss;
    [SerializeField] private DialogueDataContainer[] initialDialogues, generalDialogues, combatDialogues, funnyDialogues, suspiciousDialogues, endDialogues;

    private Dictionary<DialogueType, DialogueDataContainer[]> dialogueContainers;
    private DialogueDataContainer[] currentDialogueContainer;
    private DialogueDataContainer currentDialogue;
    private GameObject currentTextObject, prevTextObject;
    private bool handlesDialogue;

    private void Start()
    {
        dialogueContainers = new Dictionary<DialogueType, DialogueDataContainer[]>()
        { { DialogueType.Initial, initialDialogues }, { DialogueType.General, generalDialogues },{ DialogueType.Combat, combatDialogues },
          { DialogueType.Funny, funnyDialogues }, { DialogueType.Suspicious, suspiciousDialogues },{ DialogueType.End, endDialogues } };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetDialogue(DialogueType.Initial);
        }
    }

    private void SetDialogue(DialogueType dialogueType)
    {
        if (handlesDialogue)
        {
            return;
        }

        handlesDialogue = true;
        DialogueDataContainer[] value;
        if (dialogueContainers.TryGetValue(dialogueType, out value))
        {
            currentDialogueContainer = value;
        }

        var unusedDialogues = currentDialogueContainer.Where(cD => !cD.used).ToArray();

        if (unusedDialogues.Length <= 0)
        {
            handlesDialogue = false;
            ResetDialogues();
            SetDialogue(dialogueType);
            return;
        }

        int rnd = Random.Range(0, unusedDialogues.Length);
        currentDialogue = unusedDialogues[rnd];
        StartCoroutine(ViewDialogue());
    }

    private IEnumerator ViewDialogue()
    {
        DialogueData[] dialogue = currentDialogue.dialogueData;

        for (int i = 0; i < dialogue.Length; i++)
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
            if (prevTextObject != null)
            {
                prevTextObject.SetActive(false);
            }
            prevTextObject = currentTextObject;
            currentTextObject.SetActive(true);
            yield return new WaitForSeconds(timer);
        }
        yield return new WaitForSeconds(timer);
        prevTextObject.SetActive(false);
        currentDialogue.used = true;
        handlesDialogue = false;
    }

    private void ResetDialogues()
    {
        foreach (var container in dialogueContainers)
        {
            foreach (var dialogue in container.Value)
            {
                dialogue.used = false;
            }
        }
    }

    private void OnDestroy()
    {
        ResetDialogues();
    }
}
