public abstract class Travel : Skill
{
    public abstract bool Activate(); //use skill
    public override void Start()
    {
        Managers.Data.SetTravelSkill(skillId, this);
    }
}
