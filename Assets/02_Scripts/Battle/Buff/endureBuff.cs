using UnityEngine;

public class endureBuff : Buff
{
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            target.GetComponent<CharacterState>().ChangeEndure(false);
            Managers.Active.Heal(target, target.GetComponent<CharacterState>().GetAccumulateDmg()/10*4);
            target.GetComponent<CharacterState>().ResetAccumulateDmg();
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
        target.GetComponent<CharacterState>().ChangeEndure(true);
        TimeCheck();
        return true;
    }
}
