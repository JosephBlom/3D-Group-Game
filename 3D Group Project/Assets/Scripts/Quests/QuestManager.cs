using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest quest;

    public PlayerManager player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerManager>();
    }

    public void AcceptQuest()
    {
        player.currentNPC.GetComponent<QuestManager>().quest.isActive = true;
        player.quest = player.currentNPC.GetComponent<QuestManager>().quest;
        GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
        player.gameObject.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}