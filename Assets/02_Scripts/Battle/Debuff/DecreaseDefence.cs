using UnityEngine;

public class DecreaseDefence : Debuff
{
	private int decShd;
	public override void TimeCheck()
	{
		remainTurn--;
		if (remainTurn == 0)
		{
			Managers.Active.ModifyDefense(target, +1 * decShd);
			DeleteEffect();
		}
	}
	public override bool StartEffect()
	{
		if (target == null)
			return false;
		Managers.Active.ModifyDefense(target, -1 * decShd);
		TimeCheck();
		return true;
	}

	public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool TurnEnd = false)
	{
		this.target = target;
		this.remainTurn = turn;
		decShd = attribute;
		target.GetComponent<CharacterState>().AddDebuff(this, TurnEnd);
		StartEffect();
	}
}
