using System;
using System.Collections.Generic;
using UnityEngine;

public class Queen_AtkUp : MonsterSkill
{
    public override bool Activate()
    {
        int turn = 3;   //지속시간
        int attribute = 30; //증가량

        List<GameObject> list = Managers.Party.monsterParty;

        for (int i = 0; i < list.Count; i++)
        {
            IncreaseAtk buff = new IncreaseAtk();
            buff.SetBuff(turn, list[i], attribute);
        }
        return true;
    }
}