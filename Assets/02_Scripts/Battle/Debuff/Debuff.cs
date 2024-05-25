using UnityEngine;

public abstract class Debuff
{
    public GameObject target;
    public int remainTurn;
    public int count;

    public abstract void TimeCheck();

    public bool DeleteEffect()
    {
        if (remainTurn > 0)
        {
            return false;
        }

        target.GetComponent<SkillList>().DelDebuff(this);
        return true;
    }

    public abstract bool StartEffect();

    public abstract void SetDebuff(int turn, GameObject target, short attribute = 0);

    public abstract void Duplicate_Debuff(Debuff debuff);
    

    public void AddDeBuff(GameObject target)
    {
        target.GetComponent<SkillList>().AddDebuff(this);
    }
}