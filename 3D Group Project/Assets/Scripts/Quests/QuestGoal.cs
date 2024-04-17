using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public string requiredTag;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled(string tag)
    {
        if (goalType == GoalType.Kill)
        {
            if(requiredTag != null && requiredTag.Equals(tag))
            {
                currentAmount++;
            }
        }

    }

    public void ItemCollected(string tag)
    {
        if (goalType == GoalType.Gathering)
        {
            if (requiredTag != null && requiredTag.Equals(tag))
            {
                currentAmount++;
            }
        }
    }
}

public enum GoalType
{
    Kill,
    Gathering
}