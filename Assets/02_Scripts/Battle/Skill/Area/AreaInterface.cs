using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaInterface : MonoBehaviour
{
    protected Collider[] colliders;
    protected int range;
    protected TargetObject targetType;
    protected int remainTurn;

    public void SetArea(int turn, int range, TargetObject targetType, Vector3 pos)
    {
        this.remainTurn = turn;
        this.range = range;
        this.targetType = targetType;
        this.gameObject.transform.position = pos;
        Managers.Battle.Areas.Add(gameObject);
    }

    protected void GetCharacter()
    {
        colliders = Physics.OverlapBox(this.transform.position, new Vector3(range / 2, 3 / 2, range / 2), Managers.Battle.currentCharacter.transform.rotation, CalcLayer(targetType));
    }

    protected int CalcLayer(TargetObject targetType)
    {
        if (targetType == TargetObject.Me)
        {
            return 0;
        }
        int mask = 1 << 7;
        for (int i = 0; i < (int)targetType; i++)
        {
            mask <<= 1;
        }
        return mask;
    }

    public abstract void CalcTurn();

    protected void DestroyArea()
    {
        remainTurn--;
        if(remainTurn ==  0)
        {
            Destroy(gameObject);
        }
    }
}