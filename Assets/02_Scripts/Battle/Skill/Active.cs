using UnityEngine;

public abstract class Active : Skill
{
    public int stamina; //skill cool time
    public int mp;
    public GameObject target;
    bool[,] skillRange = new bool[11, 11]; //[6][6] activate pos
    public abstract bool Activate(); //use skill
    public virtual bool SetTarget(GameObject target)
    {
        this.target = target;
        Activate();
        return true;
    }
    public int range;

    public override void Start()
    {
        Managers.Data.SetSkill(skillId, this);
    }
}