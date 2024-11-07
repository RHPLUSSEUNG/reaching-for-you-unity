using UnityEngine;

public class Slow : Debuff
{
    int slow;
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            target.GetComponent<CharacterState>().ChangeSlow(false, slow);
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool turnEnd= false)
    {
        name = "Slow";
        this.target = target;
        slow = attribute;
        remainTurn = turn;
        target.GetComponent<CharacterState>().AddDebuff(this, turnEnd);
        MakeEffectAnim();
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        target.GetComponent<CharacterState>().ChangeSlow(true, slow);
        return true;
    }

}
