using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textArea;
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] Button acceptQuest;
    [SerializeField] GameObject player;

    GameObject NPC;

    private Queue<DialogueLine> sentences;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueCanvas.enabled = false;
        sentences = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialogue dialogue, GameObject NPC)
    {
        this.NPC = NPC;
        sentences.Clear();

        dialogueCanvas.enabled = true;

        foreach (DialogueLine sentence in dialogue.dialogueLines)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine sentence = sentences.Dequeue();

        if (sentence.quest)
        {
            acceptQuest.enabled = true;
            acceptQuest.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            acceptQuest.image.enabled = true;
            
            Quest quest = NPC.GetComponent<QuestManager>().isQuest();
            if(quest == null)
            {
                acceptQuest.enabled = false;
                acceptQuest.GetComponentInChildren<TextMeshProUGUI>().text = "Completed!";
            }
        }
        else
        {
            acceptQuest.enabled = false;
            acceptQuest.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            acceptQuest.image.enabled = false;
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(DialogueLine sentence)
    {
        textArea.text = "";
        foreach (char character in sentence.line.ToCharArray())
        {
            textArea.text += character;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void EndDialogue()
    {
        CloseDialogue();
    }

    public void CloseDialogue()
    {
        ExitMenu();
        dialogueCanvas.enabled = false;
    }

    public void ExitMenu()
    {
        player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;
        player.GetComponent<PlayerManager>().mouseLock(false);
    }

    public void startQuest()
    {
        NPC.GetComponent<QuestManager>().startQuest();
        CloseDialogue();
    }
}