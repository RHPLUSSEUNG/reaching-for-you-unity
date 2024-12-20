using UnityEngine;

public abstract class MonsterSkill : Skill
{
    public int stamina; //skill cool time
    public int mp;
    public GameObject target;
    public Vector3 targetPos;
    public GameObject Effect;
    public int range;

    public abstract bool Activate(); //use skill
    public virtual bool SetTarget(GameObject target)
    {
        this.target = target;
        if (!Activate())
        {
            Debug.Log("Fail to Active Skill");
            return false;
        }
        //Managers.Manager.StartCoroutine(Managers.Skill.StartEffect(Effect, target.transform.position));
        return true;
    }
    public virtual bool SetTarget(Vector3 targetpos)
    {
        this.targetPos = targetpos;
        if (!Activate())
        {
            Debug.Log("Fail to Active Skill");
            return false;
        }
        //Managers.Manager.StartCoroutine(Managers.Skill.StartEffect(Effect, targetpos));
        return true;
    }

    public override void Start()
    {
        Managers.Data.SetMonsterSkill(skillId, this);
    }
}