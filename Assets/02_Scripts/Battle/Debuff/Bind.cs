using UnityEngine;

public class Bind : Debuff
{
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            target.GetComponent<CharacterState>().ChangeBind(false);
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool turnEnd = false)
    {
        this.target = target;
        remainTurn = turn;
        target.GetComponent<CharacterState>().AddDebuff(this, turnEnd);
        MakeEffectAnim();
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        target.GetComponent<CharacterState>().ChangeBind(true);
        return true;
    }
}
