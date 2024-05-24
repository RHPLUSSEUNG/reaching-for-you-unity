using UnityEngine;

public class Paralysis : Debuff
{
    int movepoint;
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            target.GetComponent<EntityStat>().MovePoint = movepoint;
            
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute = 0)
    {
        this.target = target;
        movepoint = target.GetComponent<EntityStat>().MovePoint;
        remainTurn = turn;
        AddDeBuff(target);
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        target.GetComponent<EntityStat>().MovePoint = 0;
        return true;
    }

    public override void Duplicate_Debuff(Debuff debuff)
    {
        Paralysis paralysis = (Paralysis)debuff;
        remainTurn += paralysis.remainTurn;
    }
}
