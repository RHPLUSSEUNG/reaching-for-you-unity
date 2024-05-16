using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour 
{
    // skill attribute
    public string skillName; //skill name
    public int skillId; //skill id
    public skillType type; //skill type (passive / active)
    public ElementType element;
    
    public abstract void Start();
}
