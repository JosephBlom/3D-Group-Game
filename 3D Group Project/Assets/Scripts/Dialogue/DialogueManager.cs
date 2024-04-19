using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textArea;
    public Canvas dialogueCanvas;
    [SerializeField] Button acceptQuest;
    [SerializeField] Button shopButton;
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

            Quest quest = NPC.GetComponent<QuestManager>().quest;
            if (quest.completed)
            {
                acceptQuest.GetComponentInChildren<TextMeshProUGUI>().text = "Completed";
                acceptQuest.enabled = false;
            }
            else if (quest.isActive && !quest.completed)
            {
                acceptQuest.enabled = false;
                acceptQuest.GetComponentInChildren<TextMeshProUGUI>().text = "Need to complete mission!";
            }
        }
        else
        {
            acceptQuest.enabled = false;
            acceptQuest.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            acceptQuest.image.enabled = false;
        }
        if (sentence.shop)
        {
            shopButton.enabled = true;
            shopButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            shopButton.image.enabled = true;
        }
        else
        {
            shopButton.enabled = false;
            shopButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            shopButton.image.enabled = false;
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
}