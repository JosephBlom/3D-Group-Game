using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();

    public void startQuest()
    {
        if (quests.Count > 0)
        {
            quests[0].isActive = true;
        }
    }
    public void checkQuest()
    {
        Debug.Log("test");
        for (int i = 0; i < quests[0].needKill.Count; i++)
        {
            Debug.Log("fsfsfs");
            if (quests[0].needKill[i].gameObject == null)
            {
                Debug.Log("geggy");
                quests[0].needKill.RemoveAt(i);
            }
            else if (quests[0].needKill[i] == null)
            {
                Debug.Log("oogy");
                break;
            }
        }
    }
    public void completeQuest()
    {
        if (quests[0].isActive && quests[0].kill)
        {
            if (quests[0].needKill.Count <= 0)
            {
                Debug.Log("You win!");
            }
            quests[0].isActive = false;
            quests[0].completed = true;
        }
        if (quests[0].isActive && quests[0].find)
        {
            if (quests[0].needFind.Count <= 0)
            {
                Debug.Log("You found it all!");
            }
            quests[0].isActive = false;
            quests[0].completed = true;
        }
    }
    public Quest isQuest()
    {
        if (quests.Count > 0)
        {
            return quests[0];
        }
        return null;
    }
}
