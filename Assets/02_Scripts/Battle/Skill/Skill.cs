using UnityEngine;

public abstract class Skill : MonoBehaviour 
{
    // skill attribute
    public string skillName; //skill name
    public int skillId; //skill id
    public skillType type; //skill type (passive / active)
    public ElementType element;

    public TargetObject target_object;
    public abstract void Start();

}
