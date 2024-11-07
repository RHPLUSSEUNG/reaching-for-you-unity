using UnityEngine;

class Hide : Buff
{
    public override void SetBuff(int turn, GameObject target, int attribute = 0, bool TurnEnd = false)
    {
        this.target = target;
        this.remainTurn = turn;
        AddBuff(target, TurnEnd);
        MakeEffectAnim();
        StartEffect();
    }

    public override bool StartEffect()
    {
        target.SetActive(false);
        return true;
    }

    public override void TimeCheck()
    {
        if(remainTurn == 0)
        {
            target.SetActive(true);
            DeleteEffect();
        }
    }
}
