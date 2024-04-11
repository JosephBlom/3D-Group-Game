using System.Collections;
using System.Collections.Generic;
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
    public void completeQuest()
    {
        quests[0].isActive = false;
        quests[0].completed = true;
    }
    public Quest isQuest()
    {
        if(quests.Count > 0)
        {
            return quests[0];
        }
        return null;
    }
}
