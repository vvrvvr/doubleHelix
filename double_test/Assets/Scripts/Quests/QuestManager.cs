using System;
using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public int maxQuestsCompleted = 0;
    public int currentQuestsCompleted = 0;
    public List<Quest> linkedQuests = new List<Quest>();

    public void CompleteAllQuests()
    {
        Debug.Log("All quests completed");
    }

    public void CompleteQuest(int value)
    {
        currentQuestsCompleted += value;
        if (currentQuestsCompleted >= maxQuestsCompleted)
        {
            CompleteAllQuests();
        }
    }
    

    private void Start()
    {
        linkedQuests = new List<Quest>(FindObjectsOfType<Quest>());
        foreach (var condition in linkedQuests)
        {
            condition.SetQuestManager(this);
        }
        maxQuestsCompleted = linkedQuests.Count;
    }
}