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
    private Queue<DialogueLine> altSenetences;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueCanvas.enabled = false;
        sentences = new Queue<DialogueLine>();
        altSenetences = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialogue dialogue, GameObject NPC)
    {
        this.NPC = NPC;
        sentences.Clear();
        altSenetences.Clear();

        dialogueCanvas.enabled = true;

        foreach (DialogueLine sentence in dialogue.dialogueLines)
        {
            sentences.Enqueue(sentence);
        }

        foreach (DialogueLine words in dialogue.altDialogueLines)
        {
            altSenetences.Enqueue(words);
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
                sentence = altSenetences.Dequeue();
                acceptQuest.GetComponentInChildren<TextMeshProUGUI>().text = "Completed";
                acceptQuest.enabled = false;
            }
            else if (quest.isActive && !quest.completed)
            {
                sentence = altSenetences.Dequeue();
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
        FindFirstObjectByType<StarterAssets.StarterAssetsInputs>().gameObject.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;
        FindFirstObjectByType<PlayerManager>().gameObject.GetComponent<PlayerManager>().mouseLock(false);
    }
}