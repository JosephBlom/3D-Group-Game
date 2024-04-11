using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DialogueLine
{
    [Tooltip("Toggle This to Make the Accept Quest Button Appear.")]
    public bool quest;
    [TextArea(3, 6)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

[System.Serializable]
public class Quest
{
    public bool kill;
    public bool find;
    public List<GameObject> needKill = new List<GameObject>();
    public List<GameObject> needFind = new List<GameObject>();
    public bool isActive;
    public bool completed;
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, gameObject);
    }
}