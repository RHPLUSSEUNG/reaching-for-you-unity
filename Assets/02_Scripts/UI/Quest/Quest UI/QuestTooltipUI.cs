using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestTooltipUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Transform objectiveContainer;
    [SerializeField] GameObject objectivePrefab;
    [SerializeField] GameObject objectiveIncompletePrefab;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] Button exitButton;

    private void Awake()
    {
        exitButton.onClick.AddListener(ExitTooltip);
    }

    public void Setup(QuestStatus status)
    {        
        Quest quest = status.GetQuest();
        title.text = quest.GetTitle();

        foreach (Transform item in objectiveContainer)
        {
            Destroy(item.gameObject);
        }

        foreach (var objective in quest.GetObjectives())
        {
            GameObject prefab = objectiveIncompletePrefab;
            if (status.IsObjectiveComplete(objective.GetReference()))
            {
                prefab = objectivePrefab;
            }
            else
            {
                prefab = objectiveIncompletePrefab;
            }
            GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
            TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();

            switch (objective.GetObjectiveType())
            {
                case ObjectiveType.KILL:
                    {
                        ObjectiveKillType objectiveKill = objective as ObjectiveKillType;
                        objectiveText.text = objectiveKill.GetDescription() + " " + objectiveKill.GetKillCount() + " / " + objectiveKill.GetTargetCount();
                        break;
                    }
                case ObjectiveType.GATHER:
                    {
                        ObjectiveGatherType objectiveGather = objective as ObjectiveGatherType;
                        objectiveText.text = objectiveGather.GetDescription() + " " + objectiveGather.GetItemCount() + " / " + objectiveGather.GetTargetCount();
                        break;
                    }
                case ObjectiveType.TALK:
                case ObjectiveType.MOVE:
                    {
                        objectiveText.text = objective.GetDescription();
                        break;
                    }                                
            }                                 
        }
        //rewardText.text = GetRewardText(quest);
    }

    private string GetRewardText(Quest quest)
    {
        string rewardText = "";
        foreach (var reward in quest.GetRewards())
        {
            if (rewardText != "")
            {
                rewardText += ", ";
            }
            if (reward.number > 1)
            {
                rewardText += reward.number + " ";
            }
            //rewardText += reward.item.GetDisplayName();
        }
        if (rewardText == "")
        {
            rewardText = "보상 없음";
        }
        rewardText += ".";
        return rewardText;
    }

    public void ExitTooltip()
    {
        Destroy(gameObject);
    }
}

