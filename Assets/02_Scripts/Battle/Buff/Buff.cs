using UnityEngine;

public abstract class Buff
{
    public GameObject target;
    public int remainTurn;

    public abstract void TimeCheck();

    public bool DeleteEffect()
    {
        if (remainTurn > 0)
        {
            return false;
        }
        target.GetComponent<CharacterState>().DelBuff(this);
        
        return true;
    }

    public abstract bool StartEffect();

    public abstract void SetBuff(int turn, GameObject target, int attribute = 0, bool TurnEnd = false);

    public void AddBuff(GameObject target, bool TurnEnd)
    {
        target.GetComponent<CharacterState>().AddBuff(this, TurnEnd);
    }
}
