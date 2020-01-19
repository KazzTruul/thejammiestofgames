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
        HeroTakesDamage = 2,
        BossTakesDamage = 3,
        Suspicious_Grade1 = 4,
        Suspicious_Grade2 = 5,
        Suspicious_Grade3 = 6,
        HeroDies = 7,
        BossDies = 8,
    }
    [SerializeField] private float timeBetweenPhrase, maxTimeToWait, minTimeToWait;
    [SerializeField] private TextMeshProUGUI heroText, bossText;
    [SerializeField] private Transform heroTextParent, bossTextParent;
    [SerializeField] private DialogueDataContainer[] initialDialogues, generalDialogues, heroDamagedDialogues, 
                                                     bossDamagedDialogues, suspiciousDialogues_1, suspiciousDialogues_2, 
                                                     suspiciousDialogues_3, heroDiesDialogues, bossDiesDialogues;


    private Dictionary<DialogueType, DialogueDataContainer[]> dialogueContainers;
    private DialogueDataContainer[] currentDialogueContainer;
    private DialogueDataContainer currentDialogue;
    private GameObject currentTextObject, prevTextObject;
    private bool handleDialogue, handleSpecialDialogue;
    private float timeToWait, counter;
    private DialogueType dialogueType = DialogueType.Initial;

    public DialogueType DialogueTyp
    {
        set
        {
            dialogueType = value;
            handleSpecialDialogue = true;
        }
    }

    private void Start()
    {
        dialogueContainers = new Dictionary<DialogueType, DialogueDataContainer[]>()
        { { DialogueType.Initial, initialDialogues }, { DialogueType.General, generalDialogues },{ DialogueType.HeroTakesDamage, heroDamagedDialogues },
          { DialogueType.BossTakesDamage, bossDamagedDialogues }, { DialogueType.Suspicious_Grade1, suspiciousDialogues_1 },
          { DialogueType.Suspicious_Grade2, suspiciousDialogues_2 },{ DialogueType.Suspicious_Grade3, suspiciousDialogues_3 },
          { DialogueType.HeroDies, heroDiesDialogues },{ DialogueType.BossDies, bossDiesDialogues } };

        SetDialogue();
    }

    private void Update()
    {
        if(handleSpecialDialogue)
        {
            SetDialogue();
            handleSpecialDialogue = false;
        }
        if (handleDialogue)
        {
            return;
        }
        counter += Time.deltaTime;
        if(counter >= timeToWait)
        {
            counter = 0;
            dialogueType = DialogueType.General;
            SetDialogue();
        }
    }

    private void SetDialogue()
    {
        handleDialogue = true;
        DialogueDataContainer[] value;
        if (dialogueContainers.TryGetValue(dialogueType, out value))
        {
            currentDialogueContainer = value;
        }

        var unusedDialogues = currentDialogueContainer.Where(cD => !cD.used).ToArray();

        if (unusedDialogues.Length <= 0)
        {
            handleDialogue = false;
            ResetDialogues();
            SetDialogue();
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
            yield return new WaitForSeconds(timeBetweenPhrase);
        }
        yield return new WaitForSeconds(timeBetweenPhrase);
        prevTextObject.SetActive(false);
        currentDialogue.used = true;
        handleDialogue = false;
        timeToWait = Random.Range(minTimeToWait, maxTimeToWait);
        counter = timeToWait;
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
