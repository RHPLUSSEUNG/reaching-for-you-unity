using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Skill : MonoBehaviour
{
    // skill attribute
    public enum skillType
    {
        Passive = 1,
        Buff = 2,
        Attack = 3,
        Debuff = 4
    };
    public int skillId;
    public skillType type;
}
