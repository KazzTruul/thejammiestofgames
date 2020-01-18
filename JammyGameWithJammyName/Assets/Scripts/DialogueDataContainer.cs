using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Hero,
    Boss
}

[System.Serializable]
public struct DialogueData
{
    public string text;
    public Character character;
}

[CreateAssetMenu]
public class DialogueDataContainer : ScriptableObject
{
    public DialogueData[] dialogueData;
    public bool used = false;
}
