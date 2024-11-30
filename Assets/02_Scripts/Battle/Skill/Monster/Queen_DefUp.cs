using System.Collections.Generic;
using UnityEngine;

public class Queen_DefUp : MonsterSkill
{
    public override bool Activate()
    {
        int turn = 3;   //���ӽð�
        int attribute = 30; //������

        List<GameObject> list = Managers.Party.monsterParty;

        for (int i = 0; i < list.Count; i++)
        {
            IncreaseDefence buff = new IncreaseDefence();
            buff.SetBuff(turn, list[i], attribute);
        }
        return true;
    }
}