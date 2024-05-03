using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool completed;

    public string title;
    public int experienceReward;
    public int goldReward;

    public Transform target;

    public QuestGoal goal;

    public void checkComplete()
    {
        if (goal.IsReached())
            Complete();
    }

    public void Complete()
    {
        isActive = false;
        completed = true;
    }
}