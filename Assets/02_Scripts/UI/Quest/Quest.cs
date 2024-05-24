using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Quest", order = 0)]
public class Quest : ScriptableObject
{
    [SerializeField] string[] objectives;

    public string GetTitle()
    {
        return name;
    }

    public int GetObjectiveCount()
    {
        return objectives.Length;
    }

}
