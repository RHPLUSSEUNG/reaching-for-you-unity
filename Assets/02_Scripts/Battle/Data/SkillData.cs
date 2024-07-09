using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Skill")]
public class SkillData : ScriptableObject
{
    public int SkillId;
    public string SkillName;
    public skillType SkillType;
    public int reqLev;

    public ElementType element;
    public int stamina;
    public int mp;
    public int range;

    public TargetObject target;
}