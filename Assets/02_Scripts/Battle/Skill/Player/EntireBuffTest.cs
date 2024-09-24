using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntireBuffTest : MonsterSkill
{
    int turn;
    int attribute;
    public override bool Activate()
    {
        List<GameObject> list = Managers.Party.monsterParty;

        for(int i = 0; i < list.Count; i++)
        {
            IncreaseAtk buff = new IncreaseAtk();
            buff.SetBuff(turn, list[i], attribute);
        }
        return true;
    }
}
