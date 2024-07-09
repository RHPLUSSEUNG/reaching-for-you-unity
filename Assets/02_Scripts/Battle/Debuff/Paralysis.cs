using UnityEngine;

public class Paralysis : Debuff
{
    int movepoint;
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            target.GetComponent<CharacterState>().ChangeStun(false);
            
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool turnEnd = false)
    {
        this.target = target;
        remainTurn = turn;
        target.GetComponent<CharacterState>().AddDebuff(this, turnEnd);
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        target.GetComponent<CharacterState>().ChangeStun(true);
        return true;
    }

}
