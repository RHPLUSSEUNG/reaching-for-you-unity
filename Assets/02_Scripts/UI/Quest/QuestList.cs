using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    [SerializeField] QuestStatus[] statuses;

    public IEnumerable<QuestStatus> GetStatuses()
    {
        return statuses;
    }
}
