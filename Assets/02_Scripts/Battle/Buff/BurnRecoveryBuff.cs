using UnityEngine;

public class BurnRecoveryBuff : Buff
{
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            target.GetComponent<CharacterState>().ChangeRecoveryFire(-1);   
            DeleteEffect();
        }
    }

    public override void SetBuff(int turn, GameObject target, int attribute = 0, bool TurnEnd = false)
    {
        this.target = target;
        this.remainTurn = turn;
        AddBuff(target, TurnEnd);
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (this.target == null)
            return false;
        target.GetComponent<CharacterState>().ChangeRecoveryFire(1);
        return true;
    }
}
