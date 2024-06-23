using UnityEngine;

public abstract class Debuff
{
    public GameObject target;
    public GameObject effect;
    public int remainTurn;
    public int count;
    public string name;

    public abstract void TimeCheck();

    public bool DeleteEffect()
    {
        if (remainTurn > 0)
        {
            return false;
        }

        target.GetComponent<CharacterState>().DelDebuff(this);
        EndAnimation();
        return true;
    }

    public abstract bool StartEffect();

    public abstract void SetDebuff(int turn, GameObject target, short attribute = 0, bool TurnEnd = false);

    public void StartAnimation()
    {
        effect = Managers.Skill.InstantiateEffect(name, target);
        effect.GetComponent<ParticleSystem>().Play();
    }

    public void EndAnimation()
    {
        effect.GetComponent<ParticleSystem>().Pause();
        Managers.Destroy(effect);
    }
}