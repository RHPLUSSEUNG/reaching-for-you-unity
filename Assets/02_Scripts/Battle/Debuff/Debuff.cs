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

        target.GetComponent<CharacterState>().DelDebuff(this);
        return true;
    }

    public abstract bool StartEffect();

    public abstract void SetDebuff(int turn, GameObject target, short attribute = 0, bool TurnEnd = false);

    public abstract void Duplicate_Debuff(Debuff debuff);
}