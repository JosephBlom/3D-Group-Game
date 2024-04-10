using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textArea;
    [SerializeField] Canvas dialogueCanvas;

    private Queue<string> sentences;


    private void Start()
    {
        dialogueCanvas.enabled = false;
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        dialogueCanvas.enabled = true;

        foreach (string sentence in dialogue.sentences)
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

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        textArea.text = "";
        foreach (char character in sentence.ToCharArray())
        {
            textArea.text += character;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of Dialogue");
    }

    public void CloseDialogue()
    {
        dialogueCanvas.enabled = false;
    }

}