using UnityEngine;

public abstract class Buff
{
    public GameObject target;
    public short remainTurn;

    public abstract void TimeCheck();

    public bool DeleteEffect()
    {
        if (remainTurn > 0)
        {
            return false;
        }
        target.GetComponent<SkillList>().DelBuff(this);
        
        return true;
    }

    public abstract bool StartEffect();

    public abstract void SetBuff(short turn, GameObject target, short attribute = 0);

    public void AddBuff(GameObject target)
    {
        target.GetComponent<SkillList>().buffs.Add(this);
    }
}
