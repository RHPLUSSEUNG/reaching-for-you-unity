using Unity.VisualScripting;
using UnityEngine;

public abstract class Active : Skill
{
    public int stamina; //skill cool time
    public int mp;
    public GameObject target;
    public GameObject Effect;
    public int range;
    public bool other = false;

    public abstract bool Activate(); //use skill
    public virtual bool SetTarget(GameObject target)
    {
        this.target = target;
        if (!Activate())
        {
            Debug.Log("Fail to Active Skill");
            return false;
        }
        Managers.Manager.StartCoroutine(Managers.Skill.StartEffect(Effect, target.transform.position, other));
        return true;
    }

    public override void Start()
    {
        Managers.Data.SetPlayerSkill(skillId, this);
        if (this.target_object != TargetObject.Me)
        {
            other = true;
        }
    }
}