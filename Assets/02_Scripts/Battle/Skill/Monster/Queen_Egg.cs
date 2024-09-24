using UnityEngine;

public class Queen_Egg : MonsterSkill
{
    public override bool Activate()
    {
        GameObject obj = Managers.Party.InstantiateMonster("Enemy_Egg");
        obj.transform.position = targetPos + new Vector3(0, 1, 0);
        return true;
    }
}
